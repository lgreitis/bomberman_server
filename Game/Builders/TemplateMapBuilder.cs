using GameServices.Enums;
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
        private Map Map = new Map();
        public TemplateMapBuilder(int players)
        {
            this.players = players;
        }

        public void AddPlayers()
        {
            for (int i = 0; i < players; i++)
            {
                Map.MapPlayers.Add(new MapPlayer(null, new PositionExtended((decimal)1.5 + i, (decimal)1.5 + i), new RegularBomb(), null));
            }
        }

        public void AddProps()
        {
            Map.MapProps.Add(new CircularBombProp { Position = new Position(1, players + 1) });
            Map.MapProps.Add(new CircularBombProp { Position = new Position(players + 1, 1) });
        }

        public void AddTiles()
        {
            Map.MapTiles.AddRange(MapTileRandomizer.GetMapTiles(players + 2, players + 2, new List<MapTileType>
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

        public Map GetMap()
        {
            var builtMap = Map;
            Map = new Map();

            return builtMap;
        }
    }
}
