using Models.Game;
using Models.WebSocket;
using Newtonsoft.Json;

namespace GameServer.Data
{
    public static class ClientManager
    {
        private static readonly object _instanceLock = new object();
        private static ClientManagerInstance? _instance;
        public static ClientManagerInstance Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientManagerInstance();
                    }

                    return _instance;
                }
            }
        }

        public class ClientManagerInstance
        {
            private List<Player> Players { get; set; }
            private readonly static object _lock = new object();

            public ClientManagerInstance()
            {
                this.Players = new List<Player>();
            }

            public void AddPlayer(Player player)
            {
                lock (_lock)
                {
                    if (this.Players.Any(x => x.Username == player.Username
                                              || x.Token == player.Token))
                    {
                        return;
                    }

                    this.Players.Add(player);
                }
            }

            public List<Player> GetPlayers()
            {
                var players = new List<Player>();

                lock (_lock)
                {
                    players.AddRange(this.Players);
                }

                return players;
            }

            public void MovePlayer(string token, bool posX, bool negX, bool posY, bool negY)
            {
                lock (_lock)
                {
                    var player = this.Players.FirstOrDefault(x => x.Username == token);
                    
                    if (player == null)
                    {
                        return;
                    }

                    if (posX != negX && (posX || negX))
                    {
                        player.LocationX += posX ? 10 : -10;
                    }

                    if (posY != negY && (posY || negY))
                    {
                        player.LocationY += posY ? 10 : -10;
                    }
                }
            }
        }
    }
}
