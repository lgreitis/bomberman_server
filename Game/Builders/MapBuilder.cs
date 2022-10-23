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
            Map.MapTiles = Factory.GetTiles();
        }

        public void AddProps()
        {
            Map.MapProps = Factory.GetProps();
        }

        public void AddTiles()
        {
            Map.MapPlayers = Factory.GetPlayers();
        }

        public Map GetMap() 
        {
            var builtMap = Map;
            Map = new Map();

            return builtMap;
        }
    }
}
