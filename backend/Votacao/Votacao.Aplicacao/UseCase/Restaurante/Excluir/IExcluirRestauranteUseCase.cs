using Votacao.Comunicacao.Response.Restaurante;

namespace Votacao.Aplicacao.UseCase.Restaurante.Excluir
{
    public interface IExcluirRestauranteUseCase
    {
        Task<RegistroRestauranteResponseJson> Execute(int id);
    }
}
