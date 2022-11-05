using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels.Adapter
{
    public class PropAdapter : IBomb
    {
        public IProp Prop { get; set; }
        public Position PlacedPosition { get; set; }
        public bool IsPlaced { get; set; }

        public List<Position> Activate()
        {
            return Prop.GetAffectedPositions(PlacedPosition);
        }

        public void Place(Position position)
        {
            this.PlacedPosition = position;
            IsPlaced = true;
        }
    }
}
