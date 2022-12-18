using System;
namespace GameServices.Interpreter
{
    public class CommandExpression : Expression
    {
        public override void Interpret(Context context)
        {
            if (string.IsNullOrWhiteSpace(context.CommandText))
            {
                return;
            }

            var key = context.CommandText.Split(' ')[0].ToLower();

            switch (key)
            {
                case "msg":
                    context.Type = Enums.ContextCommandType.PrivateMessage;
                    break;
                case "bomb":
                    context.Type = Enums.ContextCommandType.UseBomb;
                    break;
                case "undo":
                    context.Type = Enums.ContextCommandType.UndoBomb;
                    break;
                case "copy":
                    context.Type = Enums.ContextCommandType.Copy;
                    break;
                case "paste":
                    context.Type = Enums.ContextCommandType.Paste;
                    break;
                default:
                    context.Type = Enums.ContextCommandType.PublicMessage;
                    break;
            }
        }
    }
}

