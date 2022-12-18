using Models.WebSocket;
using Models.WebSocket.Request;
using Models.WebSocket.Response;
using Newtonsoft.Json;
using Services.Services;
using Services;
using WebSocketSharp;
using WebSocketSharp.Server;
using GameServices.Singleton;
using GameServices.Command;
using GameServices.Models.ManagerModels;
using GameServices.Interpreter;
using GameServices.Proxy;

namespace GameServer.Behaviours
{
    public class GameBehaviour : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            using (var serviceScope = Resolver.GetScope())
            {
                var _userService = serviceScope.ServiceProvider.GetService<IUserService>();
                ArgumentNullException.ThrowIfNull(_userService);

                var requestCommand = GetRequestCommand(e.Data);

                if (!requestCommand.Success)
                {
                    return;
                }

                try
                {
                    GameManager? gameData = null;
                    bool allMessages = false;

                    switch (requestCommand.CommandId)
                    {
                        case WebSocketCommandId.Connect:
                            {
                                var requestData = GetRequestData<ConnectRequest>(e.Data);

                                GamesManager.Instance.InitializeGameManager(requestData.LobbyId);

                                var user = _userService
                                    .GetQueryable(x => x.LoginToken == requestData.Token)
                                    .Select(x => new
                                    {
                                        x.UserId,
                                        x.Username,
                                        x.LoginToken
                                    })
                                    .Single();

                                /*GamesManager.Instance.RegisterClient(
                                    requestData.LobbyId,
                                    user.Username,
                                    user.UserId,
                                    user.LoginToken ?? string.Empty,
                                    ID);*/

                                try
                                {
                                    Proxy.Instance.Connect(requestData.LobbyId, user.Username, user.UserId, user.LoginToken ?? string.Empty, ID);

                                    gameData = GamesManager.Instance.GetGameManager(ID);

                                    Send(new WebSocketResponse
                                    {
                                        ResponseId = WebSocketResponseId.Map,
                                        Data = new MapResponse
                                        {
                                            Map = gameData.GetMapTiles()
                                        }
                                    });

                                    Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                    {
                                        ResponseId = WebSocketResponseId.Players,
                                        Data = gameData.GetPlayers()
                                    });

                                    allMessages = true;
                                }
                                catch (BlockedException ex)
                                {
                                    Send(new WebSocketResponse { ResponseId = WebSocketResponseId.Blocked });
                                }

                                break;
                            }
                        case WebSocketCommandId.Move:
                            {
                                var requestData = GetRequestData<MoveRequest>(e.Data);
                                gameData = GamesManager.Instance.GetGameManager(ID);

                                var moveX = requestData.PositiveX != requestData.NegativeX;
                                var moveY = requestData.PositiveY != requestData.NegativeY;

                                var command = new MoveCommand(
                                    ID,
                                    moveX ? requestData.PositiveX : null,
                                    moveY ? requestData.PositiveY : null);

                                gameData.InvokeCommand(command);

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Players,
                                    Data = gameData.GetPlayers()
                                });

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.TextureUpdate,
                                    Data = gameData.GetTextures()
                                });

                                break;
                            }
                        case WebSocketCommandId.UseBomb:
                        case WebSocketCommandId.UndoBomb:
                            {
                                gameData = GamesManager.Instance.GetGameManager(ID);

                                var command = new UseBombCommand(gameData.GetPlayer(ID));

                                if (requestCommand.CommandId == WebSocketCommandId.UseBomb)
                                {
                                    gameData.InvokeCommand(command);
                                }
                                else
                                {
                                    gameData.RevokeCommand(command);
                                }

                                // zemiau privalo buti visi broadcastai kaip ir executecommand requeste

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.TextureUpdate,
                                    Data = gameData.GetTextures()
                                });

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Players,
                                    Data = gameData.GetPlayers()
                                });

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Map,
                                    Data = new MapResponse
                                    {
                                        Map = gameData.GetMapTiles()
                                    }
                                });

                                break;
                            }
                        case WebSocketCommandId.ExecuteCommand:
                            {
                                gameData = GamesManager.Instance.GetGameManager(ID);
                                var requestData = GetRequestData<ExecuteCommandRequest>(e.Data);

                                var context = new Context(gameData, ID, requestData.CommandText);
                                var expressions = new List<Expression>
                                {
                                    new CommandExpression(),
                                    new ArgumentExpression()
                                };

                                foreach (var exp in expressions)
                                {
                                    exp.Interpret(context);
                                }

                                if (!context.IsResponseHidden)
                                {
                                    if (context.IsSuccessful)
                                    {
                                        gameData.Log("Command executed successfully", ID);
                                    }
                                    else
                                    {
                                        gameData.Log("Command execution failed", ID);
                                    }
                                }

                                // Zemiau privalo buti visi broadcastai kaip ir usecommand requeste

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.TextureUpdate,
                                    Data = gameData.GetTextures()
                                });

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Players,
                                    Data = gameData.GetPlayers()
                                });

                                Broadcast(gameData.GetSessionIds(), new WebSocketResponse
                                {
                                    ResponseId = WebSocketResponseId.Map,
                                    Data = new MapResponse
                                    {
                                        Map = gameData.GetMapTiles()
                                    }
                                });

                                break;
                            }
                    }

                    if (gameData != null)
                    {
                        var sessionIds = gameData.GetSessionIds();

                        foreach (var sessionId in sessionIds)
                        {
                            var messages = gameData.GetSessionMessages(sessionId, allMessages && sessionId == ID);

                            if (!messages.Any())
                            {
                                continue;
                            }

                            var json = JsonConvert.SerializeObject(new WebSocketResponse
                            {
                                ResponseId = WebSocketResponseId.MessagesUpdate,
                                Data = messages
                            });
                            Sessions.SendTo(json, sessionId);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Send(WebSocketResponse response)
        {
            var json = JsonConvert.SerializeObject(response);

            Send(json);
        }

        private void Broadcast(List<string> sessionIds, WebSocketResponse response)
        {
            var json = JsonConvert.SerializeObject(response);

            foreach (var sessionId in sessionIds.Distinct())
            {
                Sessions.SendTo(json, sessionId);
            }
        }

        private (bool Success, string? CommandId) GetRequestCommand(string request)
        {
            try
            {
                var deserializedRequest = JsonConvert.DeserializeObject<WebSocketRequest>(request);

                if (deserializedRequest != null
                    && WebSocketCommandId.AllCommands.Any(x => x == deserializedRequest.CommandId.ToUpper()))
                {
                    return (true, deserializedRequest.CommandId.ToUpper());
                }

                return (false, null);
            }
            catch
            {
                return (false, null);
            }
        }

        private T GetRequestData<T>(string request) where T : class, IRequestValidation
        {
            var deserializedRequest = JsonConvert.DeserializeObject<WebSocketRequest>(request);

            if (deserializedRequest == null)
            {
                throw new Exception("Could not deserialize request model.");
            }

            var requestData = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(deserializedRequest.Data));

            if (requestData == null || !requestData.IsModelValid())
            {
                throw new Exception("Request model is not valid.");
            }

            return requestData;
        }
    }
}
