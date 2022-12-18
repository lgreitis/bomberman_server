using System;
using GameServices.Visitor;

namespace GameServices.Composite
{
	public abstract class Component
	{
		protected List<Component> Children { get; set; }
		public int Count => Children.Count;

		public Component()
		{
			Children = new List<Component>();
		}

		public void AddChild(Component child)
		{
			Children.Add(child);
		}

		public void RemoveChild(Component child)
		{
			Children.Remove(child);
		}

		public Component GetChild(int index)
		{
			return Children[index];
		}

        public abstract void Accept(IVisitor visitor);
        public abstract void Travel(IVisitor visitor);
    }
}

