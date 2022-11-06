namespace GameServices.Models.MapModels.Decorators
{
    public abstract class Damage : MapPlayer
    {
        protected Damage(MapPlayer mapPlayer) : base(mapPlayer)
        {
        }

        public override abstract int GetHealth();
    }
}
