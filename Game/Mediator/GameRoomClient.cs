using System;
namespace GameServices.Mediator
{
	public class GameRoomClient : Participant
	{
		public GameRoomClient(Chatroom chatroom, string name) : base(chatroom, name)
		{
		}

        public override string Retrieve(Message message)
        {
            message.Read(this);

            if (message.Author == this)
            {
                return $"You {message.Text}.";
            }

            return $"{message.Author.Name} {message.Text}.";
        }

        public override void Send(string message)
        {
            _chatroom.Send(new Message(this, message));
        }

        public override void Send(string message, Participant recipient)
        {
            _chatroom.Send(new Message(this, message, recipient));
        }
    }
}

