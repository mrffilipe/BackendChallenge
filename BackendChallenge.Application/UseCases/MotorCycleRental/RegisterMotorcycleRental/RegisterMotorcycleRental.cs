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
            var motorcycle = await _motorcycleQueryRepository.GetByExternalIdAsync(dto.moto_id)
                ?? throw new Exception("Moto com o identificador informado não encontrada.");

            var deliveryPerson = await _deliveryPersonQueryRepository.GetByExternalIdAsync(dto.entregador_id)
                ?? throw new Exception("Entregador com o identificador informado não encontrado.");

            if (deliveryPerson.TypeOfDriversLicense != "A" && deliveryPerson.TypeOfDriversLicense != "A+B")
                throw new Exception("Somente entregadores habilitados na categoria A podem efetuar locação.");

            var dailyValue = dto.plano switch
            {
                7 => 30.0,
                15 => 28.0,
                30 => 22.0,
                45 => 20.0,
                50 => 18.0,
                _ => throw new Exception("Plano inválido. Use 7, 15, 30, 45 ou 50 dias.")
            };

            var startDate = DateTime.UtcNow.Date.AddDays(1);
            var expectedEnd = startDate.AddDays(dto.plano - 1);

            if (dto.data_previsao_termino.Date != expectedEnd.Date)
                throw new Exception($"Data de previsão de término inválida. Deve ser {expectedEnd:yyyy-MM-dd}.");

            var entity = new MotorcycleRental(
                deliveryPerson.Id,
                motorcycle.Id,
                startDate,
                dto.data_termino,
                dto.data_previsao_termino,
                dto.plano);

            await _motorcycleRentalRepository.AddAsync(entity);
            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
