using System;
using GameServices.Models.CommonModels;
using GameServices.Visitor;

namespace GameServices.TemplateMethod
{
	public abstract class BombExplosionTemplate : IElement
	{
		public void DoBombExplosion(List<Position> positions)
		{
			PrepareBombExplosion(positions);
			ExecuteBombExplosion();
		}

		public abstract void PrepareBombExplosion(List<Position> positions);
		public abstract void ExecuteBombExplosion();
		public void Accept(IVisitor visitor)
		{
			visitor.VisitElement(this);
		}
	}
}

