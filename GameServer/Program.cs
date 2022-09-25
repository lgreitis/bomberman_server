using GameServer.Behaviours;
using WebSocketSharp.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

var wssv = new WebSocketServer("ws://localhost:5201");
wssv.AddWebSocketService<GameBehaviour>("/Game");
wssv.Start();

app.Run();

wssv.Stop();
