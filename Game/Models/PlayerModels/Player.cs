using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.PlayerModels
{
    public abstract class Player
    {
        protected IBomb Bomb;
        public abstract int GetHealth();
        public abstract List<Position> ExplodeBomb();
    }
}
