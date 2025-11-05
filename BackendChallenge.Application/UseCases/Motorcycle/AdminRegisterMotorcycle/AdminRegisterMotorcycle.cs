using BackendChallenge.Application.Services;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Application.UseCases
{
    public class AdminRegisterMotorcycle : IAdminRegisterMotorcycle
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMotorcycleQueryRepository _motorcycleQueryRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;

        public AdminRegisterMotorcycle(
            IMotorcycleRepository motorcycleRepository,
            IMotorcycleQueryRepository motorcycleQueryRepository,
            IUnityOfWorkService unityOfWorkService)
        {
            _motorcycleRepository = motorcycleRepository;
            _motorcycleQueryRepository = motorcycleQueryRepository;
            _unityOfWorkService = unityOfWorkService;
        }

        public async Task ExecuteAsync(RegisterMotorcycleDto dto)
        {
            var result = _motorcycleQueryRepository.GetByExternalIdAsync(dto.identificador);

            if (result != null)
                throw new Exception("Moto com o mesmo identificador já registrada.");

            await _motorcycleRepository.AddAsync(dto.ToEntity());

            // disparar evento

            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
