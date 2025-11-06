using BackendChallenge.Application.Services;
using BackendChallenge.Domain.Entities;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Application.UseCases
{
    public class RegisterMotorcycleRental : IRegisterMotorcycleRental
    {
        private readonly IMotorcycleQueryRepository _motorcycleQueryRepository;
        private readonly IDeliveryPersonQueryRepository _deliveryPersonQueryRepository;
        private readonly IMotorcycleRentalRepository _motorcycleRentalRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;

        public RegisterMotorcycleRental(
            IMotorcycleQueryRepository motorcycleQueryRepository,
            IDeliveryPersonQueryRepository deliveryPersonQueryRepository,
            IMotorcycleRentalRepository motorcycleRentalRepository,
            IUnityOfWorkService unityOfWorkService)
        {
            _motorcycleQueryRepository = motorcycleQueryRepository;
            _deliveryPersonQueryRepository = deliveryPersonQueryRepository;
            _motorcycleRentalRepository = motorcycleRentalRepository;
            _unityOfWorkService = unityOfWorkService;
        }

        public async Task ExecuteAsync(RegisterMotorcycleRentalDto dto)
        {
            var motorCycle = await _motorcycleQueryRepository.GetByExternalIdAsync(dto.moto_id);
            if (motorCycle == null)
                throw new Exception("Moto com o identificador informado não encontrada.");

            var deliveryPerson = await _deliveryPersonQueryRepository.GetByExternalIdAsync(dto.entregador_id);
            if (deliveryPerson == null)
                throw new Exception("Entregador com o identificador informado não encontrado.");

            var entity = new MotorcycleRental(
                deliveryPerson.Id,
                motorCycle.Id,
                dto.data_inicio,
                dto.data_termino,
                dto.data_previsao_termino,
                dto.plano);

            await _motorcycleRentalRepository.AddAsync(entity);

            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
