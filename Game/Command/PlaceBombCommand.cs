using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.PlayerModels;

namespace GameServices.Command
{
    public class PlaceBombCommand : ICommand
    {
        private MapPlayer _mapPlayer;

        public PlaceBombCommand(MapPlayer mapPlayer)
        {
            _mapPlayer = mapPlayer;
        }

        public void Execute()
        {
            _mapPlayer.Bomb.Place(new Position((int)_mapPlayer.Position.X, (int)_mapPlayer.Position.Y));
        }
    }
}
