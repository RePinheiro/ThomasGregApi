using System.Collections.Generic;
using ThomasGregAPI.Model.Entidade;

namespace ThomasGregAPI.Repository.Interface
{
    public interface IClienteRepository
    {
        bool CadastrarCliente(ClienteModel Cliente, string IdUsuario);

        List<ClienteModel> ConsultarCliente(string IdUsuario, string Email = "");

        bool AlterarCliente(ClienteModel json, string IdUsuario);

        bool ExcluirCliente(string Email, string IdUsuario);
    }
}
