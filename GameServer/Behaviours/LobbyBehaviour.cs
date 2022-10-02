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
            IUserService? _userService;
            using (var serviceScope = Resolver.GetScope())
            {
                _userService = serviceScope.ServiceProvider.GetService<IUserService>();

                if (_userService == null)
                {
                    return;
                }
            }

            var requestCommand = GetRequestCommand(e.Data);
            if (!requestCommand.Success)
            {
                return;
            }

            switch(requestCommand.CommandId)
            {
                case WebSocketCommandId.JoinLobby:
                    {
                        var requestData = GetRequestData<JoinLobbyRequest>(e.Data);

                        if (requestData == null)
                        {
                            break;
                        }

                        if (string.IsNullOrEmpty(requestData.Token) || string.IsNullOrEmpty(requestData.LobbyId))
                        {
                            break;
                        }

                        var player = ClientManager.Instance.GetPlayer(requestData.Token);

                        if (player == null)
                        {
                            break;
                        }
                        
                        // todo assign player to a lobby

                        break;
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
            }
            catch
            {
            }

            return (false, null);
        }

        private T? GetRequestData<T>(string request) where T : class
        {
            try
            {
                var deserializedRequest = JsonConvert.DeserializeObject<WebSocketRequest>(request);

                if (deserializedRequest == null)
                {
                    return null;
                }

                // TODO: Use AutoMapper
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(deserializedRequest.Data));
            }
            catch
            {
            }

            return null;
        }
    }
}
