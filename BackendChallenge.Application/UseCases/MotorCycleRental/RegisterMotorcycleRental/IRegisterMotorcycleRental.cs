namespace BackendChallenge.Application.UseCases
{
    public interface IRegisterMotorcycleRental
    {
        Task ExecuteAsync(RegisterMotorcycleRentalDto dto);
    }
}
