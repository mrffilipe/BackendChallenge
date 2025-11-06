namespace BackendChallenge.Application.UseCases
{
    public record MotorcycleRentalDto(
            string identificador,
            double valor_diaria,
            string entregador_id,
            string moto_id,
            DateTime data_inicio,
            DateTime data_termino,
            DateTime data_previsao_termino,
            DateTime data_devolucao);
}
