namespace BackendChallenge.Application.UseCases
{
    public class AdminSearchesForMotorcycleById : IAdminSearchesForMotorcycleById
    {
        private readonly IMotorcycleQueryRepository _motorcycleQueryRepository;

        public AdminSearchesForMotorcycleById(IMotorcycleQueryRepository motorcycleQueryRepository)
        {
            _motorcycleQueryRepository = motorcycleQueryRepository;
        }

        public async Task<MotorcycleDto> ExecuteAsync(string id)
        {
            var result = await _motorcycleQueryRepository.GetByExternalIdAsync(id);

            if(result == null)
                throw new Exception("Moto com o identificador informado não encontrada.");

            return result.ToDto();
        }
    }
}
