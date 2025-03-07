using Votacao.Comunicacao.Response.Restaurante;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Restaurante.Lista.PorId
{
    public class ListarRestaurantePorIdUseCase : IListarRestaurantePorIdUseCase
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public ListarRestaurantePorIdUseCase(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<RegistroRestauranteResponseJson> Execute(int Id)
        {
            var restaurante = await _restauranteRepository.ListarRestaurantePorId(Id);

            if (restaurante == null)
                return new RegistroRestauranteResponseJson
                {
                    retorno = false,
                    mensagem = "Restaurante não encontrado",
                    Restaurante = null
                };
            return new RegistroRestauranteResponseJson
            {
                retorno = true,
                mensagem = "Restaurante encontrado",
                Restaurante = restaurante
            };
        }
    }
}
