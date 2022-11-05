using GameServices.Enums;
using GameServices.Factories.MapFactory;
using GameServices.Models.MapModels;

namespace GameServices.Builders
{
    public class MapBuilder : IMapBuilder
    {
        private Map Map = new Map();
        private MapAbstractFactory Factory;

        public MapBuilder(MapAbstractFactory factory)
        {
            Factory = factory;
        }

        public void AddPlayers()
        {
            Map.MapPlayers = Factory.GetPlayers();

            if (Map.MapTiles == null || !Map.MapTiles.Any())
            {
                return;
            }

            foreach (var mapPlayer in Map.MapPlayers)
            {
                var mapTile = Map.MapTiles
                    .Where(x => x.MapTileType.IsWalkable())
                    .OrderBy(x => Guid.NewGuid())
                    .FirstOrDefault();

                if (mapTile == null)
                {
                    continue;
                }

                mapPlayer.Position.X = mapTile.Position.X + 0.5M;
                mapPlayer.Position.Y = mapTile.Position.Y + 0.5M;
                mapPlayer.MapTile = mapTile;
            }
        }

        public void AddProps()
        {
            Map.MapProps = Factory.GetProps();
        }

        public void AddTiles()
        {
            Map.MapTiles = Factory.GetTiles();
        }

        public Map GetMap() 
        {
            var builtMap = Map;
            Map = new Map();

            return builtMap;
        }
    }
}
