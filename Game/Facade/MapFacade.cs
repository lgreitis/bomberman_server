using GameServices.Facade.Subsystems;
using GameServices.Interfaces;
using GameServices.Mediator;
using GameServices.Memento;
using GameServices.Models.CommonModels;
using GameServices.Models.Containers;
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

        public FacadeMemento CreateMemento()
        {
            return new FacadeMemento(MapPlayerSubsystem.GetPlayers(), MapPropSubsystem.GetMapProps(), MapTileSubsystem.GetMapTiles(), MapTextureSubsystem.GetTextures());
        }

        public void SetMemento(FacadeMemento memento)
        {
            memento.Restore(this);
        }

        public void SetElement(List<MapTile> mapTiles)
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

        public void RegisterClient(Client client, Chatroom chatroom)
        {
            MapPlayerSubsystem.RegisterClient(client, chatroom);
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

        public List<MapTile> GetMapTiles()
        {
            return MapTileSubsystem.GetMapTiles();
        }

        public bool IsValidPlayerSessionId(string sessionId)
        {
            return MapPlayerSubsystem.IsValidPlayerSessionId(sessionId);
        }

        public MapTile? GetMapTile(decimal posX, decimal posY)
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
                MapTileSubsystem.GetBombExplosionTemplate()
            };
        }

        public Participant? GetChatParticipant(string sessionId)
        {
            return MapPlayerSubsystem.GetChatParticipant(sessionId);
        }

        public Participant? GetChatParticipantByUsername(string username)
        {
            return MapPlayerSubsystem.GetChatParticipantByUsername(username);
        }

        public void AddProp(IMapProp newProp)
        {
            MapPropSubsystem.AddProp(newProp);
        }

        public BombExplosionTemplate GetTextureContainer()
        {
            return MapTextureSubsystem.GetContainer();
        }

        public BombExplosionTemplate GetTileContainer()
        {
            return MapTileSubsystem.GetContainer();
        }

        public BombExplosionTemplate GetPlayerContainer()
        {
            return MapPlayerSubsystem.GetContainer();
        }

        public void DisconnectClient(string sessionId)
        {
            MapPlayerSubsystem.DisconnectClient(sessionId);
        }

        public string GetSessionIdByPlayerName(string username)
        {
            return MapPlayerSubsystem.GetSessionIdByPlayerName(username);
        }

        public void KickKillPlayer(string username)
        {
            MapPlayerSubsystem.KickKillPlayer(username);
        }

        public int CountAlivePlayers()
        {
            return MapPlayerSubsystem.CountAlivePlayers();
        }

        public List<string> GetDeadPlayers()
        {
            return MapPlayerSubsystem.GetDeadPlayers();
        }
    }
}
