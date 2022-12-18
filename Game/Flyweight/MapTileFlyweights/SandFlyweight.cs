using System;
namespace GameServices.Flyweight.MapTileFlyweights
{
	public class SandFlyweight : MapTileFlyweight
	{
		public SandFlyweight()
        {
            Multiplier = 0.50M;
        }

        public override decimal GetMoveAmount(decimal baseAmount)
        {
            return baseAmount * Multiplier;
        }
    }
}

