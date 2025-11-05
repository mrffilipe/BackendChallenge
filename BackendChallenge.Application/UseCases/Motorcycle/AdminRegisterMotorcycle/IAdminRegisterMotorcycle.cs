namespace BackendChallenge.Application.UseCases
{
    public interface IAdminRegisterMotorcycle
    {
        Task ExecuteAsync(RegisterMotorcycleDto dto);
    }
}
