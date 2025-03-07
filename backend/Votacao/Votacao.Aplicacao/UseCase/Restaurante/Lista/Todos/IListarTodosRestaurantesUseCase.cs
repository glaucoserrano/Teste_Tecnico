using RestauranteEntidade =  Votacao.Dominio.Entities;

namespace Votacao.Aplicacao.UseCase.Restaurante.Lista.Todos
{
    public interface IListarTodosRestaurantesUseCase
    {
        Task<List<RestauranteEntidade.Restaurante>> Execute();
    }
}
