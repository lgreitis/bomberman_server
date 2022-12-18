
using System;
using GameServices.Composite;
using GameServices.TemplateMethod;

namespace GameServices.Iterator
{
	public class CompositeAggregate : Aggregate
	{
        List<Component> items = new List<Component>();
        List<Component> flattened = new List<Component>();

        public override Iterator CreateIterator()
        {
            return new CompositeIterator(this);
        }

        public void Set(List<Component> list)
        {
            items = list;
            Flatten();
        }

        private void Flatten()
        {
            flattened = new List<Component>();

            foreach (var item in items)
            {
                Pick(item);
            }
        }

        private void Pick(Component component)
        {
            flattened.Add(component);

            for (var i = 0; i < component.Count; i++)
            {
                Pick(component.GetChild(i));
            }
        }

        public int Count
        {
            get
            {
                return flattened.Count;
            }
        }

        public Component this[int index]
        {
            get { return flattened[index]; }
            set { items.Insert(index, value); flattened.Add(value); }
        }
    }
}

