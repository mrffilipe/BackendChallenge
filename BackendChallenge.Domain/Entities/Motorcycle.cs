using BackendChallenge.Domain.Common;

namespace BackendChallenge.Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        public string ExternalId { get; private set; } = string.Empty;
        public int Year { get; private set; }
        public string Model { get; private set; } = string.Empty;
        public string Plate { get; private set; } = string.Empty;

        public ICollection<MotorcycleRental> Rentals { get; private set; } = [];

        private Motorcycle()
        {
        }

        public Motorcycle(string externalId, int year, string model, string plate)
        {
            ExternalId = externalId;
            Year = year;
            Model = model;
            Plate = plate;
        }
    }
}
