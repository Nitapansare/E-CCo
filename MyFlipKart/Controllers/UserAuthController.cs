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
    }
}
