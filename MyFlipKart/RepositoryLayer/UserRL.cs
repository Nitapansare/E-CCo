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

        public async Task<List<UserListResponse>?> FetchUserList()
        {
            var parameters = new DynamicParameters();

            string jsonResult = await _db.QueryFirstOrDefaultAsync<string>("dbo.GetUserList", null, commandType: CommandType.StoredProcedure);

            if (string.IsNullOrWhiteSpace(jsonResult))
                return new List<UserListResponse>();

            return JsonConvert.DeserializeObject<List<UserListResponse>>(jsonResult);
        }

        
       }
    }

