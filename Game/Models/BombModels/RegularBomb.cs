using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.BombModels
{
    public class RegularBomb : IBomb
    {
        public Position PlacedPosition { get; set; }
        public bool IsPlaced { get; set; } = false;
        public int PlacedTimeStamp { get; set; }

        public int Radius()
        {
            return 4;
        }

        public List<Position> Activate()
        {
            if (!this.IsPlaced)
            {
                return new List<Position>();
            }

            List<Position> affectedPositions = new List<Position>();
            
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
            this.PlacedPosition = position;
            IsPlaced = true;
        }
    }
}
