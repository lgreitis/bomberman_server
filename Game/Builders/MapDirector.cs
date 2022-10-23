using GameServices.Factories.MapFactory;
using GameServices.Model.Map;

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

        MapDirector director = new MapDirector();
        MapBuilder builder = new MapBuilder(MapFactory.getMapFactory(Enums.Level.First));
        director.Builder = builder;

        director.BuildMap();
    }
}
