using BackendChallenge.Domain.Common;

namespace BackendChallenge.Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        public string ExternalId { get; private set; } = string.Empty;
        public int Year { get; private set; }
        public string Model { get; private set; } = string.Empty;
        public string Plate { get; private set; } = string.Empty;

        public ICollection<MotorcycleRental> MotorcycleRentals { get; private set; } = [];

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

        public void UpdatePlate(string plate)
        {
            if (string.IsNullOrEmpty(plate))
                throw new Exception("A placa não pode ser nula ou vazia");

            Plate = plate;
        }

        public bool HasMotorcycleRentals()
        {
            return MotorcycleRentals.Count > 0;
        }
    }
}
