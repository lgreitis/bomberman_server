using System;
namespace GameServices.Visitor
{
	public interface IVisitor
	{
		void VisitElement(IElement element);
	}
}

