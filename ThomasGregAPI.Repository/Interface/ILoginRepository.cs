using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Repository.Interface
{
    public interface ILoginRepository
    {
        bool AlterarUsuario(string Usuario, string SenhaAntiga, string SenhaNova);
        bool CadastrarUsuario(string Usuario, string Senha);
        int AutenticarUsuario(string Usuario, string Senha);
        bool DeletarUsuario(string ID);
    }
}
