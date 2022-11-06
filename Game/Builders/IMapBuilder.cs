using GameServices.Models.MapModels;

namespace GameServices.Builders
{
    public interface IMapBuilder
    {
        void AddTiles();

        void AddProps();

        void AddPlayers();

        Map GetMap();
    }
}
