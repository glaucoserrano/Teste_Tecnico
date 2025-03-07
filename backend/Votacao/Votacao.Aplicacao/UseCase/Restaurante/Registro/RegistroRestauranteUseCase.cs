using Votacao.Comunicacao.Request.Restaurante;
using Votacao.Comunicacao.Response.Restaurante;
using RestauranteEntidade = Votacao.Dominio.Entities;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Restaurante.Registro
{
    public class RegistroRestauranteUseCase : IRegistroRestauranteUseCase
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public RegistroRestauranteUseCase(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<RegistroRestauranteResponseJson> Execute(RegistroRestauranteRequestJson request)
        {
            var restauranteExiste = await _restauranteRepository.VerificarRestauranteExiste(request.Nome);

            if (!restauranteExiste)
                return new RegistroRestauranteResponseJson
                {
                    retorno = false,
                    mensagem = "Restaurante já cadastrado",
                    Restaurante = null
                };

            var restaurante = new RestauranteEntidade.Restaurante
            {
                Nome = request.Nome
            };

            await _restauranteRepository.AdicionarRestaurante(restaurante);

            return new RegistroRestauranteResponseJson
            {
                retorno = true,
                mensagem = "Restaurante cadastrado com sucesso",
                Restaurante = restaurante
            };
        }
    }
}
