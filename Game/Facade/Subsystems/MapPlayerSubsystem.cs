using GameServices.Enums;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.Decorators;
using GameServices.Models.PlayerModels;

namespace GameServices.Facade.Subsystems
{
    public class MapPlayerSubsystem
    {
        private List<MapPlayer> MapPlayers { get; set; } = new List<MapPlayer>();

        public void Set(List<MapPlayer> mapPlayers)
        {
            MapPlayers = mapPlayers;
        }

        public void RegisterClient(Client client)
        {
            var registeredClient = MapPlayers.FirstOrDefault(x =>
                    x.Client != null
                    && x.Client.Token == client.Token);

            if (registeredClient == null)
            {
                MapPlayers.First(x => x.Client == null).Client = client;

                return;
            }

            registeredClient.Client.SessionId = client.SessionId;
        }

        public List<string> GetClientSessionIds()
        {
            return MapPlayers
                .Where(x => x.Client != null)
                .Select(x => x.Client.SessionId)
                .ToList();
        }

        public List<object> GetPlayerData()
        {
            return MapPlayers
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

        public MapPlayer GetPlayerData(string sessionId)
        {
            return MapPlayers.First(x => x.Client != null && x.Client.SessionId == sessionId);
        }

        public bool IsValidPlayerSessionId(string sessionId)
        {
            return MapPlayers.Any(x => x.Client != null && x.Client.SessionId == sessionId);
        }

        public List<MapTexture> GetTextures()
        {
            return MapPlayers
                .Where(x => x.GetBomb().IsPlaced)
                .Select(x => new MapTexture
                {
                    Position = x.GetBomb().PlacedPosition,
                    TextureType = TextureType.RegularBomb
                })
                .ToList();
        }

        public void HarmPlayers(List<Position> affectedPositions)
        {
            var affectedPlayers = MapPlayers
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

                MapPlayers.Remove(affectedPlayer);
                MapPlayers.Add(newPlayer);
            }
        }
    }
}
