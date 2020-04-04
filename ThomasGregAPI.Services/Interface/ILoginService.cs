using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Services.Interface
{
    public interface ILoginService
    {
        RespostaModel AlterarUsuario(string Usuario, string SenhaAntiga, string SenhaNova);
        RespostaModel CadastrarUsuario(string Usuario, string Senha);
        RespostaModel AutenticarUsuario(string Usuario, string Senha);
        RespostaModel DeletarUsuario(string ID);
    }
}
