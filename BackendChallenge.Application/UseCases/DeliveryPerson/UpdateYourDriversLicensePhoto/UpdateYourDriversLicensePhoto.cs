using BackendChallenge.Application.Services;

namespace BackendChallenge.Application.UseCases
{
    public class UpdateYourDriversLicensePhoto : IUpdateYourDriversLicensePhoto
    {
        private readonly IDeliveryPersonQueryRepository _deliveryPersonQueryRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;

        public UpdateYourDriversLicensePhoto(IDeliveryPersonQueryRepository deliveryPersonQueryRepository, IUnityOfWorkService unityOfWorkService)
        {
            _deliveryPersonQueryRepository = deliveryPersonQueryRepository;
            _unityOfWorkService = unityOfWorkService;
        }

        public async Task ExecuteAsync(string id, UpdateDriversLicensePhotoDto dto)
        {
            var result = await _deliveryPersonQueryRepository.GetByExternalIdAsync(id);

            if (result == null)
                throw new Exception("Entregador com o identificador informado não encontrado.");

            // enviar para o storage e recuperar nova url

            // atualizar a entidade

            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
