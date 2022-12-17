using System;
using GameServices.Enums;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.TemplateMethod;

namespace GameServices.Models.Containers
{
	public sealed class MapTextureContainer : BombExplosionTemplate
	{
		public List<MapTexture> Textures { get; set; }
        private List<MapTexture> PendingTextures { get; set; }

		public MapTextureContainer()
		{
			Textures = new List<MapTexture>();
            PendingTextures = new List<MapTexture>();
		}

        public override void PrepareBombExplosion(List<Position> positions)
        {
            var fireTextures = positions
                        .Select(x => new MapTexture
                        {
                            Position = new Position(x.X, x.Y),
                            TextureType = TextureType.Fire,
                            ValidUntil = DateTime.Now.AddMilliseconds(1000),
                        })
            .ToList();

            PendingTextures.AddRange(fireTextures);
        }

        public override void ExecuteBombExplosion()
        {
            var texturesToDisplay = PendingTextures.Where(x => x.TextureType == TextureType.Fire).ToList();

            foreach (var texture in texturesToDisplay)
            {
                Textures.Add(texture);
                PendingTextures.Remove(texture);
            }
        }
    }
}

