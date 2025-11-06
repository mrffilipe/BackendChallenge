namespace BackendChallenge.Application.UseCases
{
    public record RegisterDeliveryPersonDto : DeliveryPersonBaseDto
    {
        public RegisterDeliveryPersonDto(
            string identificador,
            string nome,
            string cnpj,
            DateTime data_nascimento,
            string numero_cnh,
            string tipo_cnh,
            string imagem_cnh) : base(identificador, nome, cnpj, data_nascimento, numero_cnh, tipo_cnh, imagem_cnh)
        {
        }
    }
}
