using System;
namespace GameServices.Flyweight.MapTileFlyweights
{
	public class WaterFlyweight : MapTileFlyweight
	{
		public WaterFlyweight()
        {
            Multiplier = 0.25M;
        }

        public override decimal GetMoveAmount(decimal baseAmount)
        {
            return baseAmount * Multiplier;
        }
    }
}

