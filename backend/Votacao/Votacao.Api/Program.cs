using Microsoft.EntityFrameworkCore;
using Votacao.Aplicacao.UseCase.Restaurante.Editar;
using Votacao.Aplicacao.UseCase.Restaurante.Excluir;
using Votacao.Aplicacao.UseCase.Restaurante.Lista.Disponiveis;
using Votacao.Aplicacao.UseCase.Restaurante.Lista.PorId;
using Votacao.Aplicacao.UseCase.Restaurante.Lista.Todos;
using Votacao.Aplicacao.UseCase.Restaurante.Registro;
using Votacao.Aplicacao.UseCase.Usuario.Editar;
using Votacao.Aplicacao.UseCase.Usuario.Excluir;
using Votacao.Aplicacao.UseCase.Usuario.Lista.PorEmail;
using Votacao.Aplicacao.UseCase.Usuario.Lista.PorId;
using Votacao.Aplicacao.UseCase.Usuario.Lista.Todos;
using Votacao.Aplicacao.UseCase.Usuario.Registro;
using Votacao.Aplicacao.UseCase.Voto.Registro;
using Votacao.Aplicacao.UseCase.Voto.Resultado;
using Votacao.Aplicacao.UseCase.Voto.VencedorSemana;
using Votacao.Dominio.Repositories;
using Votacao.Infraestrutura.DataAcess;
using Votacao.Infraestrutura.DataAcess.Repositories.Restaurante;
using Votacao.Infraestrutura.DataAcess.Repositories.Usuario;
using Votacao.Infraestrutura.DataAcess.Repositories.Voto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

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
builder.Services.AddScoped<IVotoRepository, VotoRepository>();

builder.Services.AddScoped<IRegistroUsuarioUseCase, RegistroUsuarioUseCase>();
builder.Services.AddScoped<IListaTodosUsuariosUseCase, ListaTodosUsuariosUseCase>();
builder.Services.AddScoped<IListarUsuarioPorIdUseCase, ListarUsuarioPorIdUseCase>();
builder.Services.AddScoped<IListarUsuarioPorEmailUseCase, ListarUsuarioPorEmailUseCase>();
builder.Services.AddScoped<IEditarUsuarioUseCase, EditarUsuarioUseCase>();
builder.Services.AddScoped<IExcluirUsuarioUseCase, ExcluirUsuarioUseCase>();

builder.Services.AddScoped<IRegistroRestauranteUseCase, RegistroRestauranteUseCase>();
builder.Services.AddScoped<IListarTodosRestaurantesUseCase, ListarTodosRestaurantesUseCase>();
builder.Services.AddScoped<IListarRestaurantePorIdUseCase, ListarRestaurantePorIdUseCase>();
builder.Services.AddScoped<IListarRestaurantesDisponiveisUseCase, ListarRestaurantesDisponiveisUseCase>();
builder.Services.AddScoped<IEditarRestauranteUseCase, EditarRestauranteUseCase>();
builder.Services.AddScoped<IExcluirRestauranteUseCase, ExcluirRestauranteUseCase>();

builder.Services.AddScoped<IRegistroVotoUseCase, RegistroVotoUseCase>();
builder.Services.AddScoped<IResultadoVotoUseCase, ResultadoVotoUseCase>();
builder.Services.AddScoped<IVencedorSemanaUseCase, VencedorSemanaUseCase>();

var app = builder.Build();
app.UseCors("AllowAll");

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
