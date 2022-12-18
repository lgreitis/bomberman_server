using System;
using GameServices.Iterator;
using GameServices.Models.MapModels;
using GameServices.TemplateMethod;
using GameServices.Visitor;

namespace GameServices.Composite
{
	public class BombExplosionComposite : Component
	{
		private BombExplosionTemplate bombExplosionTemplate;

		public BombExplosionComposite(BombExplosionTemplate bombExplosionTemplate)
		{
			this.bombExplosionTemplate = bombExplosionTemplate;
		}

        public override void Travel(IVisitor visitor)
        {
            var aggregate = new CompositeAggregate();
            aggregate.Set(new List<Component> { this });

            var iterator = aggregate.CreateIterator();
            var item = iterator.First() as BombExplosionComposite;

            while (item != null)
            {
                item.Accept(visitor);
                //item.DoBombExplosion(_affectedPositions);

                item = iterator.Next() as BombExplosionComposite;
            }
        }

        public override void Accept(IVisitor visitor)
        {
            bombExplosionTemplate.Accept(visitor);
        }
    }
}

