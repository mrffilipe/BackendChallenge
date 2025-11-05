namespace BackendChallenge.Application.Services
{
    public interface IUnityOfWorkService
    {
        Task SaveChangesAsync();
    }
}
