using GameServices.Enums;
using GameServices.Facade;
using GameServices.Interfaces;
using GameServices.Models.BombModels;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.MapProps;
using GameServices.Randomizers;

namespace GameServices.Builders
{
    public class TemplateMapBuilder : IMapBuilder
    {
        private int players;
        private MapFacade Map = new MapFacade();
        public TemplateMapBuilder(int players)
        {
            this.players = players;
        }

        public void AddPlayers()
        {
            var mapPlayers = new List<MapPlayer>();

            for (int i = 0; i < players; i++)
            {
                mapPlayers.Add(new MapPlayer(null, new PositionExtended((decimal)1.5 + i, (decimal)1.5 + i), new RegularBomb(), null));
            }

            Map.SetElement(mapPlayers);
        }

        public void AddProps()
        {
            var mapProps = new List<IMapProp>();
            mapProps.Add(new CircularBombProp { Position = new Position(1, players + 1) });
            mapProps.Add(new CircularBombProp { Position = new Position(players + 1, 1) });
            Map.SetElement(mapProps);
        }

        public void AddTiles()
        {
            Map.SetElement(MapTileRandomizer.GetMapTiles(players + 2, players + 2, new List<MapTileType>
            {
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Grass,
                MapTileType.Ice,
                MapTileType.Water,
                MapTileType.Sand
            }));
        }

        public MapFacade GetMap()
        {
            var builtMap = Map;
            Map = new MapFacade();

            return builtMap;
        }
    }
}
