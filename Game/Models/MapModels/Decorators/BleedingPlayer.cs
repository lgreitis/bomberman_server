namespace GameServices.Models.MapModels.Decorators
{
    public class BleedingPlayer : Damage
    {
        public BleedingPlayer(MapPlayer player) : base(player)
        {
        }

        public override int GetHealth()
        {
            return 2;
        }
    }
}
