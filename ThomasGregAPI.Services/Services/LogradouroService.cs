using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ThomasGregAPI.Model.Entidade;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Model.Enum;
using ThomasGregAPI.Repository.Interface;
using ThomasGregAPI.Services.Interface;
using ThomasGregAPI.Util;

namespace ThomasGregAPI.Services.Services
{
    public class LogradouroService : ILogradouroService
    {
        private readonly ILogradouroRepository _logradouroRepository;
        private readonly Validacao Validacao;
        public LogradouroService(ILogradouroRepository logradouroRepository)
        {
            _logradouroRepository = logradouroRepository;
            Validacao = new Validacao();
        }

        public RespostaModel AlterarLogradouro(string Email, string Id, string Logradouro, string IdUsuario)
        {
            try
            {
                //if (Validacao.ValidarJson(Json, TipoJson.Edicao))
                //{
                    if (Validacao.ValidarEmail(Email))
                    {

                    var Resposta = _logradouroRepository.AlterarLogradouro(Email, Id, Logradouro, IdUsuario);

                        if (Resposta)
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Sucess,
                                Conteudo = "Logradouro alterado com sucesso"
                            };
                        }
                        else
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Error,
                                Conteudo = "Não foi possivel alterar o logradouro."
                            };
                        }
                    }
                    else
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.BadRequest,
                            Conteudo = "O Email está no formato incorreto."
                        };
                    }
                //}
                //else
                //{
                //    return new RespostaModel
                //    {
                //        Status = StatusResposta.BadRequest,
                //        Conteudo = "O Json está no formato incorreto."
                //    };
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespostaModel CadastrarLogradouro(JObject Json, string Email, string IdUsuario)
        {
            try
            {
                if (Validacao.ValidarJson(Json, TipoJson.CadastroLogradouros))
                {
                    if (Validacao.ValidarEmail(Email))
                    {
                        var Logradouros = new List<LogradouroModel>();

                        foreach (var Logradouro in (JArray)Json["Logradouro"])
                        {
                            Logradouros.Add(new LogradouroModel
                            {
                                Logradouro = (string)Logradouro
                            });
                        }

                        var Resposta = _logradouroRepository.CadastrarLogradouro(Logradouros, Email, IdUsuario);

                        if (Resposta)
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Sucess,
                                Conteudo = "Logradouro cadastrado com sucesso."
                            };
                        }
                        else
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Error,
                                Conteudo = "Não foi possivel cadastrar o logradouro."
                            };
                        }
                    }
                    else
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.BadRequest,
                            Conteudo = "O Email está no formato incorreto."
                        };
                    }
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "O Json está no formato incorreto."
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespostaModel ConsultarLogradouro(string Email, string IdUsuario, string Id)
        {
            try
            {
                if (Validacao.ValidarEmail(Email))
                {
                    var Resposta = _logradouroRepository.ConsultarLogradouro(Email, IdUsuario, Id);

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
                        Status = StatusResposta.BadRequest,
                        Conteudo = "O Email está no formato incorreto."
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RespostaModel ExcluirLogradouro(string Email, string IdUsuario, string Id)
        {
            try
            {
                if (Validacao.ValidarEmail(Email))
                {

                    var Resposta = _logradouroRepository.ExcluirLogradouro(Email, IdUsuario, Id);

                    return new RespostaModel
                    {
                        Status = StatusResposta.Sucess,
                        Conteudo = "Logradouro excluido com sucesso."
                    };
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "O Email está no formato incorreto."
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
