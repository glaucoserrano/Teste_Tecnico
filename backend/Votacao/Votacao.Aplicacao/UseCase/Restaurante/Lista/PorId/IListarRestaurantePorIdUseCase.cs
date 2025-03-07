using Votacao.Comunicacao.Response.Restaurante;

namespace Votacao.Aplicacao.UseCase.Restaurante.Lista.PorId
{
    public interface IListarRestaurantePorIdUseCase
    {
        Task<RegistroRestauranteResponseJson> Execute(int Id);
    }
}
