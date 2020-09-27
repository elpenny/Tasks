using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MT_Task_Queuing.Config;
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
                CreateProducersAndConsumers(seedGenerator, config, taskList, out List<TaskProducer> producerList, out List<TaskConsumer> consumerList);

                var token = new CancellationToken();

                List<Thread> threadList = PrepareThreads(producerList, consumerList, token);

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

        private static void CreateProducersAndConsumers(Random seedGenerator, Configuration config, BlockingCollection<Task<string>> taskList, out List<TaskProducer> producerList, out List<TaskConsumer> consumerList)
        {
            producerList = new List<TaskProducer>();
            for (int i = 0; i < config.ProducerCount; i++)
            {
                producerList.Add(new TaskProducer(taskList, new ExpressionGenerator(seedGenerator.Next(), config), new ExpressionEvaluator(), $"Producer {i + 1}", config));
            }

            consumerList = new List<TaskConsumer>();
            for (int i = 0; i < config.ConsumerCount; i++)
            {
                consumerList.Add(new TaskConsumer(taskList, $"Consumer {i + 1}", config));
            }
        }

        private static List<Thread> PrepareThreads(List<TaskProducer> producerList, List<TaskConsumer> consumerList, CancellationToken token)
        {
            var threadList = new List<Thread>();

            foreach (var producer in producerList)
            {
                threadList.Add(new Thread(() =>
                {
                    producer.DoWork(token);
                }));
            }

            foreach (var consumer in consumerList)
            {
                threadList.Add(new Thread(() =>
                {
                    consumer.DoWork(token);
                }));
            }

            return threadList;
        }
    }
}
