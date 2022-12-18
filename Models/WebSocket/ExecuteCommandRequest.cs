using System;
using Models.WebSocket.Request;

namespace Models.WebSocket
{
	public class ExecuteCommandRequest : IRequestValidation
    {
        public string CommandText { get; set; } = string.Empty;

        public bool IsModelValid()
        {
            return !string.IsNullOrEmpty(CommandText);
        }
    }
}

