using GameServer.Data;
using Models.Game.Client;
using Models.WebSocket;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameServer.Behaviours
{
    public class GameBehaviour : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var data = e.Data.Split(';');

            switch(data[0])
            {
                case "LOGIN":
                    {
                        var username = ClientManager.Instance.GetUsername(data[1]);
                        var response = new LoginResponse
                        {
                            Type = "LOGIN",
                            IsSuccess = username != null,
                            Username = username
                        };
                        var json = JsonConvert.SerializeObject(response);

                        Send(json);
                        break;
                    }
                case "GETGAME":
                    {
                        var clientInfo = ClientManager.Instance.GetUserData();

                        Sessions.Broadcast(clientInfo);
                        break;
                    }
            }
        }
    }
}
