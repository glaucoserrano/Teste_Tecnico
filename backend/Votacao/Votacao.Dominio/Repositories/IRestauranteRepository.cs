using Votacao.Dominio.Entities;

namespace Votacao.Dominio.Repositories
{
    public interface IRestauranteRepository
    {
        Task<bool> VerificarRestauranteExiste(string nome);
        Task AdicionarRestaurante(Restaurante restaurante);
        Task<List<Restaurante>> ListarTodosRestaurantes();
        Task<Restaurante> ListarRestaurantePorId(int id);
        Task<bool> EditarRestaurante(int id, Restaurante restaurante);
        Task<bool> ExcluirRestaurante(int id);
        Task<List<Restaurante?>> ListarRestaurantesDisponiveis();
    }
}
