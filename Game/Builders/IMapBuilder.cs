using GameServices.Model.Map;

namespace GameServices.Builders
{
    public interface IMapBuilder
    {
        void AddTiles();

        void AddProps();

        void AddPlayers();
    }
}
