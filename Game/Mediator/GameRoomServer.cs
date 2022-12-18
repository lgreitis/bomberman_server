using System;
namespace GameServices.Mediator
{
	public class GameRoomServer : Participant
	{
		public GameRoomServer(Chatroom chatroom) : base(chatroom, string.Empty)
		{
		}

        public override string Retrieve(Message message)
        {
            return message.Text;
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

