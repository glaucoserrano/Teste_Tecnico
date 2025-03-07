using Microsoft.EntityFrameworkCore;
using Votacao.Aplicacao.UseCase.Restaurante.Editar;
using Votacao.Aplicacao.UseCase.Restaurante.Excluir;
using Votacao.Aplicacao.UseCase.Restaurante.Lista.PorId;
using Votacao.Aplicacao.UseCase.Restaurante.Lista.Todos;
using Votacao.Aplicacao.UseCase.Restaurante.Registro;
using Votacao.Aplicacao.UseCase.Usuario.Editar;
using Votacao.Aplicacao.UseCase.Usuario.Excluir;
using Votacao.Aplicacao.UseCase.Usuario.Lista.PorId;
using Votacao.Aplicacao.UseCase.Usuario.Lista.Todos;
using Votacao.Aplicacao.UseCase.Usuario.Registro;
using Votacao.Dominio.Repositories;
using Votacao.Infraestrutura.DataAcess;
using Votacao.Infraestrutura.DataAcess.Repositories.Restaurante;
using Votacao.Infraestrutura.DataAcess.Repositories.Usuario;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VotacaoDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injecao de Dependecias
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRestauranteRepository, RestauranteRepository>();
builder.Services.AddScoped<IRegistroUsuarioUseCase, RegistroUsuarioUseCase>();
builder.Services.AddScoped<IListaTodosUsuariosUseCase, ListaTodosUsuariosUseCase>();
builder.Services.AddScoped<IListarUsuarioPorIdUseCase, ListarUsuarioPorIdUseCase>();
builder.Services.AddScoped<IEditarUsuarioUseCase, EditarUsuarioUseCase>();
builder.Services.AddScoped<IExcluirUsuarioUseCase, ExcluirUsuarioUseCase>();

builder.Services.AddScoped<IRegistroRestauranteUseCase, RegistroRestauranteUseCase>();
builder.Services.AddScoped<IListarTodosRestaurantesUseCase, ListarTodosRestaurantesUseCase>();
builder.Services.AddScoped<IListarRestaurantePorIdUseCase, ListarRestaurantePorIdUseCase>();
builder.Services.AddScoped<IEditarRestauranteUseCase, EditarRestauranteUseCase>();
builder.Services.AddScoped<IExcluirRestauranteUseCase, ExcluirRestauranteUseCase>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
