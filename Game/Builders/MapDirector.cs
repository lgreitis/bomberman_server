using GameServices.Factories.MapFactory;
using GameServices.Models.MapModels;

namespace GameServices.Builders
{
    public class MapDirector
    {
        private IMapBuilder _builder;

        public IMapBuilder Builder
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
