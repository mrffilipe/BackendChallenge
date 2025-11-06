using BackendChallenge.Application.Common;

namespace BackendChallenge.Application.UseCases
{
    public interface IUpdateReturnDate
    {
        Task<ResponseToTheRequest> ExecuteAsync(string id, UpdateReturnDateDto dto);
    }
}
