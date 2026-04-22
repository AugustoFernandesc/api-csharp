using Microsoft.EntityFrameworkCore;
using MinhaApi.Service;
using MinhaApi.Infrastructure.Data;
using MinhaApi.Domain.Interfaces;
using MinhaApi.Infrastructure.Repositories;
using MinhaApi.Application.UseCases;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MinhaApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//conexao com o banco de dados
//e basicamente o que eu faco no TypeOrmModule.forRoot() no nest
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//isso e a injecao de dependencia (coracao do backend)
//aqui  faz basicamente o seguinte: quando alguem pedir uma interface IEmployeeRepository, entrega a classe EmployeeRepository
//no nest seria os providers, ele faz com que nao precisa colocar NEW para instanciar a classe

builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<AuthService>();

//permite que o front converse com essa api, sem isso o navegador bloqueia
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


//tudo que esta acima deste var app e o que seria no module do nestjs
var app = builder.Build();
//tudo que esta abaixo deste var app define como a requisicao viaja dentro da API

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //É o ambiente de dev? Se sim, liga a documentação.
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
//O cara tentou entrar por HTTP? Joga ele para o HTTPS (seguro).
app.UseHttpsRedirection();
//O cara tem permissão para estar aqui?
app.UseAuthorization();
//Finalmente, manda para o Controller certo.
app.MapControllers();

app.Run();
