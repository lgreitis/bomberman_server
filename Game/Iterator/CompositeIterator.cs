using System;
using GameServices.Composite;
using GameServices.TemplateMethod;

namespace GameServices.Iterator
{
	public class CompositeIterator : Iterator
	{
        CompositeAggregate aggregate;
        int current = 0;

        public CompositeIterator(CompositeAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public override Component First()
        {
            return aggregate[0];
        }

        public override Component? Next()
        {
            Component? ret = null;

            if (current < aggregate.Count - 1)
            {
                ret = aggregate[++current];
            }

            return ret;
        }
    }
}

