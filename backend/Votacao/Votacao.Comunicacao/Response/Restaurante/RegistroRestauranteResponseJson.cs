using RestauranteEntidade = Votacao.Dominio.Entities;

namespace Votacao.Comunicacao.Response.Restaurante
{
    public class RegistroRestauranteResponseJson
    {
        public bool retorno { get; set; }
        public string mensagem { get; set; } = string.Empty;
        public RestauranteEntidade.Restaurante? Restaurante { get; set; }

    }
}
