using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Models.MapModels.Adapter;
using GameServices.Models.PlayerModels;

namespace GameServices.Facade.Subsystems
{
    public class MapPropSubsystem
    {
        private List<IMapProp> MapProps { get; set; } = new List<IMapProp>();

        public void Set(List<IMapProp> mapProps)
        {
            MapProps = mapProps;
        }

        public List<MapTexture> GetTextures()
        {
            return MapProps
                .Where(x => !x.IsTaken)
                .Select(x => new MapTexture
                {
                    Position = x.Position,
                    TextureType = TextureType.Prop
                })
                .ToList();
        }

        public IMapProp? GetProp(PositionExtended position)
        {
            return MapProps.Where(x =>
                    x.Position.X == (int)position.X
                    && x.Position.Y == (int)position.Y
                    && !x.IsTaken)
                .FirstOrDefault();
        }

        public void GetProp(MapPlayer player, IMapProp? prop)
        {
            if (player.HasProp || player.GetBomb().IsPlaced || prop == null)
            {
                return;
            }

            IBomb newBomb = new PropAdapter(prop);
            prop.IsTaken = true;
            player.HasProp = true;
            player.SetBomb(newBomb);
        }
    }
}
