using Torrent.Chat.Api.Storage.DBAccess;
using Torrent.Chat.Api.Storage.DbRepository;
using Torrent.Chat.Api.Storage.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins("https://www.freetor.ru", "https://freetor.ru", "http://www.freetor.ru", "http://freetor.ru", "http://localhost:5111")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext>();
builder.Services.AddScoped<MsgRepository>();
var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello chat");

app.MapGet("/chat", async (MsgRepository rep) =>
{
    var data =  await rep.GetData<EntityChat>();
    return Results.Ok(data);
});



app.Run();

