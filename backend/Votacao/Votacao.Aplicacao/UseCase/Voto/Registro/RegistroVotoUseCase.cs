using Votacao.Comunicacao.Request.Voto;
using Votacao.Comunicacao.Response.Voto;
using VotoEntidade = Votacao.Dominio.Entities;
using Votacao.Dominio.Repositories;

namespace Votacao.Aplicacao.UseCase.Voto.Registro
{
    public class RegistroVotoUseCase : IRegistroVotoUseCase
    {
        private readonly IVotoRepository _votoRepository;

        public RegistroVotoUseCase(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        public async Task<RegistroVotoResponseJson> Execute(RegistroVotoRequestJson request)
        {
            var hoje = DateTime.UtcNow.Date;

            //•	Um profissional só pode votar em um restaurante por dia.
            var verificarUsuarioJaVotou = await _votoRepository.UsuarioJaVotou(request.UsuarioId, hoje);

            if(verificarUsuarioJaVotou)
                return new RegistroVotoResponseJson
                {
                    retorno = false,
                    mensagem = "Usuário já votou hoje"
                };

            //•	O mesmo restaurante não pode ser escolhido mais de uma vez durante a semana.
            var restauranteescolhidoSemana = await _votoRepository.RestauranteEscolhidoSemana
                (request.RestauranteId, hoje);

            if(restauranteescolhidoSemana)
                return new RegistroVotoResponseJson
                {
                    retorno = false,
                    mensagem = "Restaurante já escolhido na semana"
                };

            var voto = new VotoEntidade.Voto
            {
                UsuarioId = request.UsuarioId,
                RestauranteId = request.RestauranteId,
                DiaVoto = hoje
            };

            await _votoRepository.RegistrarVoto(voto);

            return new RegistroVotoResponseJson
            {
                retorno = true,
                mensagem = "Voto registrado com sucesso"
            };
        }
    }
}
