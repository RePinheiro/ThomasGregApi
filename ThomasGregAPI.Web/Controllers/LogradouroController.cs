using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Http;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Model.Enum;
using ThomasGregAPI.Services.Interface;

namespace ThomasGregAPI.Web.Controllers
{
    public class LogradouroController : ApiController
    {
        private readonly ILogradouroService _logradouroService;
        private readonly ILoginService _loginService;
        public LogradouroController(ILogradouroService logradouroService, ILoginService loginService)
        {
            _logradouroService = logradouroService;
            _loginService = loginService;
        }

        [HttpGet]
        [Route("api/logradouro")]
        public RespostaModel ConsultarLogradouroById(string Email, string Id = "")
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _logradouroService.ConsultarLogradouro(Email, Auth.Conteudo.ToString(), Id);
                    }
                    else return new RespostaModel
                    {
                        Status = StatusResposta.NotFound,
                        Conteudo = "Usuario ou senha não encontrado."
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "Informe o usuário e a senha no cabeçalho."
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespostaModel
                {
                    Status = StatusResposta.Error,
                    Conteudo = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("api/logradouro")]
        public RespostaModel CadastrarLogradouro([FromBody]JObject Json, string Email)
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _logradouroService.CadastrarLogradouro(Json, Email, Auth.Conteudo.ToString());
                    }
                    else return new RespostaModel
                    {
                        Status = StatusResposta.NotFound,
                        Conteudo = "Usuario ou senha não encontrado."
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "Informe o usuário e a senha no cabeçalho."
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespostaModel
                {
                    Status = StatusResposta.Error,
                    Conteudo = ex.Message
                };
            }
        }

        [HttpPut]
        [Route("api/logradouro")]
        public RespostaModel AlterarLogradouro(string id, string Logradouro, string Email)
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _logradouroService.AlterarLogradouro(Email, id, Logradouro, Auth.Conteudo.ToString());
                    }
                    else return new RespostaModel
                    {
                        Status = StatusResposta.NotFound,
                        Conteudo = "Usuario ou senha não encontrado."
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "Informe o usuário e a senha no cabeçalho."
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespostaModel
                {
                    Status = StatusResposta.Error,
                    Conteudo = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("api/logradouro")]
        public RespostaModel DeletarLogradouro(string Email, string Id = "")
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _logradouroService.ExcluirLogradouro(Email, Auth.Conteudo.ToString(), Id);
                    }
                    else return new RespostaModel
                    {
                        Status = StatusResposta.NotFound,
                        Conteudo = "Usuario ou senha não encontrado."
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "Informe o usuário e a senha no cabeçalho."
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespostaModel
                {
                    Status = StatusResposta.Error,
                    Conteudo = ex.Message
                };
            }
        }
    }
}
