using System;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.TemplateMethod;

namespace GameServices.Models.Containers
{
    public sealed class MapTileContainer : BombExplosionTemplate
    {
        public List<IMapTile> Tiles { get; set; }
        private List<IMapTile> PendingHarmTiles { get; set; }

        public MapTileContainer()
        {
            Tiles = new List<IMapTile>();
            PendingHarmTiles = new List<IMapTile>();
        }

        public override void PrepareBombExplosion(List<Position> positions)
        {
            var affectedMapTiles = Tiles
                .Where(x => positions.Any(y =>
                    y.X == x.Position.X
                    && y.Y == x.Position.Y))
                .ToList();

            affectedMapTiles.ForEach(x => x.Explode());
        }

        public override void ExecuteBombExplosion()
        {
            var unharmedTiles = PendingHarmTiles.ToList();

            foreach(var tile in unharmedTiles)
            {
                tile.Explode();
                PendingHarmTiles.Remove(tile);
            }
        }
    }
}