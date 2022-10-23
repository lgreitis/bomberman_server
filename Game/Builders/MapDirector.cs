using GameServices.Factories.MapFactory;
using GameServices.Models.MapModels;

namespace GameServices.Builders
{
    public class MapDirector
    {
        private MapBuilder _builder;

        public MapBuilder Builder
        {
            set { _builder = value; }
        }

        public Map BuildMap()
        {
            _builder.AddTiles();
            _builder.AddProps();
            _builder.AddPlayers();

            return _builder.GetMap();
        }
    }
}
