using Microsoft.AspNetCore.Mvc;
using Models.REST.User.Login;
using Models.REST.User.Register;
using Services.Services;
using System.Net;

namespace GameServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            var token = _userService.Authenticate(request.Username, request.Password);

            return StatusCode((int)HttpStatusCode.OK, new LoginResponse
            {
                Success = !string.IsNullOrWhiteSpace(token),
                Token = token
            });
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var status = false;

            if (!string.IsNullOrWhiteSpace(request.Username)
                && !string.IsNullOrEmpty(request.Password))
            {
                status = _userService.RegisterUser(request.Username, request.Password);
            }

            return StatusCode((int)HttpStatusCode.OK, new RegisterResponse
            {
                Success = status
            });
        }
    }
}