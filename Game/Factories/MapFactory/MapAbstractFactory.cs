using GameServices.Models.MapModels;

namespace GameServices.Factories.MapFactory
{
    public abstract class MapAbstractFactory
    {
        public abstract List<MapTile> GetTiles();
        public abstract List<MapProp> GetProps();
        public abstract List<MapPlayer> GetPlayers();
    }
}
