using BackendChallenge.Domain.Common;

namespace BackendChallenge.Domain.Entities
{
    public class DeliveryPerson : BaseEntity
    {
        public string ExternalId { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Cnpj { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public string DriversLicense { get; private set; } = string.Empty;
        /*
         * Para uma maior consistência no sistema, eu recomendaria que os tipos da CNH estivessem contidos dentro de um Enum.
         * Deixei o enum recomendado em Enums/TypeOfDriversLicense.
         * //public TypeOfDriversLicense TypeOfDriversLicense { get; private set; }
         */
        public string TypeOfDriversLicense { get; private set; } = string.Empty;
        public string UrlOfTheDriversLicenseImage { get; private set; } = string.Empty;

        public ICollection<MotorcycleRental> MotorcycleRentals { get; private set; } = [];

        private DeliveryPerson()
        {
        }

        public DeliveryPerson(
            string externalId,
            string name,
            string cnpj,
            DateTime dateOfBirth,
            string driversLicense,
            string typeOfDriversLicense,
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

        public static bool TypeOfDriversLicenseIsValid(string type)
        {
            return type == "A" || type == "B" || type == "A+B";
        }

        public void SetDriversLicenseImageUrl(string url)
        {
            UrlOfTheDriversLicenseImage = url;
        }
    }
}
