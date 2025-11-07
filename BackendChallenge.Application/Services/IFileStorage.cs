namespace BackendChallenge.Application.Services
{
    public interface IFileStorage
    {
        Task<string> PutAsync(string objectKey, Stream content, string contentType, CancellationToken ct = default);
    }

}
