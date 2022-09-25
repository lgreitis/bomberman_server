using Models.Game.Client;
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
            private List<Client> Clients { get; set; }

            public ClientManagerInstance()
            {
                this.Clients = new List<Client>();
            }

            public string? Authenticate(string username)
            {
                if (this.Clients.Any(x => x.Username.ToLower() == username.ToLower()))
                {
                    return null;
                }

                var client = new Client
                {
                    Username = username,
                    Token = Guid.NewGuid().ToString(),
                };

                this.Clients.Add(client);

                return client.Token;
            }

            public string? GetUsername(string token)
            {
                var client = this.Clients.SingleOrDefault(x => x.Token == token);

                if (client == null)
                {
                    return null;
                }

                return client.Username;
            }

            public string GetUserData()
            {
                var clients = this.Clients
                    .Select(x => new PlayerInfo
                    {
                        Username = x.Username,
                        PositionX = x.PositionX,
                        PositionY = x.PositionY
                    })
                    .ToList();

                return JsonConvert.SerializeObject(new PlayerInfoResponse
                {
                    Type = "PLAYERS",
                    Players = clients
                });
            }
        }
    }
}
