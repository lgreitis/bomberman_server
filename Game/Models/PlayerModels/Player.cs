using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Models.PlayerModels
{
    public abstract class Player
    {
        protected Bomb Bomb;
        protected Bomb? SavedBombState;
        public abstract int GetHealth();
        public abstract List<Position> ExplodeBomb();
    }
}
