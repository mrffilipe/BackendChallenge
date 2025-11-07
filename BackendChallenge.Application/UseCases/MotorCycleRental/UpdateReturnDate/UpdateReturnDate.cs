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
            var rental = await _motorcycleRentalQueryRepository.GetByExternalIdAsync(id)
                ?? throw new Exception("Locação com o identificador informado não encontrada.");

            var plan = rental.RentalPlan;
            var baseDaily = plan switch
            {
                7 => 30.0,
                15 => 28.0,
                30 => 22.0,
                45 => 20.0,
                50 => 18.0,
                _ => throw new Exception("Plano inválido.")
            };

            var start = rental.StartDate.Date;
            var end = dto.data_devolucao.Date;
            var expectedEnd = rental.EstimatedEndDate.Date;

            var totalDays = (end - start).Days + 1;
            if (totalDays < 1) throw new Exception("Data de devolução inválida.");

            double total = 0;

            if (end < expectedEnd)
            {
                // devolveu antes (multa sobre diárias não utilizadas)
                var usedDays = (end - start).Days + 1;
                var notUsedDays = plan - usedDays;
                var baseValue = usedDays * baseDaily;

                var penaltyRate = plan switch
                {
                    7 => 0.20,
                    15 => 0.40,
                    _ => 0.0
                };

                var penalty = notUsedDays * baseDaily * penaltyRate;
                total = baseValue + penalty;
            }
            else if (end > expectedEnd)
            {
                // devolveu depois (diária + 50 por excedente)
                var usedDays = (end - start).Days + 1;
                var extraDays = (end - expectedEnd).Days;
                total = (plan * baseDaily) + (extraDays * (baseDaily + 50));
            }
            else
            {
                // devolveu no dia certo
                total = plan * baseDaily;
            }

            await _unityOfWorkService.SaveChangesAsync();

            return new ResponseToTheRequest(
                $"Data de devolução informada com sucesso. Valor total: R${total:F2}");
        }
    }
}
