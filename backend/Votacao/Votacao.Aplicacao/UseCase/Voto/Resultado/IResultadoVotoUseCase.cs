using Votacao.Comunicacao.Response.Voto;

namespace Votacao.Aplicacao.UseCase.Voto.Resultado
{
    public interface IResultadoVotoUseCase
    {
        Task<VencedorDiaResponseJson> Execute();
    }
}
