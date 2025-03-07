using Votacao.Comunicacao.Request.Restaurante;
using Votacao.Comunicacao.Response.Restaurante;

namespace Votacao.Aplicacao.UseCase.Restaurante.Registro
{
    public interface IRegistroRestauranteUseCase
    {
        Task<RegistroRestauranteResponseJson> Execute(RegistroRestauranteRequestJson request);
    }
}
