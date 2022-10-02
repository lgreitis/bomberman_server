using Microsoft.AspNetCore.Mvc;
using Models.REST.Lobby.Get;
using Models.REST.Lobby.Init;
using Services.Services.Lobby;
using System.Net;
using static Models.REST.Lobby.Get.GetLobbiesResponse;

namespace GameServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LobbyController : ControllerBase
    {
        private ILobbyService _lobbyService;
        public LobbyController(ILobbyService lobbyService)
        {
            _lobbyService = lobbyService;
        }

        [HttpPost]
        [Route("")]
        public IActionResult InitLobby()
        {
            var lobbyId = _lobbyService.InitLobby();

            return StatusCode((int)HttpStatusCode.OK, new InitLobbyResponse
            {
                LobbyId = lobbyId,
            });
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetLobbies()
        {
            var lobbies = _lobbyService
                .GetQueryable()
                .Select(x => new Lobby
                { 
                    LobbyId = x.LobbyId, 
                    IsFull = x.LobbyUsers.Count >= 2 
                })
                .ToList();

            return StatusCode((int)HttpStatusCode.OK, new GetLobbiesResponse
            {
                Lobbies = lobbies,
            });
        }
    }
}
