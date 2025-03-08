using Votacao.Comunicacao.Response.Voto;

namespace Votacao.Aplicacao.UseCase.Voto.VencedorSemana
{
    public interface IVencedorSemanaUseCase
    {
        Task<VencedorSemanaResponseJson> Execute();
    }
}
