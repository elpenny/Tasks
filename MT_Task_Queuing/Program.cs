using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MT_Task_Queuing.Config;
using MT_Task_Queuing.Interfaces;
using MT_Task_Queuing.Services;
using MT_Task_Queuing.Services.ExpressionGenerators;

namespace MT_Task_Queuing
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new ConcurrentQueue<Task<string>>();
            Random seedGenerator = new Random();
            var config = new Configuration
            (
                queueMaxSize: 1000, 
                producerPollingDelay: 2000, 
                verboseLogging: false, 
                producerCount: 4, 
                consumerCount: 2, 
                taskOperationsMaxCount: 7, 
                taskNumbersMaxValue: 5000,
                consumerDelay: 50
            );
            

            using (var taskList = new BlockingCollection<Task<string>>(queue, config.QueueMaxSize))
            {
                CreateProducersAndConsumers(seedGenerator, config, taskList, out List<IConsumerProducer> producerConsumerList);

                var token = new CancellationToken();

                List<Thread> threadList = PrepareThreads(producerConsumerList, token);

                foreach (var thread in threadList)
                {
                    thread.Start();
                }

                foreach (var thread in threadList)
                {
                    thread.Join();

                }

                Console.WriteLine("Finished");
            }
        }

        private static void CreateProducersAndConsumers(Random seedGenerator, Configuration config, BlockingCollection<Task<string>> taskList, out List<IConsumerProducer> producerConsumerList)
        {
            producerConsumerList = new List<IConsumerProducer>();
            for (int i = 0; i < config.ProducerCount; i++)
            {
                producerConsumerList.Add(new TaskProducer(taskList, new ExpressionGenerator(seedGenerator.Next(), config), new ExpressionEvaluator(), $"Producer {i + 1}", config));
            }

            for (int i = 0; i < config.ConsumerCount; i++)
            {
                producerConsumerList.Add(new TaskConsumer(taskList, $"Consumer {i + 1}", config));
            }
        }

        private static List<Thread> PrepareThreads(IList<IConsumerProducer> producerConsumerList, CancellationToken token)
        {
            var threadList = new List<Thread>();

            foreach (var consumerProducer in producerConsumerList)
            {
                threadList.Add(new Thread(() =>
                {
                    consumerProducer.DoWork(token);
                }));
            }

            return threadList;
        }
    }
}
