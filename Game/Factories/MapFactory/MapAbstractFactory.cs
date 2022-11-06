using GameServices.Interfaces;
using GameServices.Models.MapModels;

namespace GameServices.Factories.MapFactory
{
    public abstract class MapAbstractFactory
    {
        public abstract List<IMapTile> GetTiles();
        public abstract List<IMapProp> GetProps();
        public abstract List<MapPlayer> GetPlayers();
    }
}
