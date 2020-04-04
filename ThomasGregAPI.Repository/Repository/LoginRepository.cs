using System;
using System.Data.SqlClient;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Repository.Data;
using ThomasGregAPI.Repository.Interface;

namespace ThomasGregAPI.Repository.Repository
{
    public class LoginRepository : ILoginRepository
    {

        public bool AlterarUsuario(string Usuario, string SenhaAntiga, string SenhaNova)
        {
            try
            {
                var Data = new AcessoDb();
                var Param = new SqlParameter[3];
                Param[0] = new SqlParameter("Usuario", Usuario);
                Param[1] = new SqlParameter("SenhaAntiga", SenhaAntiga);
                Param[2] = new SqlParameter("SenhaNova", SenhaNova);

                if (Data.Executar("PROC_UP_USUARIO", Param) > 0) return true;
                else return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CadastrarUsuario(string Usuario, string Senha)
        {
            try
            {
                var Data = new AcessoDb();
                var Param = new SqlParameter[2];
                Param[0] = new SqlParameter("Usuario", Usuario);
                Param[1] = new SqlParameter("Senha", Senha);

                if (Data.Executar("PROC_IN_USUARIO", Param) > 0) return true;
                else return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int AutenticarUsuario(string Usuario, string Senha)
        {
            try
            {
                var Data = new AcessoDb();

                var Resposta = Data.Consultar("PROC_SE_USUARIO", new SqlParameter("Usuario", Usuario));

                if (Resposta.Rows.Count > 0)
                {
                    if (Resposta.Rows[0][2].ToString() == Senha) return (Int32)Resposta.Rows[0][0];
                    else return 0;
                }
                else return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeletarUsuario(string ID)
        {
            try
            {
                var Data = new AcessoDb();

                if (Data.Executar("PROC_DE_USUARIO", new SqlParameter("IdUsuario", ID)) > 0) return true;
                else return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
