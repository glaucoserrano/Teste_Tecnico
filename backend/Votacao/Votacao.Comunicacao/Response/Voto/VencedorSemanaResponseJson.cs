using Votacao.Dominio.DTO;

namespace Votacao.Comunicacao.Response.Voto
{
    public class VencedorSemanaResponseJson
    {
        public bool retorno { get; set; }
        public string mensagem { get; set; } = string.Empty;

        public List<RestauranteVencedorSemanaDTO>? listaVencedor { get; set; }
    }
}
