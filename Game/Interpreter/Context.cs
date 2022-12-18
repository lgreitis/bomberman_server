using System;
using GameServices.Enums;
using GameServices.Models.ManagerModels;

namespace GameServices.Interpreter
{
	public class Context
	{
		public GameManager GameManager { get; private set; }
		public string SessionId { get; private set; }
		public string CommandText { get; private set; }

		public ContextCommandType? Type { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsResponseHidden { get; set; }

        public Context(GameManager gameManager, string sessionId, string commandText)
		{
			GameManager = gameManager;
			SessionId = sessionId;
			CommandText = commandText;
		}
	}
}

