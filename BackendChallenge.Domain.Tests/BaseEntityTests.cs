using BackendChallenge.Domain.Common;

namespace BackendChallenge.Domain.Tests
{
    public class BaseEntityTests
    {
        private class DummyEntity : BaseEntity { }

        [Fact]
        public void SetCreatedAt_deve_definir_CreatedAt_e_UpdatedAt_para_agora()
        {
            var entity = new DummyEntity();
            var before = DateTime.UtcNow;

            entity.SetCreatedAt();

            var after = DateTime.UtcNow;

            Assert.True(entity.CreatedAt >= before && entity.CreatedAt <= after);
            Assert.True(entity.UpdatedAt >= before && entity.UpdatedAt <= after);
            Assert.Equal(entity.CreatedAt, entity.UpdatedAt);
        }

        [Fact]
        public void SetUpdatedAt_deve_atualizar_apenas_UpdatedAt()
        {
            var entity = new DummyEntity();
            entity.SetCreatedAt();
            var createdAt = entity.CreatedAt;

            var before = DateTime.UtcNow;
            entity.SetUpdatedAt();
            var after = DateTime.UtcNow;

            Assert.Equal(createdAt, entity.CreatedAt);
            Assert.True(entity.UpdatedAt >= before && entity.UpdatedAt <= after);
            Assert.True(entity.UpdatedAt >= entity.CreatedAt);
        }
    }
}
