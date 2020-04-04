using System;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Model.Enum;
using ThomasGregAPI.Repository.Interface;
using ThomasGregAPI.Services.Interface;
using ThomasGregAPI.Util;

namespace ThomasGregAPI.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly Validacao Validacao;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            Validacao = new Validacao();
        }

        public RespostaModel AlterarUsuario(string Usuario, string SenhaAntiga, string SenhaNova)
        {
            try
            {
                var ValidaUsuarioSenha = Validacao.ValidarUsuarioSenha(Usuario, SenhaNova);
                if (ValidaUsuarioSenha.Status == StatusResposta.Sucess)
                {
                    var Resposta = _loginRepository.AlterarUsuario(Usuario, SenhaAntiga, SenhaNova);

                    if (Resposta)
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.Sucess,
                            Conteudo = "Usuario alterado com sucesso."
                        };
                    }
                    else
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.Error,
                            Conteudo = "Não foi possivel atualizar o usuario."
                        };
                    }
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = ValidaUsuarioSenha.Status,
                        Conteudo = ValidaUsuarioSenha.Conteudo
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespostaModel CadastrarUsuario(string Usuario, string Senha)
        {
            try
            {
                var RespostaValidacao = Validacao.ValidarUsuarioSenha(Usuario, Senha);
                if (RespostaValidacao.Status == StatusResposta.Sucess)
                {
                    if (_loginRepository.AutenticarUsuario(Usuario, Senha) == 0)
                    {
                        var Resposta = _loginRepository.CadastrarUsuario(Usuario, Senha);

                        if (Resposta)
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Sucess,
                                Conteudo = "Usuario cadastrado com sucesso."
                            };
                        }
                        else
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Error,
                                Conteudo = "Não foi possivel cadastrar o usuario."
                            };
                        }
                    }
                    else
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.Error,
                            Conteudo = "Usuário já cadastrado em nossa base de dados."
                        };
                    }
                }
                else return RespostaValidacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespostaModel AutenticarUsuario(string Usuario, string Senha)
        {
            try
            {
                var Resposta = _loginRepository.AutenticarUsuario(Usuario, Senha);

                if (Resposta != 0)
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.Sucess,
                        Conteudo = Resposta
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.NotFound,
                        Conteudo = "Usuário ou senha incorreto."
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespostaModel DeletarUsuario(string ID)
        {
            try
            {
                var Resposta = _loginRepository.DeletarUsuario(ID);

                if (Resposta)
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.Sucess,
                        Conteudo = "Usuario deletado com sucesso."
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.Error,
                        Conteudo = "Não foi possivel deletar o usuario."
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
