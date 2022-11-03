using GameServices.Builders;
using GameServices.Command;
using GameServices.Enums;
using GameServices.Factories.MapFactory;
using GameServices.Interfaces;
using GameServices.Models.MapModels;
using GameServices.Models.PlayerModels;

namespace GameServices.Models.ManagerModels
{
    public class GameManager
    {
        public readonly object Lock = new object();
        public int LobbyId { get; private set; }
        public Map? Map { get; private set; }
        public Level Level { get; private set; }
        public List<Client> Clients { get; private set; }

        public GameManager(int lobbyId)
        {
            LobbyId = lobbyId;
            Level = Level.First;
            Clients = new List<Client>();

            InitializeLevel();
        }

        private void InitializeLevel()
        {
            var director = new MapDirector
            {
                Builder = new MapBuilder(MapFactory.GetMapFactory(Level))
            };

            Map = director.BuildMap();

            switch (Level)
            {
                case Level.First:
                    Level = Level.Second;
                    break;
                case Level.Second:
                    Level = Level.Third;
                    break;
                case Level.Third:
                    Level = Level.First;
                    break;
            }
        }

        public void RegisterClient(Client client)
        {
            lock (Lock)
            {
                var registeredClient = Clients.FirstOrDefault(x => x.Token == client.Token);

                if (registeredClient == null)
                {
                    Clients.Add(client);

                    return;
                }

                registeredClient.SessionId = client.SessionId;
            }
        }

        public List<string> GetSessionIds()
        {
            lock (Lock)
            {
                return Clients.Select(x => x.SessionId).ToList();
            }
        }

        public List<object> GetPlayers()
        {
            lock (Lock)
            {
                return Clients
                    .Select(x => new
                    {
                        Username = x.Username,
                        UserId = x.UserId,
                        Token = x.Token,
                        X = x.X,
                        Y = x.Y
                    })
                    .Select(x => (object)x)
                    .ToList();
            }
        }

        public Client GetPlayer(string sessionId)
        {
            lock (Lock)
            {
                return Clients.First(x => x.SessionId == sessionId);
            }
        }

        public List<IMapTile> GetMapTiles()
        {
            lock (Lock)
            {
                if (Map == null)
                {
                    return new List<IMapTile>();
                }

                return Map.MapTiles;
            }
        }

        public void InvokeCommand(ICommand command)
        {
            command.Execute();
        }

        public bool IsValidSessionId(string sessionId)
        {
            lock (Lock)
            {
                return Clients.Any(x => x.SessionId == sessionId);
            }
        }

        public MapTileType? GetMapTile(decimal posX, decimal posY)
        {
            if (Map == null)
            {
                return null;
            }

            return Map.MapTiles.FirstOrDefault(x => x.Position.X == (int)posX && x.Position.Y == (int)posY)?.MapTileType ?? null;
        }

        public IMapTile? GetIMapTile(decimal posX, decimal posY)
        {
            if (Map == null)
            {
                return null;
            }

            return Map.MapTiles.FirstOrDefault(x => x.Position.X == (int)posX && x.Position.Y == (int)posY) ?? null;
        }
    }
}
