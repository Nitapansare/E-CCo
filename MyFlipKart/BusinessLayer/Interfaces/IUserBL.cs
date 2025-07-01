using MyFlipKart.DTOS.Reponse;
using MyFlipKart.DTOS.Request;

namespace MyFlipKart.BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        Task<UserRegResponse> UserRegistation(UserRegRequest request);
        Task<LoginResponse> ValidateUser(LoginRequest request);
        Task<List<UserListResponse>?> FetchUserList(UserListRequest request);
        Task<UserListResponse> FetchUserDetails(int id);
        Task<UserListResponse?> DeleteUserById(int id);

    }
}
