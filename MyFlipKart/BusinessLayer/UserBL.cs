using MyFlipKart.BusinessLayer.Interfaces;
using MyFlipKart.DTOS.Reponse;
using MyFlipKart.DTOS.Request;
using MyFlipKart.RepositoryLayer.Interface;
using Newtonsoft.Json;

namespace MyFlipKart.BusinessLayer
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _user;
        public UserBL(IUserRL user)
        {
            _user = user;
        }


        public async Task<UserRegResponse> UserRegistation(UserRegRequest request)
        {
            var result = await _user.RegisterUser(JsonConvert.SerializeObject(request));
            return JsonConvert.DeserializeObject<UserRegResponse>(result);
        }

        public async Task<LoginResponse> ValidateUser(LoginRequest request)
        {
            var user = new LoginResponse();
            bool result = await _user.ValidateUser(request);
            if (result != null)
            {
                user.Status = "Success";
                user.Message = "Loggin Success";
                user.Token = "";

            }
            else
            {
                user.Status = "Failed";
                user.Message = "Loggin Failed, Invalide Credentials";
                user.Token = "";
            }
            return user;
        }

        public Task<List<UserListResponse>?> FetchUserList(UserListRequest Request)
        {
            return _user.FetchUserList(Request);
        }
        public async Task<UserListResponse> FetchUserDetails(int id)
        {
            return await _user.FetchUserDetealis(id);


        }

        public async Task<UserListResponse?> DeleteUserById(int id)
        {
           return await _user.DeleteUser(id);
        }
    }
}
