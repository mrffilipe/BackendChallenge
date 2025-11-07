using BackendChallenge.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace BackendChallenge.Infrastructure.Services
{
    public sealed class RabbitMqMessageBus : IMessageBus, IAsyncDisposable
    {
        private readonly ILogger<RabbitMqMessageBus> _logger;
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly string _exchange;

        public RabbitMqMessageBus(IConfiguration cfg, ILogger<RabbitMqMessageBus> logger)
        {
            _logger = logger;

            var uri = cfg["Rabbit:Connection"] ?? cfg["Rabbit__Connection"];
            _exchange = cfg["Rabbit:Exchange"] ?? "backendchallenge";

            var factory = new ConnectionFactory
            {
                Uri = new Uri(uri),
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true,
                ClientProvidedName = "backendchallenge:publisher"
            };

            // Cria conexão e canal de forma síncrona (ou pode ser assíncrona)
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            // Declara o exchange de forma assíncrona
            _channel.ExchangeDeclareAsync(
                exchange: _exchange,
                type: ExchangeType.Topic,
                durable: true,
                autoDelete: false
            ).GetAwaiter().GetResult();

            _logger.LogInformation("RabbitMQ connected and exchange declared: {Exchange}", _exchange);
        }

        public async Task PublishAsync(string exchange, string routingKey, string payload, CancellationToken ct = default)
        {
            var targetExchange = string.IsNullOrWhiteSpace(exchange) ? _exchange : exchange;

            var props = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent
            };

            var body = Encoding.UTF8.GetBytes(payload);

            await _channel.BasicPublishAsync(
                exchange: targetExchange,
                routingKey: routingKey,
                mandatory: true,
                basicProperties: props,
                body: body,
                cancellationToken: ct
            );

            _logger.LogInformation("Published event {RoutingKey} to {Exchange}", routingKey, targetExchange);
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_channel != null)
                {
                    await _channel.CloseAsync();
                    await _channel.DisposeAsync();
                }

                if (_connection != null)
                {
                    await _connection.CloseAsync();
                    await _connection.DisposeAsync();
                }

                _logger.LogInformation("RabbitMQ connection closed gracefully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disposing RabbitMQ resources");
            }
        }
    }
}
