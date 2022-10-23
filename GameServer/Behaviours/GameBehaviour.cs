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

                                Send(new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Map,
                                    Data = new MapResponse
                                    {
                                        Map = JsonConvert.DeserializeObject(GamesManager.Instance.GetMap(requestData.LobbyId))
                                    }
                                });

                                Broadcast(GamesManager.Instance.GetSessionIds(requestData.LobbyId), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Players,
                                    Data = JsonConvert.DeserializeObject(GamesManager.Instance.GetPlayers(requestData.LobbyId))
                                });

                                break;
                            }
                        case WebSocketCommandId.Move:
                            {
                                var requestData = GetRequestData<MoveRequest>(e.Data);

                                GamesManager.Instance.MovePlayer(
                                    requestData.Token,
                                    requestData.PositiveX,
                                    requestData.NegativeX,
                                    requestData.PositiveY,
                                    requestData.NegativeY);

                                var lobbyId = GamesManager.Instance.GetLobbyId(requestData.Token);
                                Broadcast(GamesManager.Instance.GetSessionIds(lobbyId), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Players,
                                    Data = JsonConvert.DeserializeObject(GamesManager.Instance.GetPlayers(lobbyId))
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

        private void Broadcast(WebSocketResponse response)
        {
            var json = JsonConvert.SerializeObject(response);

            Sessions.Broadcast(json);
        }

        private void Broadcast(List<string> sessionIds, WebSocketResponse response)
        {
            var json = JsonConvert.SerializeObject(response);

            foreach (var sessionId in sessionIds)
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
