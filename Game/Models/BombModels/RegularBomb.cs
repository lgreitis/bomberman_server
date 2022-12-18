using GameServices.Interfaces;
using GameServices.Models.CommonModels;
using GameServices.Models.MapModels;
using GameServices.Prototype;
using GameServices.State;

namespace GameServices.Models.BombModels
{
    public class RegularBomb : Bomb
    {
        public int PlacedTimeStamp { get; set; }

        public int Radius()
        {
            return 4;
        }

        public override List<Position> Activate(MapPlayer player)
        {
            if (!this.IsPlaced
                || !ActivatableAfter.HasValue
                || DateTime.Now < ActivatableAfter.Value)
            {
                return new List<Position>();
            }

            var affectedPositions = new List<Position>();
            
            for (int i = 0; i < Radius(); i++)
            {
                affectedPositions.Add(new Position(PlacedPosition.X - i, PlacedPosition.Y));
                affectedPositions.Add(new Position(PlacedPosition.X + i, PlacedPosition.Y));
                affectedPositions.Add(new Position(PlacedPosition.X, PlacedPosition.Y - i));
                affectedPositions.Add(new Position(PlacedPosition.X, PlacedPosition.Y + i));
            }

            State.Handle(this, player);

            return affectedPositions.Distinct().ToList();
        }

        public override void Place(Position position, MapPlayer player)
        {
            PlacedPosition = position;
            ActivatableAfter = DateTime.Now.AddSeconds(2);
            State.Handle(this, player);
        }

        public override void Reset(MapPlayer player)
        {
            PlacedPosition = null;
            ActivatableAfter = null;
            State.Handle(this, player);
        }

        public override IPrototypable ShallowCopy()
        {
            return (IPrototypable) this.MemberwiseClone();
        }

        public override IPrototypable DeepCopy()
        {
            var clone = (RegularBomb)this.MemberwiseClone();

            clone.PlacedPosition = PlacedPosition != null
                ? new Position(PlacedPosition.X, PlacedPosition.Y)
                : null;
            clone.ActivatableAfter = ActivatableAfter;
            clone.PlacedTimeStamp = PlacedTimeStamp;

            return clone;
        }
    }
}
