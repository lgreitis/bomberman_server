using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels.Adapter
{
    public class PropAdapter : IBomb
    {
        public IMapProp Prop { get; set; }
        public Position? PlacedPosition { get; set; }
        public bool IsPlaced { get { return PlacedPosition != null; } }
        public DateTime? ActivatableAfter { get; set; }

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
    }
}
