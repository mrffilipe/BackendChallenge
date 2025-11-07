namespace BackendChallenge.Application.UseCases
{
    public class SearcheForMotorcycleRentalById : ISearcheForMotorcycleRentalById
    {
        private readonly IMotorcycleRentalQueryRepository _motorcycleRentalQueryRepository;

        public SearcheForMotorcycleRentalById(IMotorcycleRentalQueryRepository motorcycleRentalQueryRepository)
        {
            _motorcycleRentalQueryRepository = motorcycleRentalQueryRepository;
        }

        public async Task<MotorcycleRentalDto> ExecuteAsync(string id)
        {
            var rental = await _motorcycleRentalQueryRepository.GetByExternalIdAsync(id)
                ?? throw new Exception("Locação com o identificador informado não encontrada.");

            // valor diário baseado no plano
            var dailyValue = rental.RentalPlan switch
            {
                7 => 30.0,
                15 => 28.0,
                30 => 22.0,
                45 => 20.0,
                50 => 18.0,
                _ => 0.0
            };

            return new MotorcycleRentalDto(
                identificador: rental.ExternalId,
                valor_diaria: dailyValue,
                entregador_id: rental.DeliveryPerson.ExternalId,
                moto_id: rental.Motorcycle.ExternalId,
                data_inicio: rental.StartDate,
                data_termino: rental.EndDate,
                data_previsao_termino: rental.EstimatedEndDate,
                data_devolucao: rental.EndDate
            );
        }
    }
}
