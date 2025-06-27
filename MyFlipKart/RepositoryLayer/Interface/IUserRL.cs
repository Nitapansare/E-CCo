using MyFlipKart.DTOS.Reponse;
using MyFlipKart.DTOS.Request;

namespace MyFlipKart.RepositoryLayer.Interface
{
    public interface IUserRL
    {
        Task<string> RegisterUser(string Request);
        Task<bool> ValidateUser(LoginRequest request);
        Task<List<UserListResponse>?> FetchUserList(UserListRequest req);
    }
}
