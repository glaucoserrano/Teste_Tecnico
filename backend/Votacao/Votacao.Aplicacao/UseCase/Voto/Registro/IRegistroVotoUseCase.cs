using Votacao.Comunicacao.Request.Voto;
using Votacao.Comunicacao.Response.Voto;

namespace Votacao.Aplicacao.UseCase.Voto.Registro
{
    public interface IRegistroVotoUseCase
    {
        Task<RegistroVotoResponseJson> Execute (RegistroVotoRequestJson request);
    }
}
