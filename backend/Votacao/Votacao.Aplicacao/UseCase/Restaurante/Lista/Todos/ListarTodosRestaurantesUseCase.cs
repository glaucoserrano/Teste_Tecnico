
using RestauranteEntidade = Votacao.Dominio.Entities;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Restaurante.Lista.Todos
{
    public class ListarTodosRestaurantesUseCase : IListarTodosRestaurantesUseCase
    {
        private readonly IRestauranteRepository _restauranteRepository;
public ListarTodosRestaurantesUseCase(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<List<RestauranteEntidade.Restaurante>> Execute()
        {
            var restaurante = await _restauranteRepository.ListarTodosRestaurantes();
            return restaurante;
        }
    }
}
