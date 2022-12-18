using System;
namespace GameServices.Mediator
{
	public abstract class Chatroom
	{
		public abstract void Send(Message message);
		public abstract List<string> Retrieve(Participant participant, bool allMessages);
	}
}

