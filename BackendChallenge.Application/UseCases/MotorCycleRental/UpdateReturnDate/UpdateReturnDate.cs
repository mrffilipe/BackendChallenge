using BackendChallenge.Application.Common;
using BackendChallenge.Application.Services;

namespace BackendChallenge.Application.UseCases
{
    public class UpdateReturnDate : IUpdateReturnDate
    {
        private readonly IMotorcycleRentalQueryRepository _motorcycleRentalQueryRepository;
        private readonly IUnityOfWorkService _unityOfWorkService;

        public UpdateReturnDate(IMotorcycleRentalQueryRepository motorcycleRentalQueryRepository, IUnityOfWorkService unityOfWorkService)
        {
            _motorcycleRentalQueryRepository = motorcycleRentalQueryRepository;
            _unityOfWorkService = unityOfWorkService;
        }

        public async Task<ResponseToTheRequest> ExecuteAsync(string id, UpdateReturnDateDto dto)
        {
            var result = await _motorcycleRentalQueryRepository.GetByExternalIdAsync(id);

            if (result == null)
                throw new Exception("Locação com o identificador informado não encontrada.");

            // finalizar a lógica

            await _unityOfWorkService.SaveChangesAsync();

            return new ResponseToTheRequest("Data de devolução informada com sucesso");
        }
    }
}
