using Votacao.Dominio.Entities;

namespace Votacao.Dominio.Repositories
{
    public interface IRestauranteRepository
    {
        Task<bool> VerificarRestauranteExiste(string nome);
        Task AdicionarRestaurante(Restaurante restaurante);
    }
}
