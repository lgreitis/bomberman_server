using GameServices.Models.Containers;
using GameServices.Models.MapModels;
using GameServices.TemplateMethod;

namespace GameServices.Facade.Subsystems
{
    public class MapTextureSubsystem
    {
        private MapTextureContainer _container = new MapTextureContainer();

        public void Set(List<MapTexture> mapTextures)
        {
            _container.Textures = mapTextures;
        }

        public BombExplosionTemplate GetBombExplosionTemplate()
        {
            return _container;
        }

        public List<MapTexture> GetTextures()
        {
            _container.Textures = _container.Textures.Where(x => !x.TimeLeft.HasValue || x.TimeLeft.Value > 0).ToList();

            return _container.Textures;
        }

        public MapTextureContainer GetContainer()
        {
            return _container;
        }

        public void SetContainer(MapTextureContainer container)
        {
            _container = container;
        }
    }
}
