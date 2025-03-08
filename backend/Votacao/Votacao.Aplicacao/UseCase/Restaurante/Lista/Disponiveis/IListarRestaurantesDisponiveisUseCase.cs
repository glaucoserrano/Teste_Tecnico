using Votacao.Comunicacao.Response.Restaurante;

namespace Votacao.Aplicacao.UseCase.Restaurante.Lista.Disponiveis
{
    public interface IListarRestaurantesDisponiveisUseCase
    {
        Task<RestaurantesDisponiveisResponseJson> Execute();
    }
}
