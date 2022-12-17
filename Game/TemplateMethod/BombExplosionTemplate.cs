using System;
using GameServices.Models.CommonModels;

namespace GameServices.TemplateMethod
{
	public abstract class BombExplosionTemplate
	{
		public void DoBombExplosion(List<Position> positions)
		{
			PrepareBombExplosion(positions);
			ExecuteBombExplosion();
		}

		public abstract void PrepareBombExplosion(List<Position> positions);
		public abstract void ExecuteBombExplosion();
	}
}

