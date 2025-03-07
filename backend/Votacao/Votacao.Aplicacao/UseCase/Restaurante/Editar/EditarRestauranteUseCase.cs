using Votacao.Comunicacao.Response.Restaurante;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Restaurante.Editar
{
    public class EditarRestauranteUseCase : IEditarRestauranteUseCase
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public EditarRestauranteUseCase(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<RegistroRestauranteResponseJson> Execute(int Id, Dominio.Entities.Restaurante restaurante)
        {
            var restauranteExiste = await _restauranteRepository.EditarRestaurante(Id, restaurante);

            if (!restauranteExiste)
                return new RegistroRestauranteResponseJson
                {
                    retorno = false,
                    mensagem = "Restaurante não encontrado",
                    Restaurante = null
                };
            var restauranteEditado = await _restauranteRepository.ListarRestaurantePorId(Id);
            return new RegistroRestauranteResponseJson
            {
                retorno = true,
                mensagem = "Restaurante editado com sucesso",
                Restaurante = restauranteEditado
            };
        }
    }
}
