using Blog.Core.Utilities;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.RabbitMQ
{
    public interface IQueueFactory
    {
        Task PublishAsync<T>(T data, string queueName);
        Task<IChannel> ConnectAsync(string queueName);
    }
}
