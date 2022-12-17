using System;
using GameServices.TemplateMethod;

namespace GameServices.Iterator
{
	public class BombExplosionTemplateAggregate : Aggregate
	{
        List<BombExplosionTemplate> items = new List<BombExplosionTemplate>();

        public override Iterator CreateIterator()
        {
            return new BombExplosionTemplateIterator(this);
        }

        public void Set(List<BombExplosionTemplate> list)
        {
            items = list;
        }

        public int Count
        {
            get { return items.Count; }
        }

        public BombExplosionTemplate this[int index]
        {
            get { return items[index]; }
            set { items.Insert(index, value); }
        }
    }
}

