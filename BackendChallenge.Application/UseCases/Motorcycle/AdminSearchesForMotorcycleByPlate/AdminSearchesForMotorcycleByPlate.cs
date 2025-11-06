namespace BackendChallenge.Application.UseCases
{
    public class AdminSearchesForMotorcycleByPlate : IAdminSearchesForMotorcycleByPlate
    {
        private readonly IMotorcycleQueryRepository _motorcycleQueryRepository;

        public AdminSearchesForMotorcycleByPlate(IMotorcycleQueryRepository motorcycleQueryRepository)
        {
            _motorcycleQueryRepository = motorcycleQueryRepository;
        }

        public async Task<IEnumerable<MotorcycleDto>> ExecuteAsync(string plate)
        {
            var result = await _motorcycleQueryRepository.GetByPlateAsync(plate);

            if (result == null)
                throw new Exception("Moto com a placa informada não encontrada.");

            /*
             * De a especificação do teste "A placa é um dado único e não pode se repetir." e no contrato to Swagger,
             * espera um retorno de várias motos, mas a placa é um dado único, portanto, não há como haver mais de uma moto com a mesma placa
             */
            return (IEnumerable<MotorcycleDto>)result.ToDto();
        }
    }
}
