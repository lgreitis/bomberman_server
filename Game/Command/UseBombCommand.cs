using GameServices.ChainOfResponsibility;
using GameServices.Enums;
using GameServices.Models.BombModels;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.Decorators;
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
            if (_mapPlayer is DeadPlayer)
            {
                return;
            }

            var gameManager = GamesManager.Instance.GetGameManager(_mapPlayer.Client.SessionId);

            lock (gameManager.Lock)
            {
                if (_mapPlayer.GetBomb().IsPlaced)
                {
                    var affectedPositions = _mapPlayer.ExplodeBomb();

                    if (!affectedPositions.Any())
                    {
                        return;
                    }

                    var playerBeforeHander = new PlayerBombInitiatedHandler(_mapPlayer);

                    var bombHandler = new BombHandler(gameManager, affectedPositions);
                    playerBeforeHander.SetSuccessor(bombHandler);

                    var playerAfterHandler = new PlayerBombExplodedHandler(_mapPlayer);
                    bombHandler.SetSuccessor(playerAfterHandler);

                    var propHandler = new PropGeneratorHandler(gameManager);
                    playerAfterHandler.SetSuccessor(propHandler);

                    playerBeforeHander.HandleRequest();

                    return;
                }

                _mapPlayer.SaveBombState();
                _mapPlayer.GetBomb().Place(new Position((int)_mapPlayer.Position.X, (int)_mapPlayer.Position.Y), _mapPlayer);
            }
        }

        public void Undo()
        {
            if (_mapPlayer is DeadPlayer)
            {
                return;
            }

            var gameManager = GamesManager.Instance.GetGameManager(_mapPlayer.Client.SessionId);

            lock (gameManager.Lock)
            {
                _mapPlayer.ResetBombState();
            }
        }
    }
}
