using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.MapModels.MapProps
{
    public class CircularBombProp : IMapProp
    {
        public Position Position { get; set; }
        public bool IsTaken { get; set; } = false;
        int Radius = 5;

        public List<Position> GetAffectedPositions(Position placedPosition)
        {
            return new List<Position>();
        }
    }
}
