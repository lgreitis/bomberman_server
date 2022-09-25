using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.WebSocket
{
    public class PlayerInfoResponse
    {
        public string Type { get; set; }
        public List<PlayerInfo> Players { get; set; }
    }

    public class PlayerInfo
    {
        public string Username { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }
}
