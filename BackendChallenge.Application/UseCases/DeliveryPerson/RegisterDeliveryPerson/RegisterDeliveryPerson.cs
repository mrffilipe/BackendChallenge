using BackendChallenge.Application.Common;
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
        private readonly IFileStorage _fileStorage;

        public RegisterDeliveryPerson(
            IDeliveryPersonQueryRepository deliveryPersonQueryRepository,
            IDeliveryPersonRepository deliveryPersonRepository,
            IUnityOfWorkService unityOfWorkService,
            IFileStorage fileStorage)
        {
            _deliveryPersonQueryRepository = deliveryPersonQueryRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
            _unityOfWorkService = unityOfWorkService;
            _fileStorage = fileStorage;
        }

        public async Task ExecuteAsync(RegisterDeliveryPersonDto dto)
        {
            if (!DeliveryPerson.TypeOfDriversLicenseIsValid(dto.tipo_cnh))
                throw new Exception("Tipo da CNH inválido! Tente 'A', 'B' ou 'AB'");

            var result = await _deliveryPersonQueryRepository.GetByExternalIdAsync(dto.identificador);

            if (result != null)
                throw new Exception("Entregador com o mesmo identificador já registrado.");

            if (!ImageValidator.TryDecodeBase64(dto.imagem_cnh, out var bytes))
                throw new Exception("imagem_cnh inválida (base64).");

            var contentType = ImageValidator.DetectContentType(bytes)
                ?? throw new Exception("Formato de imagem não suportado. Somente PNG ou BMP.");

            var ext = contentType == "image/png" ? "png" : "bmp";
            var objectKey = $"cnh/{dto.identificador}/{Guid.NewGuid():N}.{ext}";

            using var stream = new MemoryStream(bytes);
            var storedKeyOrUrl = await _fileStorage.PutAsync(objectKey, stream, contentType);

            var entity = dto.ToEntity();
            entity.SetDriversLicenseImageUrl(storedKeyOrUrl);

            await _deliveryPersonRepository.AddAsync(entity);
            await _unityOfWorkService.SaveChangesAsync();
        }
    }
}
