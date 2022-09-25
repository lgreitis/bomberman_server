using GameServer.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.REST.Auth.Authenticate;
using System.Net;

namespace GameServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Authenticate(AuthRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                var token = ClientManager.Instance.Authenticate(request.Username);

                if (token != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, new AuthResponse
                    {
                        Token = token
                    });
                }
            }

            return StatusCode((int)HttpStatusCode.Unauthorized);
        }
    }
}