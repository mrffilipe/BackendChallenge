using BackendChallenge.Domain.Common;

namespace BackendChallenge.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid MotorcycleId { get; set; }      // FK interna
        public string MotorcycleExternalId { get; set; } = string.Empty;
        public string Type { get; set; } = "motorcycle.created.2024";
    }
}
