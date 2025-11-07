namespace BackendChallenge.Application.Services
{
    public interface IMessageBus
    {
        Task PublishAsync(string exchange, string routingKey, string payload, CancellationToken ct = default);
    }
}
