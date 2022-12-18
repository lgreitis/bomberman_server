using System;
namespace GameServices.Visitor
{
	public interface IElement
	{
		void Accept(IVisitor visitor);
	}
}