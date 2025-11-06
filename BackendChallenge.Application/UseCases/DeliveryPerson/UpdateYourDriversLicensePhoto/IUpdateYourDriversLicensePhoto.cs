namespace BackendChallenge.Application.UseCases
{
    public interface IUpdateYourDriversLicensePhoto
    {
        Task ExecuteAsync(string id, UpdateDriversLicensePhotoDto dto);
    }
}
