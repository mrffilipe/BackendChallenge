namespace BackendChallenge.Application.UseCases
{
    public interface IAdminUpdatesMotorcyclePlate
    {
        Task ExecuteAsync(string id, UpdateMotorcyclePlateDto dto);
    }
}
