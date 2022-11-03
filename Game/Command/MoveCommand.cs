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
            var moveAmount = 0.1M;
            var gameManager = GamesManager.Instance.GetGameManager(_sessionId);

            lock (gameManager.Lock)
            {
                var client = gameManager.GetPlayer(_sessionId);
                var currentMapTileType = gameManager.GetMapTile(client.X, client.Y) ?? MapTileType.Bedrock;

                if (_moveX.HasValue)
                {
                    var newX = client.X - moveAmount;

                    if (_moveX.Value)
                    {
                        newX = client.X + moveAmount;
                    }

                    var mapTileType = gameManager.GetMapTile(newX, client.Y);

                    if (mapTileType.HasValue && mapTileType.Value.IsWalkable())
                    {
                        client.X = newX + (currentMapTileType == MapTileType.Wood ? moveAmount * 2 : 0);
                    }
                }

                if (_moveY.HasValue)
                {
                    var newY = client.Y - moveAmount;

                    if (_moveY.Value)
                    {
                        newY = client.Y + moveAmount;
                    }

                    var mapTileType = gameManager.GetMapTile(client.X, newY);

                    if (mapTileType.HasValue && mapTileType.Value.IsWalkable())
                    {
                        client.Y = newY + (currentMapTileType == MapTileType.Wood ? moveAmount * 2 : 0);
                    }
                }
            }
        }
    }
}
