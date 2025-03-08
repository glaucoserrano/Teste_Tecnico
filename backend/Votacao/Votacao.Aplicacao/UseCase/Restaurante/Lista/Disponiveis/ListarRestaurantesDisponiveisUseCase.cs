using Votacao.Comunicacao.Response.Restaurante;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Restaurante.Lista.Disponiveis
{
    public class ListarRestaurantesDisponiveisUseCase : IListarRestaurantesDisponiveisUseCase
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public ListarRestaurantesDisponiveisUseCase(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<RestaurantesDisponiveisResponseJson> Execute()
        {
            var restaurantes = await _restauranteRepository.ListarRestaurantesDisponiveis();

            if (restaurantes == null)
                new RestaurantesDisponiveisResponseJson
                {
                    retorno= false,
                    mensagem = "Nenhum restaurante disponivel!",
                    Restaurantes = null
                };

            return new RestaurantesDisponiveisResponseJson
            {
                retorno = true,
                mensagem = "Restaurantes",
                Restaurantes = restaurantes
            };
        }
    }
}
