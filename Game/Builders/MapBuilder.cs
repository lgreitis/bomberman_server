using GameServices.Enums;
using GameServices.Facade;
using GameServices.Factories.MapFactory;

namespace GameServices.Builders
{
    public class MapBuilder : IMapBuilder
    {
        private MapFacade Map = new MapFacade();
        private MapAbstractFactory Factory;

        public MapBuilder(MapAbstractFactory factory)
        {
            Factory = factory;
        }

        public void AddPlayers()
        {
            var players = Factory.GetPlayers();

            if (Map.DoMapTilesExists())
            {

                foreach (var mapPlayer in players)
                {
                    var mapTile = Map.GetMapTiles()
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

            Map.SetElement(players);
        }

        public void AddProps()
        {
            Map.SetElement(Factory.GetProps());
        }

        public void AddTiles()
        {
            Map.SetElement(Factory.GetTiles());
        }

        public MapFacade GetMap() 
        {
            var builtMap = Map;
            Map = new MapFacade();

            return builtMap;
        }
    }
}
