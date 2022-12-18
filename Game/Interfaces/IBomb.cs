using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Prototype;
using GameServices.State;

namespace GameServices.Interfaces
{
    public abstract class Bomb : IPrototypable
    {
        public BombState State { get; set; } = new TakenBomb();

        public Position? PlacedPosition { get; set; }
        public bool IsPlaced { get; }
        public DateTime? ActivatableAfter { get; set; }
        public abstract void Place(Position position, MapPlayer player);
        public abstract List<Position> Activate(MapPlayer player);
        public abstract void Reset(MapPlayer player);

        public abstract IPrototypable DeepCopy();
        public abstract IPrototypable ShallowCopy();
    }
}
