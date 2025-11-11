using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Domain.Tests
{
    public class MotorcycleTests
    {
        [Fact]
        public void Construtor_deve_preencher_campos()
        {
            var moto = new Motorcycle("ext-2", 2020, "CG 160", "ABC1D23");
            Assert.Equal("ext-2", moto.ExternalId);
            Assert.Equal(2020, moto.Year);
            Assert.Equal("CG 160", moto.Model);
            Assert.Equal("ABC1D23", moto.Plate);
        }

        [Fact]
        public void UpdatePlate_deve_atualizar_placa_quando_valida()
        {
            var moto = new Motorcycle("ext-2", 2020, "CG 160", "ABC1D23");
            moto.UpdatePlate("XYZ9Z99");
            Assert.Equal("XYZ9Z99", moto.Plate);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdatePlate_deve_lancar_excecao_quando_nula_ou_vazia(string invalid)
        {
            var moto = new Motorcycle("ext-2", 2020, "CG 160", "ABC1D23");
            var ex = Assert.Throws<Exception>(() => moto.UpdatePlate(invalid));
            Assert.Contains("placa não pode ser nula ou vazia", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void HasMotorcycleRentals_deve_indicar_se_possui_alugueis()
        {
            var moto = new Motorcycle("ext-2", 2020, "CG 160", "ABC1D23");
            Assert.False(moto.HasMotorcycleRentals());

            // simula 1 aluguel associado
            moto.MotorcycleRentals.Add(new MotorcycleRental(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, 1));
            Assert.True(moto.HasMotorcycleRentals());
        }
    }
}
