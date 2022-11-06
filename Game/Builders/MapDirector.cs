using GameServices.Facade;
using GameServices.Factories.MapFactory;

namespace GameServices.Builders
{
    public class MapDirector
    {
        private IMapBuilder _builder;

        public IMapBuilder Builder
        {
            set { _builder = value; }
        }

        public MapFacade BuildMap()
        {
            _builder.AddTiles();
            _builder.AddProps();
            _builder.AddPlayers();

            return _builder.GetMap();
        }
    }
}
