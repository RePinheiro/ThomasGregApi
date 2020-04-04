using Newtonsoft.Json.Linq;
using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Services.Interface
{
    public interface IClienteService
    {
        RespostaModel CadastrarCliente(JObject json, string IdUsuario);

        RespostaModel ConsultarCliente(string Email, string IdUsuario);

        RespostaModel ConsultarCliente(string IdUsuario);

        RespostaModel AlterarCliente(JObject json, string IdUsuario);

        RespostaModel ExcluirCliente(string Email, string IdUsuario);
    }
}
