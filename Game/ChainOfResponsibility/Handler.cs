using System;
namespace GameServices.ChainOfResponsibility
{
	public abstract class Handler
	{
		protected Handler? _nextHandler;

		public void SetSuccessor(Handler handler)
		{
			_nextHandler = handler;
		}

		public abstract void HandleRequest();
	}
}

