using GameServer.Data;
using Models.WebSocket;
using Models.WebSocket.Request;
using Models.WebSocket.Response;
using Newtonsoft.Json;
using Services;
using Services.Services;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameServer.Behaviours
{
    public class LobbyBehaviour : WebSocketBehavior
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
                        case WebSocketCommandId.JoinLobby:
                            {
                                var requestData = GetRequestData<JoinLobbyRequest>(e.Data);

                                // TODO: Does lobby exists?

                                var userId = _userService
                                    .GetQueryable(x => x.LoginToken == requestData.Token)
                                    .Select(x => x.UserId)
                                    .SingleOrDefault();

                                if (userId <= 0)
                                {
                                    break;
                                }

                                LobbyManager.Instance.AddPlayerToLobby(requestData.LobbyId, userId);

                                if (LobbyManager.Instance.CanStartGame(requestData.LobbyId))
                                {
                                    Broadcast(new WebSocketResponse
                                    {
                                        ResponseId = WebSocketResponseId.StartGame,
                                        Data = new StartGameResponse
                                        {
                                            LobbyId = requestData.LobbyId
                                        }
                                    });
                                }

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
