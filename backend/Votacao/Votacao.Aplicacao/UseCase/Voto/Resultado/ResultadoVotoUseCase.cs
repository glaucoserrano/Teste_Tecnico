using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votacao.Comunicacao.Response.Restaurante;
using Votacao.Comunicacao.Response.Voto;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Voto.Resultado
{
    public class ResultadoVotoUseCase : IResultadoVotoUseCase
    {
        private readonly IVotoRepository _votoRepository;

        public ResultadoVotoUseCase(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        public async Task<VencedorDiaResponseJson> Execute()
        {
            var restaurante = await _votoRepository.ListarResultadoRestauranteHoje();

            if (restaurante == null)
                return new VencedorDiaResponseJson
                {
                    retorno = false,
                    mensagem = "Nenhum voto registrado hoje",
                    vencedor = null
                };
            return new VencedorDiaResponseJson
            {
                retorno = true,
                mensagem = "Restaurante Escolhido",
                vencedor = restaurante
            };
        }
    }
}
