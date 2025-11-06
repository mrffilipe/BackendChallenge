using BackendChallenge.Domain.Common;

namespace BackendChallenge.Domain.Entities
{
    public class MotorcycleRental : BaseEntity
    {
        public string ExternalId { get; private set; } = $"loc-{Guid.NewGuid()}";
        public Guid DeliveryPersonId { get; private set; }
        public DeliveryPerson DeliveryPerson { get; private set; } = null!;
        public Guid MotorcycleId { get; private set; }
        public Motorcycle Motorcycle { get; private set; } = null!;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime EstimatedEndDate { get; private set; }
        /*
         * Para uma maior consistência no sistema, eu recomendaria que os planos estivessem contidos dentro de um Enum.
         * Deixei o enum recomendado em Enums/RentalPlan.
         * public RentalPlan RentalPlan { get; private set; }
         */
        public int RentalPlan { get; private set; }

        private MotorcycleRental()
        {
        }

        public MotorcycleRental(
            Guid deliveryPersonId,
            Guid motorcycleId,
            DateTime startDate,
            DateTime endDate,
            DateTime estimatedEndDate,
            int rentalPlan)
        {
            DeliveryPersonId = deliveryPersonId;
            MotorcycleId = motorcycleId;
            StartDate = startDate;
            EndDate = endDate;
            EstimatedEndDate = estimatedEndDate;
            RentalPlan = rentalPlan;
        }
    }
}
