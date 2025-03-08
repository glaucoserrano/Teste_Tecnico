using Votacao.Comunicacao.Response.Restaurante;

namespace Votacao.Aplicacao.UseCase.Voto.Resultado
{
    public interface IResultadoVotoUseCase
    {
        Task<RegistroRestauranteResponseJson> Execute();
    }
}
