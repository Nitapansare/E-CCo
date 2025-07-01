using Dapper;
using MyFlipKart.DTOS.Reponse;
using MyFlipKart.DTOS.Request;
using MyFlipKart.RepositoryLayer.Interface;
using Newtonsoft.Json;
using System.Data;

namespace MyFlipKart.RepositoryLayer
{
    public class UserRL : IUserRL
    {
        private readonly IDbConnection _db;
        public UserRL(IDbConnection db)
        {
            _db = db;
        }



        public async Task<string> RegisterUser(string request)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Json", request);

            string jsonResult = await _db.QueryFirstOrDefaultAsync<string>(
                "dbo.sp_RegisterUser",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return jsonResult; // already a JSON string
        }

        public async Task<bool> ValidateUser(LoginRequest request)
        {
            const string sql = @"Select * from dbo.tbl_User where Email_Id = @Email_Id and Password = @Password";

            var result = await _db.QueryFirstOrDefaultAsync(sql, new { Email_Id = request.Email_Id, Password = request.Password });

            return result != null ? true : false;
        }

        public async Task<List<UserListResponse>?> FetchUserList(UserListRequest Request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageNumber", Request.PageNumber);
            parameters.Add("@FilterBy", Request.FilterBy);
            parameters.Add("@FilterValue", Request.FilterValue);
            string jsonResult = await _db.QueryFirstOrDefaultAsync<string>("dbo.GetUserList", parameters, commandType: CommandType.StoredProcedure);

            if (string.IsNullOrWhiteSpace(jsonResult))
                return new List<UserListResponse>();

            return JsonConvert.DeserializeObject<List<UserListResponse>>(jsonResult);
        }

        public async Task<UserListResponse> FetchUserDetealis(int id)
        {
            const string sql = @"Select * from dbo.tbl_User where UserId = @UserId";

            var result = await _db.QueryFirstOrDefaultAsync(sql, new { UserId = id });
            var resp = new UserListResponse();
            if (result != null)
            {
                resp.Name = result.Name;
                resp.Address = result.Address;
                resp.phone = result.Phone;
                resp.CreatedDate = result.CreatedDate;
                resp.Email_Id = result.Email_Id;
            }
            return resp;
        }

        public async Task<UserListResponse?> DeleteUser(int id)
        {
            const string sql = @"
        UPDATE tbl_User
        SET IsDelete = 1
        OUTPUT 
            INSERTED.UserId,
            INSERTED.Name,
            INSERTED.Email_Id,
            INSERTED.Phone,
            INSERTED.Address,
            INSERTED.CreatedDate
        WHERE UserId = @UserId";

            var deletedUser = await _db.QueryFirstOrDefaultAsync<UserListResponse>(sql, new { UserId = id });

            return deletedUser;
        }
    }
  
}