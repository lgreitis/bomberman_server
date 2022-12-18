using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Prototype;

namespace GameServices.Models.MapModels.Adapter
{
    public class PropAdapter : Bomb
    {
        public IMapProp Prop { get; set; }

        public PropAdapter(IMapProp prop)
        {
            Prop = prop;
        }
        public override List<Position> Activate()
        {
            return Prop.GetAffectedPositions(PlacedPosition);
        }

        public override void Place(Position position)
        {
            this.PlacedPosition = position;
        }

        public override void Reset()
        {
            PlacedPosition = null;
            ActivatableAfter = null;
        }

        public override IPrototypable ShallowCopy()
        {
            return (IPrototypable)this.MemberwiseClone();
        }

        public override IPrototypable DeepCopy()
        {
            var clone = (PropAdapter)this.MemberwiseClone();

            clone.PlacedPosition = PlacedPosition != null
                ? new Position(PlacedPosition.X, PlacedPosition.Y)
                : null;
            clone.ActivatableAfter = ActivatableAfter;
            clone.Prop = Prop;

            return clone;
        }
    }
}
