using Votacao.Dominio.DTO;
using Votacao.Dominio.Entities;


namespace Votacao.Dominio.Repositories
{
    public interface IVotoRepository
    {
        Task<bool> UsuarioJaVotou(int UsuarioId, DateTime hoje);
        Task<bool> RestauranteEscolhidoSemana(int RestauranteId, DateTime hoje);
        Task RegistrarVoto(Voto voto);

        Task<VencedorHojeDTO?> ListarResultadoRestauranteHoje();
        Task<List<RestauranteVencedorSemanaDTO>> ListarVencedoresSemana();
    }
}
