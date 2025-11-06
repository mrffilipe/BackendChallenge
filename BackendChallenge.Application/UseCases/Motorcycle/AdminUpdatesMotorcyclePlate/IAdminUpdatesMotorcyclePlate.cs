using BackendChallenge.Application.Common;

namespace BackendChallenge.Application.UseCases
{
    public interface IAdminUpdatesMotorcyclePlate
    {
        Task<ResponseToTheRequest> ExecuteAsync(string id, UpdateMotorcyclePlateDto dto);
    }
}
