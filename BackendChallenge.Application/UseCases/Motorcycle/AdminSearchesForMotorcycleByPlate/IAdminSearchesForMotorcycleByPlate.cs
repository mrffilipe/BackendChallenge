namespace BackendChallenge.Application.UseCases
{
    public interface IAdminSearchesForMotorcycleByPlate
    {
        Task<IEnumerable<MotorcycleDto>> ExecuteAsync(string plate);
    }
}
