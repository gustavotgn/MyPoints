using MyPoints.CommandContract.Interfaces;

namespace MyPoints.Identity.Test
{
    public class FakeMessageService : IMessageService
    {
        public bool Enqueue<T>(string queueName, T obj)
        {
            return true;
        }

    }
}
