using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votacao.Comunicacao.Response.Restaurante;
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

        public async Task<RegistroRestauranteResponseJson> Execute()
        {
            var restaurante = await _votoRepository.ListarResultadoRestauranteHoje();

            if (restaurante == null)
                return new RegistroRestauranteResponseJson
                {
                    retorno = false,
                    mensagem = "Nenhum voto registrado hoje",
                    Restaurante = null
                };
            return new RegistroRestauranteResponseJson
            {
                retorno = true,
                mensagem = "Restaurante Escolhido",
                Restaurante = restaurante
            };
        }
    }
}
