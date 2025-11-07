using BackendChallenge.Domain.Entities;

namespace BackendChallenge.Application.UseCases
{
    public static class DeliveryPersonMapper
    {
        public static DeliveryPerson ToEntity(this RegisterDeliveryPersonDto dto)
        {
            return new DeliveryPerson(
                dto.identificador,
                dto.nome,
                dto.cnpj,
                dto.data_nascimento,
                dto.numero_cnh,
                dto.tipo_cnh,
                "");
        }
    }
}
