using GameServer.Data;
using Models.WebSocket.Response;
using Models.WebSocket;
using Newtonsoft.Json;

namespace Models.Behaviour.Lobby;
public class Lobby : Subject
{
    public int LobbyId { get; set; }

    public override void Send()
    {
        var json = JsonConvert.SerializeObject(new WebSocketResponse
        {
            ResponseId = WebSocketResponseId.StartGame,
            Data = new StartGameResponse
            {
                LobbyId = LobbyId
            }
        });

        foreach (LobbyUser user in LobbyUsers)
        {
            user.Connection.Send(json);
        }
    }
}
