using RestauranteEntidade = Votacao.Dominio.Entities;

namespace Votacao.Comunicacao.Response.Restaurante
{
    public class RestaurantesDisponiveisResponseJson
    {
        public bool retorno { get; set; }
        public string mensagem { get; set; } = string.Empty;
        public List<RestauranteEntidade.Restaurante?>? Restaurantes { get; set; }
    }
}
