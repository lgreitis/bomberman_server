using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Prototype;

namespace GameServices.Models.MapModels.Adapter
{
    public class PropAdapter : IBomb
    {
        public IMapProp Prop { get; set; }
        public Position? PlacedPosition { get; set; }
        public bool IsPlaced { get { return PlacedPosition != null; } }
        public DateTime? ActivatableAfter { get; set; }

        public PropAdapter(IMapProp prop)
        {
            Prop = prop;
        }
        public List<Position> Activate()
        {
            return Prop.GetAffectedPositions(PlacedPosition);
        }

        public void Place(Position position)
        {
            this.PlacedPosition = position;
        }

        public void Reset()
        {
            PlacedPosition = null;
            ActivatableAfter = null;
        }

        public IPrototypable ShallowCopy()
        {
            return (IPrototypable)this.MemberwiseClone();
        }

        public IPrototypable DeepCopy()
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
