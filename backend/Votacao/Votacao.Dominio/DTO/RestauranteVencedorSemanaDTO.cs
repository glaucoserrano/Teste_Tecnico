namespace Votacao.Dominio.DTO
{
    public class RestauranteVencedorSemanaDTO
    {
        public DateTime Data {  get; set; }
        public int RestauranteId { get; set; }
        public string RestauranteNome { get; set; } = string.Empty;
        public int TotalVotos { get; set; }
    }
}
