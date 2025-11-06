namespace BackendChallenge.Application.UseCases
{
    public interface IRegisterDeliveryPerson
    {
        Task ExecuteAsync(RegisterDeliveryPersonDto dto);
    }
}
