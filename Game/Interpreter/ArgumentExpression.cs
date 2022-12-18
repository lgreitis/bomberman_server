using System;
using GameServices.Command;
using GameServices.Mediator;
using Models.WebSocket.Request;

namespace GameServices.Interpreter
{
    public class ArgumentExpression : Expression
    {
        public override void Interpret(Context context)
        {
            if (!context.Type.HasValue)
            {
                return;
            }

            try
            {
                var arguments = context.CommandText.Split(" ");

                switch (context.Type.Value)
                {
                    case Enums.ContextCommandType.PublicMessage:
                        {
                            var participant = context.GameManager.GetChatParticipant(context.SessionId);

                            if (participant == null)
                            {
                                throw new Exception();
                            }

                            context.IsResponseHidden = true;
                            context.IsSuccessful = true;

                            var text = string.Empty;

                            for (var i = 0; i < arguments.Length; i++)
                            {
                                text = $"{text}{arguments[i]} ";
                            }

                            if (string.IsNullOrWhiteSpace(text))
                            {
                                return;
                            }

                            context.GameManager.Log($"[ALL] {participant.Name}: {text}");

                            break;
                        }
                    case Enums.ContextCommandType.PrivateMessage:
                        {
                            if (arguments.Length <= 2)
                            {
                                throw new Exception();
                            }

                            var participant = context.GameManager.GetChatParticipant(context.SessionId);

                            if (participant == null)
                            {
                                throw new Exception();
                            }

                            var recipient = context.GameManager.GetChatParticipantByUsername(arguments[1]);

                            if (recipient == null || recipient == participant)
                            {
                                throw new Exception();
                            }

                            context.IsResponseHidden = true;
                            context.IsSuccessful = true;

                            var text = string.Empty;

                            for (var i = 2; i < arguments.Length; i++)
                            {
                                text = $"{text}{arguments[i]} ";
                            }

                            if (string.IsNullOrWhiteSpace(text))
                            {
                                return;
                            }

                            context.GameManager.Log($"[PRIVATE] {participant.Name}: {text}", recipient);
                            context.GameManager.Log($"Message sent", participant);

                            break;
                        }
                    case Enums.ContextCommandType.UseBomb:
                    case Enums.ContextCommandType.UndoBomb:
                        {
                            var command = new UseBombCommand(context.GameManager.GetPlayer(context.SessionId));

                            if (context.Type == Enums.ContextCommandType.UseBomb)
                            {
                                context.GameManager.InvokeCommand(command);
                            }
                            else
                            {
                                context.GameManager.RevokeCommand(command);
                            }

                            context.IsSuccessful = true;

                            break;
                        }
                    case Enums.ContextCommandType.Copy:
                        {
                            var participant = context.GameManager.GetChatParticipant(context.SessionId);

                            if (participant == null)
                            {
                                throw new Exception();
                            }

                            context.IsResponseHidden = true;
                            context.IsSuccessful = true;
                            var ok = context.GameManager.SaveState();

                            if (!ok)
                            {
                                context.GameManager.Log("Game state is already saved", participant);
                            }
                            else
                            {
                                context.GameManager.Log("Game state has been saved. Use 'paste' to restore it!");
                            }

                            break;
                        }
                    case Enums.ContextCommandType.Paste:
                        {
                            var participant = context.GameManager.GetChatParticipant(context.SessionId);

                            if (participant == null)
                            {
                                throw new Exception();
                            }

                            context.IsResponseHidden = true;
                            context.IsSuccessful = true;
                            var ok = context.GameManager.RestoreState();

                            if (!ok)
                            {
                                context.GameManager.Log("There is no saved game state. Use 'copy' to do it", participant);
                            }
                            else
                            {
                                context.GameManager.Log("Game state has been restored. Use 'copy' to do it again!");
                            }


                            break;
                        }
                    case Enums.ContextCommandType.Block:
                        {
                            if (arguments.Length < 2)
                            {
                                throw new Exception();
                            }

                            var participant = context.GameManager.GetChatParticipant(context.SessionId);

                            if (participant == null)
                            {
                                throw new Exception();
                            }

                            var recipient = context.GameManager.GetChatParticipantByUsername(arguments[1]);

                            if (recipient == null || recipient == participant)
                            {
                                throw new Exception();
                            }

                            context.IsResponseHidden = false;
                            context.IsSuccessful = true;

                            Proxy.Proxy.Instance.Block(arguments[1], context.GameManager.LobbyId);

                            break;
                        }
                }
            }
            catch
            {
                context.IsResponseHidden = false;
                context.IsSuccessful = false;
            }
        }
    }
}

