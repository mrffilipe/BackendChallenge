using BackendChallenge.Domain.Common;
using BackendChallenge.Domain.Enums;

namespace BackendChallenge.Domain.Entities
{
    public class MotorcycleRental : BaseEntity
    {
        public Guid DeliveryPersonId { get; private set; }
        public DeliveryPerson DeliveryPerson { get; private set; } = null!;
        public Guid MotorcycleId { get; private set; }
        public Motorcycle Motorcycle { get; private set; } = null!;
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public DateOnly EstimatedEndDate { get; private set; }
        public RentalPlan RentalPlan { get; private set; }

        private MotorcycleRental()
        {
        }

        public MotorcycleRental(
            Guid deliveryPersonId,
            Guid motorcycleId,
            DateOnly startDate,
            DateOnly endDate,
            DateOnly estimatedEndDate,
            RentalPlan rentalPlan)
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
