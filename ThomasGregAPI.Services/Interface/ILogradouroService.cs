using Newtonsoft.Json.Linq;
using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Services.Interface
{
    public interface ILogradouroService
    {
        RespostaModel AlterarLogradouro(string Email, string Id, string Logradouro, string IdUsuario);

        RespostaModel CadastrarLogradouro(JObject json, string Email, string IdUsuario);

        RespostaModel ConsultarLogradouro(string Email, string IdUsuario, string Id);

        RespostaModel ExcluirLogradouro(string Email, string IdUsuario, string Id);
    }
}
