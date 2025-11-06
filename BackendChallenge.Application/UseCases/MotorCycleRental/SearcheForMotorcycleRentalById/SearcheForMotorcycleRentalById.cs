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
            var result = await _motorcycleRentalQueryRepository.GetByExternalIdAsync(id);

            if(result == null)
                throw new Exception("Locação com o identificador informado não encontrada.");

            //return new MotorcycleRentalDto();
            throw new NotImplementedException();
        }
    }
}
