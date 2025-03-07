namespace Votacao.Dominio.Entities
{
    public class Voto
    {
        public int id { get; set; }
        public required Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public required Restaurante Restaurante { get; set; }
        public int RestauranteId { get; set; }
        public DateTime DiaVoto { get; set; } = DateTime.UtcNow;
    }
}
