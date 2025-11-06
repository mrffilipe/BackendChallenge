using BackendChallenge.Application.Services;

namespace BackendChallenge.Application.UseCases
{
    public class AdminUpdatesMotorcyclePlate : IAdminUpdatesMotorcyclePlate
    {
        private readonly IMotorcycleQueryRepository _motorcycleQueryRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;

        public AdminUpdatesMotorcyclePlate(IMotorcycleQueryRepository motorcycleQueryRepository, IUnityOfWorkService unityOfWorkService)
        {
            _motorcycleQueryRepository = motorcycleQueryRepository;
            _unityOfWorkService = unityOfWorkService;
        }

        public async Task ExecuteAsync(string id, UpdateMotorcyclePlateDto dto)
        {
            var result = await _motorcycleQueryRepository.GetByExternalIdAsync(id);

            if (result == null)
                throw new Exception("Moto com o identificador informado não encontrada.");

            result.UpdatePlate(dto.placa);

            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
