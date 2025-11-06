namespace BackendChallenge.Application.UseCases
{
    public interface IAdminRemovesMotorcycleById
    {
        Task ExecuteAsync(string id);
    }
}
