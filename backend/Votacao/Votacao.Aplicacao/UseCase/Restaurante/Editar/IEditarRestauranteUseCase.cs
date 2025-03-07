using Votacao.Comunicacao.Response.Restaurante;
using RestauranteEntidade = Votacao.Dominio.Entities;

namespace Votacao.Aplicacao.UseCase.Restaurante.Editar
{
    public interface IEditarRestauranteUseCase
    {
        Task<RegistroRestauranteResponseJson> Execute(int Id, RestauranteEntidade.Restaurante restaurante);
    }
}
