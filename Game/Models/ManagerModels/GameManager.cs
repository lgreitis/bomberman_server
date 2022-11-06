using GameServices.Builders;
using GameServices.Command;
using GameServices.Enums;
using GameServices.Factories.MapFactory;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.Decorators;
using GameServices.Models.PlayerModels;

namespace GameServices.Models.ManagerModels
{
    // todo facade 
    public class GameManager
    {
        public readonly object Lock = new object();
        public int LobbyId { get; private set; }
        public Map? Map { get; private set; }
        public Level Level { get; private set; }

        public GameManager(int lobbyId)
        {
            LobbyId = lobbyId;
            Level = Level.First;

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
                var registeredClient = Map.MapPlayers.FirstOrDefault(x =>
                    x.Client != null
                    && x.Client.Token == client.Token);

                if (registeredClient == null)
                {
                    Map.MapPlayers.First(x => x.Client == null).Client = client;

                    return;
                }

                registeredClient.Client.SessionId = client.SessionId;
            }
        }

        public List<string> GetSessionIds()
        {
            lock (Lock)
            {
                return Map.MapPlayers
                    .Where(x => x.Client != null)
                    .Select(x => x.Client.SessionId)
                    .ToList();
            }
        }

        public List<object> GetPlayers()
        {
            lock (Lock)
            {
                return Map.MapPlayers
                    .Where(x => x.Client != null)
                    .Select(x => new
                    {
                        Username = x.Client.Username,
                        UserId = x.Client.UserId,
                        Token = x.Client.Token,
                        X = x.Position.X,
                        Y = x.Position.Y,
                        HealthPoints = x.GetHealth()
                    })
                    .Select(x => (object)x)
                    .ToList();
            }
        }

        public MapPlayer GetPlayer(string sessionId)
        {
            lock (Lock)
            {
                return Map.MapPlayers.First(x =>
                    x.Client != null
                    && x.Client.SessionId == sessionId);
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
                return Map.MapPlayers.Any(x => x.Client != null && x.Client.SessionId == sessionId);
            }
        }

        public IMapTile? GetMapTile(decimal posX, decimal posY)
        {
            if (Map == null)
            {
                return null;
            }

            return Map.MapTiles.FirstOrDefault(x => x.Position.X == (int)posX && x.Position.Y == (int)posY);
        }

        public IMapTile? GetIMapTile(decimal posX, decimal posY)
        {
            if (Map == null)
            {
                return null;
            }

            return Map.MapTiles.FirstOrDefault(x => x.Position.X == (int)posX && x.Position.Y == (int)posY) ?? null;
        }

        public List<MapTexture> GetTextures()
        {
            lock (Lock)
            {
                var bombTextures = Map.MapPlayers
                    .Where(x => x.GetBomb().IsPlaced)
                    .Select(x => new MapTexture
                    {
                        Position = x.GetBomb().PlacedPosition,
                        TextureType = TextureType.RegularBomb
                    })
                    .ToList();

                Map.MapTextures = Map.MapTextures.Where(x => !x.TimeLeft.HasValue || x.TimeLeft.Value > 0).ToList();

                return bombTextures.Concat(Map.MapTextures).Distinct().ToList();
            }
        }

        public void HarmPlayers(List<Position> affectedPositions)
        {
            var affectedPlayers = Map.MapPlayers
                .Where(x => affectedPositions.Any(y =>
                    y.X == (int)x.Position.X
                    && y.Y == (int)x.Position.Y))
                .ToList();

            foreach (var affectedPlayer in affectedPlayers)
            {
                MapPlayer newPlayer;

                if (affectedPlayer is DeadPlayer)
                {
                    continue;
                }
                else if (affectedPlayer is BleedingPlayer)
                {
                    newPlayer = new InjuredPlayer(affectedPlayer);
                }
                else if (affectedPlayer is InjuredPlayer)
                {
                    newPlayer = new DeadPlayer(affectedPlayer);
                }
                else
                {
                    newPlayer = new BleedingPlayer(affectedPlayer);
                }

                Map.MapPlayers.Remove(affectedPlayer);
                Map.MapPlayers.Add(newPlayer);
            }
        }

        public void HarmMapTiles(List<Position> affectedPositions)
        {
            var affectedMapTiles = Map.MapTiles
                .Where(x => affectedPositions.Any(y =>
                    y.X == x.Position.X
                    && y.Y == x.Position.Y))
                .ToList();

            affectedMapTiles.ForEach(x => x.Explode());
        }
    }
}
