using GameServices.Enums;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
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
                var client = gameManager.GetPlayer(_sessionId);
                var currentMapTileType = gameManager.GetMapTile(client.X, client.Y) ?? MapTileType.Bedrock;

                client.mapTile = gameManager.GetIMapTile(client.X, client.Y) ?? new BedrockTile { MapTileType = MapTileType.Bedrock };
                var moveAmount = client.GetSpeed(baseMoveAmount);

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
