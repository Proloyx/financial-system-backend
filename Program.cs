using System.Reflection.Metadata.Ecma335;
using System.Text;
using DotNetEnv;
using FinancialSystem.Interfaces;
using FinancialSystem.Models;
using FinancialSystem.Models.DB.DBModels;
using FinancialSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

// builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt => {
//         var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("Key")));
//         var signingcredentials = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha256Signature);
//         opt.RequireHttpsMetadata = false;

//         opt.TokenValidationParameters = new TokenValidationParameters(){
//                 ValidateAudience = false,
//                 ValidateIssuer = false,
//                 IssuerSigningKey = symmetrickey
//         };        
// });

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Env.GetString("DbConnection")));
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "FSObserver API", Version = "v1" });

//     // Configuración de seguridad para Swagger
//     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//     {
//         Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
//                       Enter 'Bearer' [space] and then your token in the text input below.
//                       \r\n\r\nExample: 'Bearer 12345abcdef'",
//         Name = "Authorization",
//         BearerFormat = "JWT",
//         In = ParameterLocation.Header,
//         Type = SecuritySchemeType.ApiKey,
//         Scheme = "Bearer"
//     });

//     c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//     {
//         {
//             new OpenApiSecurityScheme
//             {
//                 Reference = new OpenApiReference
//                 {
//                     Type = ReferenceType.SecurityScheme,
//                     Id = "Bearer"
//                 },
//                 Scheme = "oauth2",
//                 Name = "Bearer",
//                 In = ParameterLocation.Header,

//             },
//             new List<string>()
//         }
//     });
// });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton<IRequest,Request>();
//builder.Services.AddSingleton(provider => FirestoreDb.Create(Env.GetString("ProjectId")));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder =>
        builder
        .WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader());;

app.UseRouting();
app.MapControllers();

app.Run();
