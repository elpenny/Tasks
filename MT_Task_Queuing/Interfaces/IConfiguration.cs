namespace MT_Task_Queuing.Interfaces
{
    internal interface IConfiguration
    {
        int QueueMaxSize { get; }
        int ProducerPollingDelay { get; }
        bool VerboseLogging { get; }
        int ProducerCount { get; }
        int TaskOperationsMaxCount { get; }
        int TaskNumbersMaxValue { get; }
        int ConsumerDelay { get; }
    }
}
