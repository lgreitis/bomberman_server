using GameServices.Model.Map;
using GameServices.Models.Map;

namespace GameServices.Factories.MapFactory
{
    public abstract class MapAbstractFactory
    {
        public abstract List<MapTile> getTiles();
        public abstract List<MapProp> getProps();
        public abstract List<MapPlayer> getPlayers();
    }
}
