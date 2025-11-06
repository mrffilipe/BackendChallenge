namespace BackendChallenge.Application.UseCases
{
    public interface IAdminSearchesForMotorcycleById
    {
        Task<MotorcycleDto> ExecuteAsync(string id);
    }
}
