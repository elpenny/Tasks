﻿using MT_Task_Queuing.Config;
using MT_Task_Queuing.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MT_Task_Queuing
{
    internal class TaskProducer
    {
        private BlockingCollection<Task<string>> _taskList;
        private TaskGenerator _generator;
        private ExpressionEvaluator _evaluator;
        private readonly IConfiguration _configuration;

        private int _itemAdded = 0;
        private int _initialLoadCount;
        private string _name;

        public TaskProducer(BlockingCollection<Task<string>> taskList, TaskGenerator generator, ExpressionEvaluator evaluator, string name, IConfiguration configuration)
        {
            _taskList = taskList;
            _generator = generator;
            _evaluator = evaluator;
            _name = name;
            _configuration = configuration;
            _initialLoadCount = configuration.QueueMaxSize / configuration.ProducerCount;
        }

        public void DoWork(CancellationToken token)
        {
            AddUntilFull();
            Task.Run(() => MonitorQueueAsync(token));
        }

        private void AddUntilFull()
        {
            for (int i = 0; i < _initialLoadCount; i++)
            {
                if(_taskList.Count == _configuration.QueueMaxSize)
                {
                    break;
                }

                if(_taskList.TryAdd(ProduceNewTask()))
                {
                    if (_configuration.VerboseLogging)
                    {
                        Console.WriteLine($"Producer : {_name} : added item {++_itemAdded}");
                    }
                }
            }

            if(_configuration.VerboseLogging)
            {
                Console.WriteLine($"Producer: {_name} : added { _itemAdded} items");
            }
        }

        private async Task MonitorQueueAsync(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                await Task.Delay(_configuration.ProducerPollingDelay);
                Poll();
            }
        }

        private void Poll()
        {
            if(_taskList.Count <= _configuration.QueueMaxSize / 2)
            {
                AddUntilFull();
            }
            else
            {
                if(_configuration.VerboseLogging)
                {
                    Console.WriteLine($"Producer: {_name} : waiting for queue to empty in half.");
                }
            }
        }

        private Task<string> ProduceNewTask()
        {
            return new Task<string>(() =>
            {
                string expression = _generator.Next();
                return _evaluator.Anaylyze(expression, _configuration.VerboseLogging);
            });
        }
    }
}