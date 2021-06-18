using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookReviewing.Api.Consumers
{
    public abstract class RabbitMqListener<TMessage> : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public RabbitMqListener(string queueName)
        {
            InitializeRabbitMqListener(queueName);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonConvert.DeserializeObject<TMessage>(content);

                HandleMessage(message);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            //consumer.Shutdown += OnConsumerShutdown;
            //consumer.Registered += OnConsumerRegistered;
            //consumer.Unregistered += OnConsumerUnregistered;
            //consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void InitializeRabbitMqListener(string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _queueName = queueName;
            _connection = factory.CreateConnection();
            //_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected abstract void HandleMessage(TMessage message);
    }
}
