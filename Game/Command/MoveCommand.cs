using GameServices.Enums;
using GameServices.Singleton;

namespace GameServices.Command
{
    public class MoveCommand : ICommand
    {
        private string _sessionId;
        private bool? _moveX;
        private bool? _moveY;

        public MoveCommand(string sessionId, bool? moveX, bool? moveY)
        {
            _sessionId = sessionId;
            _moveX = moveX;
            _moveY = moveY;
        }

        public void Execute()
        {
            var gameManager = GamesManager.Instance.GetGameManager(_sessionId);
            var baseMoveAmount = 0.1M;

            lock (gameManager.Lock)
            {
                var player = gameManager.GetPlayer(_sessionId);
                var updateX = player.Position.X;
                var updateY = player.Position.Y;
                var validMove = true;

                var moveAmount = player.GetMoveAmount(baseMoveAmount);

                if (moveAmount > 1)
                {
                    moveAmount = 1;
                }

                if (_moveX.HasValue)
                {
                    updateX = player.Position.X - moveAmount;

                    if (_moveX.Value)
                    {
                        updateX = player.Position.X + moveAmount;
                    }
                }

                if (_moveY.HasValue)
                {
                    updateY = player.Position.Y - moveAmount;

                    if (_moveY.Value)
                    {
                        updateY = player.Position.Y + moveAmount;
                    }
                }

                if (_moveX.HasValue && _moveY.HasValue)
                {
                    var xMove = (int)updateX - (int)player.Position.X;
                    var yMove = (int)updateY - (int)player.Position.Y;

                    if (xMove != 0 && yMove != 0)
                    {
                        validMove = false;

                        var rightTile = gameManager.GetMapTile(player.Position.X + 1, player.Position.Y).MapTileType.IsWalkable();
                        var leftTile = gameManager.GetMapTile(player.Position.X - 1, player.Position.Y).MapTileType.IsWalkable();
                        var topTile = gameManager.GetMapTile(player.Position.X, player.Position.Y + 1).MapTileType.IsWalkable();
                        var botTile = gameManager.GetMapTile(player.Position.X, player.Position.Y - 1).MapTileType.IsWalkable();

                        if (xMove == 1 && yMove == 1)
                        {
                            validMove = topTile || rightTile;
                        }

                        if (xMove == -1 && yMove == -1)
                        {
                            validMove = botTile || leftTile;
                        }

                        if (xMove == 1 && yMove == -1)
                        {
                            validMove = botTile || rightTile;
                        }

                        if (xMove == -1 && yMove == 1)
                        {
                            validMove = topTile || leftTile;
                        }
                    }
                }

                if (validMove && (_moveX.HasValue || _moveY.HasValue))
                {
                    var movedMapTileX = gameManager.GetMapTile(updateX, player.Position.Y);
                    var movedMapTileY = gameManager.GetMapTile(player.Position.X, updateY);
                    var updateStrategy = false;

                    if (movedMapTileX.MapTileType.IsWalkable())
                    {
                        player.Position.X = updateX;
                        updateStrategy = true;
                    }

                    if (movedMapTileY.MapTileType.IsWalkable())
                    {
                        player.Position.Y = updateY;
                        updateStrategy = true;
                    }

                    if (updateStrategy)
                    {
                        player.MapTile = gameManager.GetMapTile(player.Position.X, player.Position.Y);
                    }
                }
            }
        }
    }
}
