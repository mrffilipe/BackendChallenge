namespace BackendChallenge.Application.UseCases
{
    public record RegisterMotorcycleDto(
        string identificador,
        int ano,
        string modelo,
        string placa);
}
