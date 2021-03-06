﻿using MT_Task_Queuing.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT_Task_Queuing.Config
{
    public class Configuration : IConfiguration
    {
        public int QueueMaxSize { get; }
        public int ProducerPollingDelay { get; }
        public bool VerboseLogging { get; }
        public int ProducerCount { get; }
        public int ConsumerCount { get; }
        public int TaskOperationsMaxCount { get; }
        public int TaskNumbersMinMaxValue { get; }
        public int ConsumerDelay { get; }

        public Configuration(int queueMaxSize = 100, int producerPollingDelay = 2000, bool verboseLogging = false, int producerCount = 4, int consumerCount = 2, int taskOperationsMaxCount = 7, int taskNumbersMaxValue = 5000, int consumerDelay = 500)
        {
            QueueMaxSize = queueMaxSize;
            ProducerPollingDelay = producerPollingDelay;
            VerboseLogging = verboseLogging;
            ProducerCount = producerCount >= 4 ? producerCount : 4;
            ConsumerCount = consumerCount >= 2 ? consumerCount : 2;
            TaskOperationsMaxCount = taskOperationsMaxCount;
            TaskNumbersMinMaxValue = taskNumbersMaxValue;
            ConsumerDelay = consumerDelay;
        }
    }
}
