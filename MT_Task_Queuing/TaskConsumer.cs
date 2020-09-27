using MT_Task_Queuing.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MT_Task_Queuing
{
    internal class TaskConsumer
    {
        private BlockingCollection<Task<string>> _taskList;
        private readonly string _name;
        private readonly IConfiguration _configuration;

        public TaskConsumer(BlockingCollection<Task<string>> taskList, string name, IConfiguration configuration)
        {
            _taskList = taskList;
            _name = name;
            _configuration = configuration;
        }

        public void DoWork(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                foreach(var task in _taskList.GetConsumingEnumerable())
                {
                    Thread.Sleep(_configuration.ConsumerDelay);

                    task.Start();
                    task.Wait();

                    Console.WriteLine($"Consumer: {_name}, result: {task.Result}");
                }
            }
        }
    }
}