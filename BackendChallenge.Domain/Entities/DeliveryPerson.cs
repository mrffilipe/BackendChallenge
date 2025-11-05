using BackendChallenge.Domain.Common;
using BackendChallenge.Domain.Enums;

namespace BackendChallenge.Domain.Entities
{
    public class DeliveryPerson : BaseEntity
    {
        public string ExternalId { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Cnpj { get; private set; } = string.Empty;
        public DateOnly DateOfBirth { get; private set; }
        public string DriversLicense { get; private set; } = string.Empty;
        public TypeOfDriversLicense TypeOfDriversLicense { get; private set; }
        public string UrlOfTheDriversLicenseImage { get; private set; } = string.Empty;

        public ICollection<MotorcycleRental> Rentals { get; private set; } = [];

        private DeliveryPerson()
        {
        }

        public DeliveryPerson(
            string externalId,
            string name,
            string cnpj,
            DateOnly dateOfBirth,
            string driversLicense,
            TypeOfDriversLicense typeOfDriversLicense,
            string urlOfTheDriversLicenseImage)
        {
            ExternalId = externalId;
            Name = name;
            Cnpj = cnpj;
            DateOfBirth = dateOfBirth;
            DriversLicense = driversLicense;
            TypeOfDriversLicense = typeOfDriversLicense;
            UrlOfTheDriversLicenseImage = urlOfTheDriversLicenseImage;
        }
    }
}
