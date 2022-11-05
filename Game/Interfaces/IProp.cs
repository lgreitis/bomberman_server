using GameServices.Models.CommonModels;

namespace GameServices.Interfaces
{
    public interface IProp
    {
        public Position Position { get; set; }
        public bool IsTaken { get; set; }
        public List<Position> GetAffectedPositions(Position placedPosition);
    }
}
