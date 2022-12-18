using System;
using GameServices.Enums;
using GameServices.Interfaces;
using GameServices.Models.CommonModels;

namespace GameServices.Flyweight
{
	public abstract class MapTileFlyweight
	{
        protected decimal Multiplier;

        public abstract decimal GetMoveAmount(decimal baseAmount);
    }
}

