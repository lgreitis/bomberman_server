using GameServices.Facade.Subsystems;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.PlayerModels;
using GameServices.TemplateMethod;

namespace GameServices.Facade
{
    public class MapFacade
    {
        private MapTileSubsystem MapTileSubsystem;
        private MapPropSubsystem MapPropSubsystem;
        private MapPlayerSubsystem MapPlayerSubsystem;
        private MapTextureSubsystem MapTextureSubsystem;

        public MapFacade()
        {
            MapTileSubsystem = new MapTileSubsystem();
            MapPropSubsystem = new MapPropSubsystem();
            MapPlayerSubsystem = new MapPlayerSubsystem();
            MapTextureSubsystem = new MapTextureSubsystem();
        }

        public void SetElement(List<IMapTile> mapTiles)
        {
            if (mapTiles == null)
            {
                return;
            }

            MapTileSubsystem.Set(mapTiles);
        }

        public void SetElement(List<IMapProp> mapProps)
        {
            if (mapProps == null)
            {
                return;
            }

            MapPropSubsystem.Set(mapProps);
        }

        public void SetElement(List<MapPlayer> mapPlayers)
        {
            if (mapPlayers == null)
            {
                return;
            }

            MapPlayerSubsystem.Set(mapPlayers);
        }

        public void SetElement(List<MapTexture> mapTextures)
        {
            if (mapTextures == null)
            {
                return;
            }

            MapTextureSubsystem.Set(mapTextures);
        }

        public bool DoMapTilesExists()
        {
            return MapTileSubsystem.DoMapTilesExists();
        }

        public void RegisterClient(Client client)
        {
            MapPlayerSubsystem.RegisterClient(client);
        }

        public List<string> GetClientSessionIds()
        {
            return MapPlayerSubsystem.GetClientSessionIds();
        }

        public List<object> GetPlayerData()
        {
            return MapPlayerSubsystem.GetPlayerData();
        }

        public MapPlayer GetPlayerData(string sessionId)
        {
            return MapPlayerSubsystem.GetPlayerData(sessionId);
        }

        public List<IMapTile> GetMapTiles()
        {
            return MapTileSubsystem.GetMapTiles();
        }

        public bool IsValidPlayerSessionId(string sessionId)
        {
            return MapPlayerSubsystem.IsValidPlayerSessionId(sessionId);
        }

        public IMapTile? GetMapTile(decimal posX, decimal posY)
        {
            return MapTileSubsystem.GetMapTile(posX, posY);
        }

        public List<MapTexture> GetTextures()
        {
            var bombTextures = MapPlayerSubsystem.GetTextures();
            var propTextures = MapPropSubsystem.GetTextures();
            var commonTextures = MapTextureSubsystem.GetTextures();

            return bombTextures.Concat(propTextures).Concat(commonTextures).Distinct().ToList();
        }

        public void HarmPlayers(List<Position> affectedPositions)
        {
            MapPlayerSubsystem.HarmPlayers(affectedPositions);
        }

        public void HarmMapTiles(List<Position> affectedPositions)
        {
            MapTileSubsystem.HarmMapTiles(affectedPositions);
        }

        public void PickProp(MapPlayer player)
        {
            var prop = MapPropSubsystem.GetProp(player.Position);
            MapPropSubsystem.GetProp(player, prop);
        }

        public List<BombExplosionTemplate> GetBombExplosionTemplates()
        {
            return new List<BombExplosionTemplate>
            {
                MapPlayerSubsystem.GetBombExplosionTemplate(),
                MapTextureSubsystem.GetBombExplosionTemplate(),
                MapTextureSubsystem.GetBombExplosionTemplate()
            };
        }
    }
}
