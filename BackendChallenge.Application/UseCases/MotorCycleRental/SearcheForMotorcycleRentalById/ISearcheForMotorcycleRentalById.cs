namespace BackendChallenge.Application.UseCases
{
    public interface ISearcheForMotorcycleRentalById
    {
        Task<MotorcycleRentalDto> ExecuteAsync(string id);
    }
}
