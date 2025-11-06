using BackendChallenge.Application.Services;
using BackendChallenge.Domain.Repositories;

namespace BackendChallenge.Application.UseCases
{
    public class AdminRemovesMotorcycleById : IAdminRemovesMotorcycleById
    {
        private readonly IMotorcycleQueryRepository _motorcycleQueryRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;

        public AdminRemovesMotorcycleById(
            IMotorcycleQueryRepository motorcycleQueryRepository,
            IMotorcycleRepository motorcycleRepository,
            IUnityOfWorkService unityOfWorkService)
        {
            _motorcycleQueryRepository = motorcycleQueryRepository;
            _motorcycleRepository = motorcycleRepository;
            _unityOfWorkService = unityOfWorkService;
        }

        public async Task ExecuteAsync(string id)
        {
            var result = await _motorcycleQueryRepository.GetByExternalIdAsync(id);

            if (result == null)
                throw new Exception("Moto com o identificador informado não encontrada.");

            if (result.HasMotorcycleRentals())
                throw new Exception("Essa moto não pode ser removida, pois há locações.");

            _motorcycleRepository.Remove(result);

            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
