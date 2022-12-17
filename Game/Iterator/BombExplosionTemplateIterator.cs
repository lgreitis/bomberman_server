using System;
using GameServices.TemplateMethod;

namespace GameServices.Iterator
{
	public class BombExplosionTemplateIterator : Iterator
    {
        BombExplosionTemplateAggregate aggregate;
        int current = 0;

        public BombExplosionTemplateIterator(BombExplosionTemplateAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public override BombExplosionTemplate First()
        {
            return aggregate[0];
        }

        public override BombExplosionTemplate? Next()
        {
            BombExplosionTemplate? ret = null;

            if (current < aggregate.Count - 1)
            {
                ret = aggregate[++current];
            }

            return ret;
        }

        public override BombExplosionTemplate CurrentItem()
        {
            return aggregate[current];
        }

        public override bool IsDone()
        {
            return current >= aggregate.Count;
        }
    }
}

