using Votacao.Comunicacao.Response.Voto;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Voto.VencedorSemana
{
    public class VencedorSemanaUseCase : IVencedorSemanaUseCase
    {
        private readonly IVotoRepository _votoRepository;

        public VencedorSemanaUseCase(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        public async Task<VencedorSemanaResponseJson> Execute()
        {
            var restaurantes = await _votoRepository.ListarVencedoresSemana();

            if (restaurantes == null)
                new VencedorSemanaResponseJson
                {
                    retorno = false,
                    mensagem = "Sem restaurantes vencedores essa semana"
                };

            return new VencedorSemanaResponseJson
            {
                retorno = true,
                mensagem = "Restaurantes vencedores",
                listaVencedor = restaurantes
            };
        }
    }
}
