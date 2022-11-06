using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.Decorators;
using GameServices.Models.PlayerModels;

namespace GameServices.Facade
{
    public class MapFacade
    {
        private List<IMapTile> MapTiles { get; set; } = new List<IMapTile>();
        private List<IMapProp> MapProps { get; set; } = new List<IMapProp>();
        private List<MapPlayer> MapPlayers { get; set; } = new List<MapPlayer>();
        private List<MapTexture> MapTextures { get; set; } = new List<MapTexture>();

        public MapFacade()
        {
        }

        public void SetElement(List<IMapTile> mapTiles)
        {
            if (mapTiles == null)
            {
                return;
            }

            MapTiles = mapTiles;
        }

        public void SetElement(List<IMapProp> mapProps)
        {
            if (mapProps == null)
            {
                return;
            }

            MapProps = mapProps;
        }

        public void SetElement(List<MapPlayer> mapPlayers)
        {
            if (mapPlayers == null)
            {
                return;
            }

            MapPlayers = mapPlayers;
        }

        public void SetElement(List<MapTexture> mapTextures)
        {
            if (mapTextures == null)
            {
                return;
            }

            MapTextures = mapTextures;
        }

        public bool DoMapTilesExists()
        {
            return MapTiles != null && MapTiles.Any();
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

        public List<IMapTile> GetMapTiles()
        {
            return MapTiles;
        }

        public bool IsValidPlayerSessionId(string sessionId)
        {
            return MapPlayers.Any(x => x.Client != null && x.Client.SessionId == sessionId);
        }

        public IMapTile? GetMapTile(decimal posX, decimal posY)
        {
            return MapTiles.FirstOrDefault(x => x.Position.X == (int)posX && x.Position.Y == (int)posY);
        }

        public List<MapTexture> GetTextures()
        {
            var bombTextures = MapPlayers
                .Where(x => x.GetBomb().IsPlaced)
                .Select(x => new MapTexture
                {
                    Position = x.GetBomb().PlacedPosition,
                    TextureType = TextureType.RegularBomb
                })
                .ToList();

            MapTextures = MapTextures.Where(x => !x.TimeLeft.HasValue || x.TimeLeft.Value > 0).ToList();

            return bombTextures.Concat(MapTextures).Distinct().ToList();
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

        public void HarmMapTiles(List<Position> affectedPositions)
        {
            var affectedMapTiles = MapTiles
                .Where(x => affectedPositions.Any(y =>
                    y.X == x.Position.X
                    && y.Y == x.Position.Y))
                .ToList();

            affectedMapTiles.ForEach(x => x.Explode());
        }

        public void AddTextures(List<MapTexture> textures)
        {
            if (textures == null || !textures.Any())
            {
                return;
            }

            MapTextures.AddRange(textures);
        }
    }
}
