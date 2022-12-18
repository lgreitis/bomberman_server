using System;
namespace GameServices.Mediator
{
	public class GameRoomConsole : Chatroom
	{
		private List<Message> _messages; 

		public GameRoomConsole()
		{
			_messages = new List<Message>();
		}

        public override List<string> Retrieve(Participant participant, bool allMessages)
        {
            var toSend = new List<(string, DateTime)>();

            foreach (var message in _messages)
            {
                if ((allMessages || !message.IsRead(participant))
                    && (message.Recipient == null || message.Recipient == participant))
                {
                    var formatted = participant.Retrieve(message);
                    toSend.Add((formatted, message.DateSent));
                }
            }

            return toSend.OrderBy(x => x.Item2).Select(x => x.Item1).ToList();
        }

        public override void Send(Message message)
        {
            _messages.Add(message);
        }
    }
}

