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
            Console.WriteLine("Hello World!");

            var queue = new ConcurrentQueue<Task<string>>();
            Random seedGenerator = new Random();
            var config = new Configuration(
                queueMaxSize: 100, 
                producerPollingDelay: 2000, 
                verboseLogging: true, 
                producerCount: 4, 
                consumerCount: 2, 
                taskOperationsMaxCount: 5, 
                taskNumbersMaxValue: 100,
                consumerDelay: 100);
            

            using (var taskList = new BlockingCollection<Task<string>>(queue, config.QueueMaxSize))
            {
                var producerList = new List<TaskProducer>();
                for (int i = 0; i < config.ProducerCount; i++)
                {
                    producerList.Add(new TaskProducer(taskList, new ExpressionGenerator(seedGenerator.Next(), config), new ExpressionEvaluator(), $"Producer {i+1}", config));
                }

                var consumerList = new List<TaskConsumer>();
                for (int i = 0; i < config.ConsumerCount; i++)
                {
                    consumerList.Add(new TaskConsumer(taskList, $"Consumer {i+1}", config));
                }

                var token = new CancellationToken();
                var threadList = new List<Thread>();

                foreach(var producer in producerList)
                {
                    threadList.Add(new Thread(() =>
                        {
                            producer.DoWork(token);
                        })
                    );
                }

                foreach (var consumer in consumerList)
                {
                    threadList.Add(new Thread(() =>
                        {
                            consumer.DoWork(token);
                        })
                    );
                }

                foreach(var thread in threadList)
                {
                    thread.Start();
                }

                foreach(var thread in threadList)
                {
                    thread.Join();
                }

                Console.WriteLine("Finished");
            }        
        }
    }
}
