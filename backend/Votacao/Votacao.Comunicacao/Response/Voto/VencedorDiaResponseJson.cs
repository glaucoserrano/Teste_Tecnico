using Votacao.Dominio.DTO;

namespace Votacao.Comunicacao.Response.Voto
{
    public class VencedorDiaResponseJson
    {
        public bool retorno { get; set; }
        public string mensagem { get; set; } = string.Empty;
       
        public VencedorHojeDTO? vencedor { get; set; }
    }
}
