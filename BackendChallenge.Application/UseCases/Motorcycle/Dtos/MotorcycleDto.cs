namespace BackendChallenge.Application.UseCases
{
    public record MotorcycleDto : MotorcycleBaseDto
    {
        public MotorcycleDto(
            string identificador,
            int ano,
            string modelo,
            string placa) : base(identificador, ano, modelo, placa)
        {
        }
    }
}
