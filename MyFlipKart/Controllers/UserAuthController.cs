using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyFlipKart.BusinessLayer.Interfaces;
using MyFlipKart.DomainModels;
using MyFlipKart.DTOS.Reponse;
using MyFlipKart.DTOS.Request;
using System.Data;
using System.Data.SqlClient;

namespace MyFlipKart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserBL _userBL;
        public UserAuthController(IUserBL userBL)
        {
            _userBL = userBL;

        }

        [HttpPost("register-json")]
        public async Task<IActionResult> RegisterUser(UserRegRequest request)
        {
            if (request == null)
                return BadRequest(ApiResponse<string>.Fail("Invalid request", "INVALID_INPUT"));

            var response = await _userBL.UserRegistation(request);

            if (response.Status == "Failed")
            {
                return BadRequest(ApiResponse<UserRegResponse>.Fail(
                    response.Message ?? "Registration failed",
                    "REGISTRATION_FAILED",
                    400
                ));
            }

            return Ok(ApiResponse<UserRegResponse>.Success(
                response,
                response.Message ?? "User registered successfully",
                "REGISTRATION_SUCCESS"
            ));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginRequest login)
        {
            if (login == null)
            {
                return BadRequest(ApiResponse<string>.Fail("Invalid request", "INVALID_INPUT"));
            }
            var response = await _userBL.ValidateUser(login);

            if (response.Status == "Failed")
            {
                return BadRequest(ApiResponse<LoginResponse>.Fail(
                    response.Message ?? "Registration failed",
                    "LOGIN_FAILED",
                    400
                ));
            }

            return Ok(ApiResponse<LoginResponse>.Success(
                response,
                response.Message ?? "User login successfully",
                "LOGIN_SUCCESS"
            ));
        }

        [HttpPost("fetch_userlist")]
        public async Task<IActionResult> UserList(UserListRequest Request)
        {
            try
            {
                var response = await _userBL.FetchUserList(Request);
                return Ok(ApiResponse<List<UserListResponse>>.Success(
                    response
                ));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("GetUserDetails/{id}")]
        public async Task<IActionResult> UserLisGetuserDetails(int id)
        {
            try
            {
                var response = await _userBL.FetchUserDetails(id);
                return Ok(ApiResponse<UserListResponse>.Success(
                    response
                ));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpDelete("delete_user/{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            try
            {
                var response = await _userBL.DeleteUserById(id);
                if (response != null)
                    return Ok(ApiResponse<UserListResponse>.Success(
                        response, "User Deleted Success."
                    ));
                else
                    return BadRequest(ApiResponse<LoginResponse>.Fail(
                    "User Delete failed.",
                    "LOGIN_FAILED",
                    400
                ));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}



