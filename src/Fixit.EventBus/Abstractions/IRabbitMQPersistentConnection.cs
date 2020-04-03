﻿using System;
using RabbitMQ.Client;

namespace Fixit.EventBus.Abstractions
{
    public interface IRabbitMQPersistentConnection
        : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}