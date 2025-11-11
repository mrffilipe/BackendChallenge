using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Tests
{
    public class DeliveryPersonTests
    {
        [Fact]
        public void Construtor_deve_preencher_campos_obrigatorios()
        {
            var dp = new DeliveryPerson(
                externalId: "ext-1",
                name: "João",
                cnpj: "12345678000199",
                dateOfBirth: new DateTime(1999, 10, 10),
                driversLicense: "12345",
                typeOfDriversLicense: "A",
                urlOfTheDriversLicenseImage: "http://cdn/cnh.png"
            );

            Assert.Equal("ext-1", dp.ExternalId);
            Assert.Equal("João", dp.Name);
            Assert.Equal("12345678000199", dp.Cnpj);
            Assert.Equal(new DateTime(1999, 10, 10), dp.DateOfBirth);
            Assert.Equal("12345", dp.DriversLicense);
            Assert.Equal("A", dp.TypeOfDriversLicense);
            Assert.Equal("http://cdn/cnh.png", dp.UrlOfTheDriversLicenseImage);
        }

        [Theory]
        [InlineData("A", true)]
        [InlineData("B", true)]
        [InlineData("A+B", true)]
        [InlineData("C", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void TypeOfDriversLicenseIsValid_deve_validar_tipos_suportados(string type, bool esperado)
        {
            var valido = DeliveryPerson.TypeOfDriversLicenseIsValid(type);
            Assert.Equal(esperado, valido);
        }

        [Fact]
        public void SetDriversLicenseImageUrl_deve_atualizar_url()
        {
            var dp = new DeliveryPerson("e", "n", "c", DateTime.UtcNow, "d", "A", "old");
            dp.SetDriversLicenseImageUrl("http://novo/cnh.png");
            Assert.Equal("http://novo/cnh.png", dp.UrlOfTheDriversLicenseImage);
        }
    }
}
