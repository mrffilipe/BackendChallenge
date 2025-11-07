using BackendChallenge.Domain.Common;
using BackendChallenge.Domain.Entities;
using BackendChallenge.Infrastructure.Persistence;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BackendChallenge.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _sp;
    private readonly string _amqp;
    private IConnection? _connection;
    private IChannel? _channel;

    public Worker(ILogger<Worker> logger, IConfiguration cfg, IServiceProvider sp)
    {
        _logger = logger;
        _sp = sp;
        _amqp = cfg["Rabbit:Connection"] ?? cfg["Rabbit__Connection"]!;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_amqp),
            AutomaticRecoveryEnabled = true,
            TopologyRecoveryEnabled = true,
            ClientProvidedName = "backendchallenge:worker"
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(null, cancellationToken);

        await _channel.ExchangeDeclareAsync(
            exchange: "backendchallenge",
            type: ExchangeType.Topic,
            durable: true,
            autoDelete: false,
            cancellationToken: cancellationToken);

        await _channel.QueueDeclareAsync(
            queue: "backendchallenge.motorcycle.created",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        await _channel.QueueBindAsync(
            queue: "backendchallenge.motorcycle.created",
            exchange: "backendchallenge",
            routingKey: "motorcycle.created.v1",
            arguments: null,
            cancellationToken: cancellationToken);

        _logger.LogInformation("RabbitMQ worker connected and queue bound.");
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_channel is null)
        {
            _logger.LogError("RabbitMQ channel is not initialized.");
            return;
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (ch, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var evt = JsonSerializer.Deserialize<MotorcycleCreated>(json);

                if (evt is null)
                    throw new InvalidOperationException("Invalid payload received from RabbitMQ.");

                if (evt.Year == 2024)
                {
                    using var scope = _sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    await db.AddAsync(new Notification
                    {
                        MotorcycleId = evt.Id,
                        MotorcycleExternalId = evt.ExternalId,
                        Type = "motorcycle.created.2024"
                    });

                    await db.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("Notification persisted for motorcycle {ExternalId}", evt.ExternalId);
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message from RabbitMQ");
                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: "backendchallenge.motorcycle.created",
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        _logger.LogInformation("Worker subscribed to queue backendchallenge.motorcycle.created");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
        {
            await _channel.CloseAsync(cancellationToken);
            await _channel.DisposeAsync();
        }

        if (_connection != null)
        {
            await _connection.CloseAsync(cancellationToken);
            await _connection.DisposeAsync();
        }

        _logger.LogInformation("RabbitMQ worker stopped gracefully.");
        await base.StopAsync(cancellationToken);
    }
}
