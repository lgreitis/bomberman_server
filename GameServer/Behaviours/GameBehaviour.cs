using GameServer.Data;
using Models.WebSocket;
using Models.WebSocket.Request;
using Models.WebSocket.Response;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameServer.Behaviours
{
    public class GameBehaviour : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var requestCommand = GetRequestCommand(e.Data);

            if (!requestCommand.Success)
            {
                return;
            }

            switch (requestCommand.CommandId)
            {
                case WebSocketCommandId.Connect:
                    {
                        var requestData = GetRequestData<ConnectRequest>(e.Data);

                        if (requestData == null)
                        {
                            break;
                        }

                        ClientManager.Instance.AddPlayer(new Models.Game.Player
                        {
                            Username = requestData.Token,
                            Token = requestData.Token
                        });

                        break;
                    }
                case WebSocketCommandId.Move:
                    {
                        var requestData = GetRequestData<MoveRequest>(e.Data);

                        if (requestData == null)
                        {
                            break;
                        }

                        ClientManager.Instance.MovePlayer(
                            requestData.Token,
                            requestData.PositiveX,
                            requestData.NegativeX,
                            requestData.PositiveY,
                            requestData.NegativeY);

                        break;
                    }
            }

            Broadcast(new WebSocketResponse
            {
                ResponseId = WebSocketResponseId.GameUpdate,
                Data = new GameUpdateResponse
                {
                    Players = ClientManager.Instance.GetPlayers()
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
            }
            catch
            {
            }

            return (false, null);
        }

        private T? GetRequestData<T>(string request) where T: class
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
