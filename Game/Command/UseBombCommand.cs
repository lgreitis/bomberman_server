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


                    _mapPlayer.HasProp = false;
                    _mapPlayer.SetBomb(new RegularBomb());
                    _mapPlayer.GetBomb().Reset();

                    var fireTextures = affectedPositions
                        .Select(x => new MapTexture
                        {
                            Position = new Position(x.X, x.Y),
                            TextureType = TextureType.Fire,
                            ValidUntil = DateTime.Now.AddMilliseconds(1000),
                        })
                        .ToList();

                    gameManager.Map.AddTextures(fireTextures);

                    gameManager.HarmPlayers(affectedPositions);
                    gameManager.HarmMapTiles(affectedPositions);

                    return;
                }

                _mapPlayer.GetBomb().Place(new Position((int)_mapPlayer.Position.X, (int)_mapPlayer.Position.Y));
            }
        }
    }
}
