﻿using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.Containers;
using GameServices.Models.MapModels;
using GameServices.TemplateMethod;

namespace GameServices.Facade.Subsystems
{
    public class MapTileSubsystem
    {
        private MapTileContainer _container = new MapTileContainer();

        public BombExplosionTemplate GetBombExplosionTemplate()
        {
            return _container;
        }

        public void Set(List<IMapTile> mapTiles)
        {
            _container.Tiles = mapTiles;
        }

        public bool DoMapTilesExists()
        {
            return _container.Tiles != null && _container.Tiles.Any();
        }

        public List<IMapTile> GetMapTiles()
        {
            return _container.Tiles;
        }

        public IMapTile? GetMapTile(decimal posX, decimal posY)
        {
            return _container.Tiles.FirstOrDefault(x => x.Position.X == (int)posX && x.Position.Y == (int)posY);
        }

        public void HarmMapTiles(List<Position> affectedPositions)
        {
            var affectedMapTiles = _container.Tiles
                .Where(x => affectedPositions.Any(y =>
                    y.X == x.Position.X
                    && y.Y == x.Position.Y))
                .ToList();

            affectedMapTiles.ForEach(x => x.Explode());
        }
    }
}
