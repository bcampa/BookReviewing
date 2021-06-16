using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookReviewing.Api
{
    public class BookCreatedConsumer : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private string _queueName = "book-created";

        public BookCreatedConsumer()
        {
            InitializeRabbitMqListener();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var bookCreated = JsonConvert.DeserializeObject<BookCreatedMessage>(content);

                //HandleMessage(bookCreated);
                Console.WriteLine("A MENSAGEM CHEGOU");

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            //consumer.Shutdown += OnConsumerShutdown;
            //consumer.Registered += OnConsumerRegistered;
            //consumer.Unregistered += OnConsumerUnregistered;
            //consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            //_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
    }

    public class BookCreatedMessage
    {
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
