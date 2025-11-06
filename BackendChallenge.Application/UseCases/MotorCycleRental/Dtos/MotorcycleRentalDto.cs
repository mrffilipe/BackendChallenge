namespace BackendChallenge.Application.UseCases
{
    public record MotorcycleRentalDto : MotorcycleRentalBaseDto
    {
        public MotorcycleRentalDto(
            string identificador,
            int valor_diaria,
            string entregador_id,
            string moto_id,
            DateTime data_inicio,
            DateTime data_termino,
            DateTime data_previsao_termino,
            DateTime data_devolucao) : base(entregador_id, moto_id, data_inicio, data_termino, data_previsao_termino)
        {
        }
    }
}
