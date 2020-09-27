using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MT_Task_Queuing.Config;
using MT_Task_Queuing.Services;

namespace MT_Task_Queuing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var queue = new ConcurrentQueue<Task<string>>();
            var config = new Configuration(20, 2000, true, 4, 2);

            using (var taskList = new BlockingCollection<Task<string>>(queue, config.QueueMaxSize))
            {
                var producer1 = new TaskProducer(taskList, new ExpressionGenerator(), new ExpressionEvaluator(), "producer 1", config);
                var producer2 = new TaskProducer(taskList, new ExpressionGenerator(), new ExpressionEvaluator(), "producer 2", config);
                var producer3 = new TaskProducer(taskList, new ExpressionGenerator(), new ExpressionEvaluator(), "producer 3", config);
                var producer4 = new TaskProducer(taskList, new ExpressionGenerator(), new ExpressionEvaluator(), "producer 4", config);

                var consumer1 = new TaskConsumer(taskList, "consumer1", config);
                var consumer2 = new TaskConsumer(taskList, "consumer2", config);

                var token = new CancellationToken();


                Thread p1 = new Thread(() =>
                {
                    producer1.DoWork(token);
                });
                Thread p2 = new Thread(() =>
                {
                    producer2.DoWork(token);
                });
                Thread p3 = new Thread(() =>
                {
                    producer3.DoWork(token);
                });
                Thread p4 = new Thread(() =>
                {
                    producer4.DoWork(token);
                });

                Thread c1 = new Thread(() =>
                {
                    consumer1.DoWork(token);
                });

                Thread c2 = new Thread(() =>
                {
                    consumer2.DoWork(token);
                });

                p1.Start();
                p2.Start();
                p3.Start();
                p4.Start();
                c1.Start();
                c2.Start();

                p1.Join();
                p2.Join();
                p3.Join();
                p4.Join();
                c1.Join();
                c2.Join();


                Console.WriteLine("Finished");
            }        
        }
    }
}
