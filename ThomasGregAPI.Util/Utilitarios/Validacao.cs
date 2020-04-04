using System;
using Newtonsoft.Json.Linq;
using ThomasGregAPI.Model.Enum;
using Newtonsoft.Json.Schema;
using System.IO;
using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Util
{
    public class Validacao
    {
        public bool ValidarEmail(string Email)
        {
            if (Email.Contains("@") && Email.Contains(".")) return true;
            else return false;
        }

        public bool ValidarJson(JObject Json, TipoJson Tipojson)
        {
            try
            {
                var appDomain = AppDomain.CurrentDomain;
                JSchema Schema;

                switch (Tipojson)
                {
                    case TipoJson.CadastroCliente:
                        Schema = JSchema.Parse(File.ReadAllText(Path.Combine(appDomain.BaseDirectory, "SchemasJson\\CadastroClienteSchema.json")));
                        break;
                    case TipoJson.EdicaoCliente:
                        Schema = JSchema.Parse(File.ReadAllText(Path.Combine(appDomain.BaseDirectory, "SchemasJson\\EdicaoClienteSchema.json")));
                        break;
                    case TipoJson.CadastroLogradouros:
                        Schema = JSchema.Parse(File.ReadAllText(Path.Combine(appDomain.BaseDirectory, "SchemasJson\\CadastroLogradouroSchema.json")));
                        break;
                    default:
                        Schema = null;
                        break;
                }

                if (Schema != null) return Json.IsValid(Schema);
                else return false;
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Não foi possivel validar o json.");
            }
            catch (DriveNotFoundException)
            {
                throw new Exception("Não foi possivel validar o json.");
            }
            catch (EndOfStreamException)
            {
                throw new Exception("Não foi possivel validar o json.");
            }
            catch (FileLoadException)
            {
                throw new Exception("Não foi possivel validar o json.");
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Não foi possivel validar o json.");
            }
            catch (PathTooLongException)
            {
                throw new Exception("Não foi possivel validar o json.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespostaModel ValidarUsuarioSenha(string Usuario, string Senha)
        {
            try
            {
                string Erros = "";

                if (Usuario.Length < 8) Erros += "\n*Usuário deve ter mais que 7 caracteres";
                if (Usuario.Length > 15) Erros += "\n*Usuário não pode ter mais que 15 caracteres";

                if (Senha.Length < 8) Erros += "\n*Senha deve ter mais que 7 caracteres";
                if (Senha.Length > 15) Erros += "\n*Senha não pode ter mais que 15 caracteres";

                if(Erros.Length == 0)
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.Sucess,
                        Conteudo = "Usuario e senha corretos."
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = Erros
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
