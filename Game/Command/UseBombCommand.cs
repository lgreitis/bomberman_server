using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Singleton;

namespace GameServices.Command
{
    public class UseBombCommand : ICommand
    {
        private MapPlayer _mapPlayer;

        public UseBombCommand(MapPlayer mapPlayer)
        {
            _mapPlayer = mapPlayer;
        }

        public void Execute()
        {
            var gameManager = GamesManager.Instance.GetGameManager(_mapPlayer.Client.SessionId);

            lock (gameManager.Lock)
            {
                if (_mapPlayer.Bomb.IsPlaced)
                {
                    var affectedPositions = _mapPlayer.Bomb.Activate();

                    if (!affectedPositions.Any())
                    {
                        return;
                    }

                    lock (gameManager.Lock)
                    {
                        gameManager.HarmPlayers(affectedPositions);
                        gameManager.HarmMapTiles(affectedPositions);
                    }

                    _mapPlayer.Bomb.Reset();

                    return;
                }

                _mapPlayer.Bomb.Place(new Position((int)_mapPlayer.Position.X, (int)_mapPlayer.Position.Y));
            }
        }
    }
}
