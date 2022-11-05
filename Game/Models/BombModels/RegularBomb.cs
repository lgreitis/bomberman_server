using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.BombModels
{
    public class RegularBomb : IBomb
    {
        public Position? PlacedPosition { get; set; }
        public bool IsPlaced { get { return PlacedPosition != null; } }
        public int PlacedTimeStamp { get; set; }
        public DateTime? ActivatableAfter { get; set; }

        public int Radius()
        {
            return 4;
        }

        public List<Position> Activate()
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

            return affectedPositions.Distinct().ToList();
        }

        public void Place(Position position)
        {
            PlacedPosition = position;
            ActivatableAfter = DateTime.Now.AddSeconds(2);
        }

        public void Reset()
        {
            PlacedPosition = null;
            ActivatableAfter = null;
        }
    }
}
