using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Tests
{
    public class MotorcycleRentalTests
    {
        [Fact]
        public void Construtor_deve_preencher_campos_basicos()
        {
            var dpId = Guid.NewGuid();
            var motoId = Guid.NewGuid();
            var start = DateTime.UtcNow.Date;
            var estimated = start.AddDays(7);
            var end = start.AddDays(3);

            var rental = new MotorcycleRental(dpId, motoId, start, end, estimated, rentalPlan: 7);

            Assert.Equal(dpId, rental.DeliveryPersonId);
            Assert.Equal(motoId, rental.MotorcycleId);
            Assert.Equal(start, rental.StartDate);
            Assert.Equal(end, rental.EndDate);
            Assert.Equal(estimated, rental.EstimatedEndDate);
            Assert.Equal(7, rental.RentalPlan);
            Assert.False(string.IsNullOrWhiteSpace(rental.ExternalId));
        }
    }
}
