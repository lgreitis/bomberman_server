using GameServices.Models.CommonModels;

namespace GameServices.Interfaces
{
    public interface IBomb
    {
        public Position PlacedPosition { get; set; }
        public bool IsPlaced { get; set; }
        public abstract void Place(Position position);
        public abstract List<Position> Activate();
    }
}
