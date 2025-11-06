namespace BackendChallenge.Application.UseCases
{
    public record RegisterMotorcycleRentalDto(
        string entregador_id,
        string moto_id,
        DateTime data_inicio,
        DateTime data_termino,
        DateTime data_previsao_termino,
        int plano) : MotorcycleRentalBaseDto(entregador_id, moto_id, data_inicio, data_termino, data_previsao_termino);
}
