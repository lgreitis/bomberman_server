using Models.WebSocket;
using Models.WebSocket.Request;
using Models.WebSocket.Response;
using Newtonsoft.Json;
using Services.Services;
using Services;
using WebSocketSharp;
using WebSocketSharp.Server;
using GameServices.Singleton;
using Models.Behaviour.Game;
using GameServices.Models.ManagerModels;
using Newtonsoft.Json.Linq;
using GameServices.Command;

namespace GameServer.Behaviours
{
    public class GameBehaviour : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            using (var serviceScope = Resolver.GetScope())
            {
                var _userService = serviceScope.ServiceProvider.GetService<IUserService>();
                ArgumentNullException.ThrowIfNull(_userService);

                var requestCommand = GetRequestCommand(e.Data);

                if (!requestCommand.Success)
                {
                    return;
                }

                try
                {
                    switch (requestCommand.CommandId)
                    {
                        case WebSocketCommandId.Connect:
                            {
                                var requestData = GetRequestData<ConnectRequest>(e.Data);

                                GamesManager.Instance.InitializeGameManager(requestData.LobbyId);

                                var user = _userService
                                    .GetQueryable(x => x.LoginToken == requestData.Token)
                                    .Select(x => new
                                    {
                                        x.UserId,
                                        x.Username,
                                        x.LoginToken
                                    })
                                    .Single();

                                GamesManager.Instance.RegisterClient(requestData.LobbyId, new Client
                                {
                                    Username = user.Username,
                                    UserId = user.UserId,
                                    Token = user.LoginToken ?? string.Empty,
                                    IsConnected = true,
                                    SessionId = ID
                                });

                                var gameManager = GamesManager.Instance.GetGameManager(ID);

                                Send(new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Map,
                                    Data = new MapResponse
                                    {
                                        Map = gameManager.GetMapTiles()
                                    }
                                });

                                Broadcast(gameManager.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Players,
                                    Data = gameManager.GetPlayers()
                                });

                                break;
                            }
                        case WebSocketCommandId.Move:
                            {
                                var requestData = GetRequestData<MoveRequest>(e.Data);
                                var gameData = GamesManager.Instance.GetGameManager(ID);

                                var moveX = requestData.PositiveX != requestData.NegativeX;
                                var moveY = requestData.PositiveY != requestData.NegativeY;

                                var command = new MoveCommand(
                                    ID,
                                    moveX ? requestData.PositiveX : null,
                                    moveY ? requestData.PositiveY : null);

                                gameData.InvokeCommand(command);

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Players,
                                    Data = gameData.GetPlayers()
                                });

                                break;
                            }
                    }
                }
                catch
                {
                }
            }
        }

        private void Send(WebSocketResponse response)
        {
            var json = JsonConvert.SerializeObject(response);

            Send(json);
        }

        private void Broadcast(List<string> sessionIds, WebSocketResponse response)
        {
            var json = JsonConvert.SerializeObject(response);

            foreach (var sessionId in sessionIds.Distinct())
            {
                Sessions.SendTo(json, sessionId);
            }
        }

        private (bool Success, string? CommandId) GetRequestCommand(string request)
        {
            try
            {
                var deserializedRequest = JsonConvert.DeserializeObject<WebSocketRequest>(request);

                if (deserializedRequest != null
                    && WebSocketCommandId.AllCommands.Any(x => x == deserializedRequest.CommandId.ToUpper()))
                {
                    return (true, deserializedRequest.CommandId.ToUpper());
                }

                return (false, null);
            }
            catch
            {
                return (false, null);
            }
        }

        private T GetRequestData<T>(string request) where T : class, IRequestValidation
        {
            var deserializedRequest = JsonConvert.DeserializeObject<WebSocketRequest>(request);

            if (deserializedRequest == null)
            {
                throw new Exception("Could not deserialize request model.");
            }

            var requestData = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(deserializedRequest.Data));

            if (requestData == null || !requestData.IsModelValid())
            {
                throw new Exception("Request model is not valid.");
            }

            return requestData;
        }
    }
}
