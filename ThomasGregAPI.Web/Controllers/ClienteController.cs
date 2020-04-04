using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Http;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Model.Enum;
using ThomasGregAPI.Services.Interface;

namespace ThomasGregAPI.Web.Controllers
{ 
    public class ClienteController : ApiController
    {
        private readonly IClienteService _clienteService;
        private readonly ILoginService _loginService;
        public ClienteController(IClienteService clienteService, ILoginService loginService)
        {
            _clienteService = clienteService;
            _loginService = loginService;
        }

        
        [HttpGet]
        [Route("api/cliente")]
        public RespostaModel ConsultarCliente()
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _clienteService.ConsultarCliente(Auth.Conteudo.ToString());
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
                        Conteudo = "Informe o usuário no cabeçalho."
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/cliente")]
        public RespostaModel ConsultarClienteByEmail(string Email)
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _clienteService.ConsultarCliente(Auth.Conteudo.ToString(), Email);
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
                        Conteudo = "Informe o usuário no cabeçalho."
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/cliente")]
        public RespostaModel CadastrarCliente([FromBody]JObject Json)
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _clienteService.CadastrarCliente(Json, Auth.Conteudo.ToString());
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
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/cliente")]
        public RespostaModel AlterarCliente([FromBody] JObject Json)
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _clienteService.AlterarCliente(Json, Auth.Conteudo.ToString());
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
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/cliente")]
        public RespostaModel DeletarCliente(string Email)
        {
            try
            {
                if (Request.Headers.Contains("Usuario") && Request.Headers.Contains("Senha"))
                {
                    var Auth = _loginService.AutenticarUsuario(Request.Headers.GetValues("Usuario").First(), Request.Headers.GetValues("Senha").First());
                    if (Auth.Status == StatusResposta.Sucess)
                    {
                        return _clienteService.ExcluirCliente(Email, Auth.Conteudo.ToString());
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
                        Conteudo = "Informe o usuário e senha no cabeçalho."
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
