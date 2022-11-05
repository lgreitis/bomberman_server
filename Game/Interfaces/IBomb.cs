using GameServices.Models.CommonModels;

namespace GameServices.Interfaces
{
    public interface IBomb
    {
        public Position? PlacedPosition { get; set; }
        public bool IsPlaced { get; }
        public DateTime? ActivatableAfter { get; set; }
        public void Place(Position position);
        public List<Position> Activate();
        public void Reset();
    }
}
