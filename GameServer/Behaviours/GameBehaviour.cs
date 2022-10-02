using GameServer.Data;
using Models.WebSocket;
using Models.WebSocket.Request;
using Models.WebSocket.Response;
using Newtonsoft.Json;
using Services.Services;
using Services;
using WebSocketSharp;
using WebSocketSharp.Server;

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

                                GameManager.Instance.InitializeGame(requestData.LobbyId);

                                var user = _userService
                                    .GetQueryable(x => x.LoginToken == requestData.Token)
                                    .Select(x => new
                                    {
                                        x.UserId,
                                        x.Username,
                                        x.LoginToken
                                    })
                                    .Single();

                                GameManager.Instance.RegisterPlayer(requestData.LobbyId, user.UserId, user.LoginToken, user.Username);

                                break;
                            }
                        case WebSocketCommandId.Move:
                            {
                                var requestData = GetRequestData<MoveRequest>(e.Data);

                                GameManager.Instance.MovePlayer(
                                    requestData.Token,
                                    requestData.PositiveX,
                                    requestData.NegativeX,
                                    requestData.PositiveY,
                                    requestData.NegativeY);

                                break;
                            }
                    }
                }
                catch
                {
                }
            }

            Broadcast(new WebSocketResponse
            {
                ResponseId = WebSocketResponseId.GameUpdate,
                Data = new GameUpdateResponse
                {
                    Games = GameManager.Instance.GetGamesData()
                }
            });
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
