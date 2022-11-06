using GameServices.Facade;

namespace GameServices.Builders
{
    public interface IMapBuilder
    {
        void AddTiles();

        void AddProps();

        void AddPlayers();

        MapFacade GetMap();
    }
}
