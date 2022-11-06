using GameServices.Models.MapModels;

namespace GameServices.Facade.Subsystems
{
    public class MapTextureSubsystem
    {
        private List<MapTexture> MapTextures { get; set; } = new List<MapTexture>();

        public void Set(List<MapTexture> mapTextures)
        {
            MapTextures = mapTextures;
        }

        public List<MapTexture> GetTextures()
        {
            MapTextures = MapTextures.Where(x => !x.TimeLeft.HasValue || x.TimeLeft.Value > 0).ToList();

            return MapTextures;
        }

        public void AddTextures(List<MapTexture> textures)
        {
            MapTextures.AddRange(textures);
        }
    }
}
