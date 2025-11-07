using BackendChallenge.Application.Services;
using BackendChallenge.Domain.Common;
using BackendChallenge.Domain.Repositories;
using System.Text.Json;

namespace BackendChallenge.Application.UseCases
{
    public class AdminRegisterMotorcycle : IAdminRegisterMotorcycle
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMotorcycleQueryRepository _motorcycleQueryRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;
        private readonly IMessageBus _bus;

        public AdminRegisterMotorcycle(
            IMotorcycleRepository motorcycleRepository,
            IMotorcycleQueryRepository motorcycleQueryRepository,
            IUnityOfWorkService unityOfWorkService,
            IMessageBus bus)
        {
            _motorcycleRepository = motorcycleRepository;
            _motorcycleQueryRepository = motorcycleQueryRepository;
            _unityOfWorkService = unityOfWorkService;
            _bus = bus;
        }

        public async Task ExecuteAsync(RegisterMotorcycleDto dto)
        {
            var result = await _motorcycleQueryRepository.GetByExternalIdAsync(dto.identificador);

            if (result != null)
                throw new Exception("Moto com o mesmo identificador já registrada.");

            var entity = dto.ToEntity();
            await _motorcycleRepository.AddAsync(entity);
            await _unityOfWorkService.SaveChangesAsync();

            var evt = new MotorcycleCreated(
                entity.Id,
                entity.ExternalId,
                entity.Year,
                entity.Model,
                entity.Plate,
                entity.CreatedAt);

            // Considerar Outbox para consistência
            var payload = JsonSerializer.Serialize(evt);
            await _bus.PublishAsync("backendchallenge", "motorcycle.created.v1", payload);
        }
    }
}
