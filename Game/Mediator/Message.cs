using System;
namespace GameServices.Mediator
{
	public class Message
	{
		private List<Participant> _readBy;
        public Participant Author { get; private set; }
        public Participant? Recipient { get; private set; }
        public string Text { get; private set; }
		public DateTime DateSent;

        public Message(Participant author, string text)
        {
            _readBy = new List<Participant>();
            Author = author;
            Text = text;
            DateSent = DateTime.Now;
        }

        public Message(Participant author, string text, Participant recipient) : this(author, text)
        {
			Recipient = recipient;
        }

        public void Read(Participant participant)
		{
			if (!IsRead(participant))
			{
				_readBy.Add(participant);
			}
		}

		public bool IsRead(Participant participant)
		{
			var isRead = _readBy.Any(x => x == participant);

			return isRead;
		}
	}
}

