using UsuarioEntidade = Votacao.Dominio.Entities;

namespace Votacao.Comunicacao.Response.Usuario
{
    public class RegistroUsuarioResponseJson
    {
        public bool retorno { get; set; }
        public string mensagem { get; set; } = string.Empty;
        public UsuarioEntidade.Usuario? Usuario { get; set; }

    }
}
