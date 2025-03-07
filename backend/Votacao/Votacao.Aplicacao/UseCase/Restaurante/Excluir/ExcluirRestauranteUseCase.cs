using Votacao.Comunicacao.Response.Restaurante;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Restaurante.Excluir
{
    public class ExcluirRestauranteUseCase : IExcluirRestauranteUseCase
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public ExcluirRestauranteUseCase(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<RegistroRestauranteResponseJson> Execute(int id)
        {
            var restaurante = await _restauranteRepository.ExcluirRestaurante(id);

            if (!restaurante)
                return new RegistroRestauranteResponseJson {
                    retorno = false,
                    mensagem = "Restaurante não encontrado",
                    Restaurante = null
                };

            return new RegistroRestauranteResponseJson
            {
                retorno = true,
                mensagem = "Restaurante excluído com sucesso",
                Restaurante = null
            };
        }
    }
}
