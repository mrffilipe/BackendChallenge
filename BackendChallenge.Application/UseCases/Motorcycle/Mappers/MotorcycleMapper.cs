using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Application.UseCases
{
    public static class MotorcycleMapper
    {
        public static Motorcycle ToEntity(this RegisterMotorcycleDto dto)
        {
            return new Motorcycle(dto.identificador, dto.ano, dto.modelo, dto.placa);
        }
    }
}
