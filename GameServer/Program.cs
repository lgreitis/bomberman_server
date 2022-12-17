using GameServer.Behaviours;
using Services;
using WebSocketSharp.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var policy = "_policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

Registrator.Register(builder.Services);

var app = builder.Build();
Resolver.Configure(app.Services);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseAuthorization();
app.MapControllers();
app.UseCors(policy);

var wssv = new WebSocketServer("ws://192.168.0.153:5201");
wssv.AddWebSocketService<GameBehaviour>("/Game");
wssv.AddWebSocketService<LobbyBehaviour>("/Lobby");
wssv.Start();

app.Run();

wssv.Stop();
