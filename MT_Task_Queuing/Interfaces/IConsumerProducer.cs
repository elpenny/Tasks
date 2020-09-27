using System.Threading;

namespace MT_Task_Queuing.Interfaces
{
    public interface IConsumerProducer
    {
        public void DoWork(CancellationToken token);
    }
}
