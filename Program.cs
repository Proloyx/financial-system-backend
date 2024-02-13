using FinancialSystem.Interfaces;
using FinancialSystem.Services;
using FinancialSystemBackend.Interfaces;
using FinancialSystemBackend.Services;


DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRequest,Request>();
//builder.Services.AddSingleton<IUrlBuilder,UrlBuilder>();
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
