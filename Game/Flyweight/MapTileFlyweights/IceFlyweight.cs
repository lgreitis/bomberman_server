using System;
namespace GameServices.Flyweight.MapTileFlyweights
{
	public class IceFlyweight : MapTileFlyweight
	{
		public IceFlyweight()
        {
            Multiplier = 1.50M;
        }

        public override decimal GetMoveAmount(decimal baseAmount)
        {
            return baseAmount * Multiplier;
        }
    }
}

