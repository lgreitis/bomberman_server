using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Behaviour.Lobby
{
    public class LobbyUser
    {
        public int UserId { get; set; }
        public WebSocketSharp.WebSocket Connection { get; set; }

        public LobbyUser(int userId, WebSocketSharp.WebSocket connection)
        {
            UserId = userId;
            Connection = connection;
        }
    }
}
