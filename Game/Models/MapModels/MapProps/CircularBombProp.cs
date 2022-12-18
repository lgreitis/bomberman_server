using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels.MapProps
{
    public class CircularBombProp : IMapProp
    {
        public Position Position { get; set; }
        public bool IsTaken { get; set; } = false;
        private const int Radius = 5;

        public List<Position> GetAffectedPositions(Position placedPosition)
        {
            List<Position> positions = new List<Position>();

            for (int x = 0; x < Radius; x++)
            {
                positions.Add(new Position(placedPosition.X - x, placedPosition.Y));
                positions.Add(new Position(placedPosition.X + x, placedPosition.Y));

                for (int y = 0; y < Radius - x; y++)
                {
                    positions.Add(new Position(placedPosition.X + x, placedPosition.Y - y));
                    positions.Add(new Position(placedPosition.X + x, placedPosition.Y + y));
                    positions.Add(new Position(placedPosition.X - x, placedPosition.Y - y));
                    positions.Add(new Position(placedPosition.X - x, placedPosition.Y + y));
                }
            }

            return positions.Distinct().ToList();
        }

        public object Clone()
        {
            return (CircularBombProp)this.MemberwiseClone();
        }
    }
}
