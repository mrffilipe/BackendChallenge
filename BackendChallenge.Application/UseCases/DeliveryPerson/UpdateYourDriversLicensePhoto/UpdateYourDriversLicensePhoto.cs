using BackendChallenge.Application.Common;
using BackendChallenge.Application.Services;

namespace BackendChallenge.Application.UseCases
{
    public class UpdateYourDriversLicensePhoto : IUpdateYourDriversLicensePhoto
    {
        private readonly IDeliveryPersonQueryRepository _deliveryPersonQueryRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;
        private readonly IFileStorage _fileStorage;

        public UpdateYourDriversLicensePhoto(
            IDeliveryPersonQueryRepository deliveryPersonQueryRepository,
            IUnityOfWorkService unityOfWorkService,
            IFileStorage fileStorage)
        {
            _deliveryPersonQueryRepository = deliveryPersonQueryRepository;
            _unityOfWorkService = unityOfWorkService;
            _fileStorage = fileStorage;
        }

        public async Task ExecuteAsync(string id, UpdateDriversLicensePhotoDto dto)
        {
            var result = await _deliveryPersonQueryRepository.GetByExternalIdAsync(id);

            if (result == null)
                throw new Exception("Entregador com o identificador informado não encontrado.");

            if (!ImageValidator.TryDecodeBase64(dto.imagem_cnh, out var bytes))
                throw new Exception("imagem_cnh inválida (base64).");

            var contentType = ImageValidator.DetectContentType(bytes)
                ?? throw new Exception("Formato de imagem não suportado. Somente PNG ou BMP.");

            var ext = contentType == "image/png" ? "png" : "bmp";
            var objectKey = $"cnh/{id}/{Guid.NewGuid():N}.{ext}";

            using var stream = new MemoryStream(bytes);
            var storedKeyOrUrl = await _fileStorage.PutAsync(objectKey, stream, contentType);

            result.SetDriversLicenseImageUrl(storedKeyOrUrl);

            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
