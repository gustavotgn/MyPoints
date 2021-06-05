using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.CommandContract.Interfaces
{
    public interface IMessageService
    {
        bool Enqueue<T>(string queueName, T obj);
    }
}
