using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Tests
{
    public class NotificationTests
    {
        [Fact]
        public void Notification_deve_ter_valores_iniciais_padrao()
        {
            var n = new Notification { MotorcycleId = Guid.NewGuid(), MotorcycleExternalId = "ext-3" };
            Assert.Equal("motorcycle.created.2024", n.Type);
            Assert.Equal("ext-3", n.MotorcycleExternalId);
            Assert.NotEqual(Guid.Empty, n.MotorcycleId);
        }
    }
}
