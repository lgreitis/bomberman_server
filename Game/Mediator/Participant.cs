using System;
namespace GameServices.Mediator
{
	public abstract class Participant
	{
		public string Name { get; private set; }
		protected Chatroom _chatroom;

		public Participant(Chatroom chatroom, string name)
		{
			_chatroom = chatroom;
			Name = name;
		}

        public abstract void Send(string message);
        public abstract void Send(string message, Participant recipient);
        public abstract string Retrieve(Message message);
	}
}

