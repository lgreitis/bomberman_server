using GameServices.Models.CommonModels;

namespace GameServices.Interfaces
{
    public interface IMapProp : ICloneable
    {
        public Position Position { get; set; }
        public bool IsTaken { get; set; }
        public List<Position> GetAffectedPositions(Position placedPosition);
    }
}
