using BackendChallenge.Application.Services;
using BackendChallenge.Domain.Entities;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Application.UseCases
{
    public class RegisterDeliveryPerson : IRegisterDeliveryPerson
    {
        private readonly IDeliveryPersonQueryRepository _deliveryPersonQueryRepository;
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;

        public RegisterDeliveryPerson(
            IDeliveryPersonQueryRepository deliveryPersonQueryRepository,
            IDeliveryPersonRepository deliveryPersonRepository,
            IUnityOfWorkService unityOfWorkService)
        {
            _deliveryPersonQueryRepository = deliveryPersonQueryRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
            _unityOfWorkService = unityOfWorkService;
        }

        public async Task ExecuteAsync(RegisterDeliveryPersonDto dto)
        {
            if (!DeliveryPerson.TypeOfDriversLicenseIsValid(dto.tipo_cnh))
                throw new Exception("Tipo da CNH inválido! Tente 'A', 'B' ou 'AB'");

            var result = await _deliveryPersonQueryRepository.GetByExternalIdAsync(dto.identificador);

            if (result != null)
                throw new Exception("Entregador com o mesmo identificador já registrado.");

            await _deliveryPersonRepository.AddAsync(dto.ToEntity());

            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
