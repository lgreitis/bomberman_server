using GameServices.Enums;
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

                        var fireTextures = affectedPositions
                            .Select(x => new MapTexture
                            {
                                Position = new Position(x.X, x.Y),
                                TextureType = TextureType.Fire,
                                ValidUntil = DateTime.Now.AddMilliseconds(1000),
                            })
                            .ToList();

                        gameManager.Map.MapTextures.AddRange(fireTextures);
                    }

                    _mapPlayer.Bomb.Reset();

                    return;
                }

                _mapPlayer.Bomb.Place(new Position((int)_mapPlayer.Position.X, (int)_mapPlayer.Position.Y));
            }
        }
    }
}
