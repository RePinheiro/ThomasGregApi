using System.Collections.Generic;
using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Repository.Interface
{
    public interface ILogradouroRepository
    {
        bool AlterarLogradouro(string Email, string Id, string Logradouro, string IdUsuario);

        bool CadastrarLogradouro(List<LogradouroModel> Logradouro, string Email, string IdUsuario);

        List<LogradouroModel> ConsultarLogradouro(string Email, string IdUsuario, string Id);

        bool ExcluirLogradouro(string Email, string IdUsuario, string Id);
    }
}
