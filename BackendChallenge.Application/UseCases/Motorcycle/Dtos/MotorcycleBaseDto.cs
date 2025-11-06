namespace BackendChallenge.Application.UseCases
{
    public record MotorcycleBaseDto(
        string identificador,
        int ano,
        string modelo,
        string placa);
}
