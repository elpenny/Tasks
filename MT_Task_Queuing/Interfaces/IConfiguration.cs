using System;
using System.Collections.Generic;
using System.Text;

namespace MT_Task_Queuing.Interfaces
{
    interface IConfiguration
    {
        int QueueMaxSize { get; }
        int ProducerPollingDelay { get; }
        bool VerboseLogging { get; }
        int ProducerCount { get; }
    }
}
