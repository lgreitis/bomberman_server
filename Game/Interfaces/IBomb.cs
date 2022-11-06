using GameServices.Models.CommonModels;
using GameServices.Prototype;

namespace GameServices.Interfaces
{
    public interface IBomb : IPrototypable
    {
        public Position? PlacedPosition { get; set; }
        public bool IsPlaced { get; }
        public DateTime? ActivatableAfter { get; set; }
        public void Place(Position position);
        public List<Position> Activate();
        public void Reset();
    }
}
