using System;
using Newtonsoft.Json.Linq;
using ThomasGregAPI.Model.Entidade;
using ThomasGregAPI.Services.Interface;
using ThomasGregAPI.Util;
using ThomasGregAPI.Model.Enum;
using ThomasGregAPI.Repository.Interface;
using System.Collections.Generic;
using ThomasGregAPI.Model.Entidades;

namespace ThomasGregAPI.Services.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly Validacao Validacao;

        public ClienteService(IClienteRepository clienteRepository)
        {
            Validacao = new Validacao();
            _clienteRepository = clienteRepository;
        }

        public RespostaModel CadastrarCliente(JObject Json, string IdUsuario)
        {
            try
            {
                if (Json == null) throw new Exception("Está faltando o corpo da requisição.");

                if (Validacao.ValidarJson(Json, TipoJson.CadastroCliente))
                {
                    if (Validacao.ValidarEmail((string)Json["Email"]))
                    {
                        var Logradouros = new List<LogradouroModel>();

                        foreach (var Logradouro in (JArray)Json["Logradouro"])
                        {
                            Logradouros.Add(new LogradouroModel
                            {
                                Logradouro = (string)Logradouro
                            });
                        }

                        var Resposta = _clienteRepository.CadastrarCliente(new ClienteModel
                        {
                            Email = (string)Json["Email"],
                            Nome = (string)Json["Nome"].ToString(),
                            Logotipo = (string)Json["Logotipo"],
                            Logradouro = Logradouros
                        }, IdUsuario);

                        if (Resposta)
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Sucess,
                                Conteudo = "Cliente cadastrado com sucesso."
                            };
                        }
                        else
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Error,
                                Conteudo = "Não foi possivel cadastrar o cliente."
                            };
                        }
                    }
                    else
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.BadRequest,
                            Conteudo = "O email está no formato incorreto."
                        };
                    }
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "O json está no formato incorreto."
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

        public RespostaModel ConsultarCliente(string IdUsuario, string Email)
        {
            try
            {

                if (Validacao.ValidarEmail(Email))
                {
                    var Resposta = _clienteRepository.ConsultarCliente(IdUsuario, Email);

                    if (Resposta != null)
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
                            Conteudo = "Registro não localizado em nossa base de dados."
                        };
                    }
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "O email está no formato incorreto."
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

        public RespostaModel ConsultarCliente(string IdUsuario)
        {
            try
            {
                var Resposta = _clienteRepository.ConsultarCliente(IdUsuario);

                if (Resposta != null)
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
                        Conteudo = "Clientes não localizados em nossa base de dados."
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

        public RespostaModel AlterarCliente(JObject Json, string IdUsuario)
        {
            try
            {
                if (Json == null) throw new Exception("Está faltando o corpo da requisição.");

                if (Validacao.ValidarJson(Json, TipoJson.EdicaoCliente))
                {
                    if (Validacao.ValidarEmail((string)Json["Email"]))
                    {
                        var Resposta = _clienteRepository.AlterarCliente(new ClienteModel
                        {
                            Email = (string)Json["Email"],
                            Nome = (string)Json["Nome"].ToString(),
                            Logotipo = (string)Json["Logotipo"]
                        }, IdUsuario);

                        if (Resposta)
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Sucess,
                                Conteudo = "Cliente alterado com sucesso."
                            };
                        }
                        else
                        {
                            return new RespostaModel
                            {
                                Status = StatusResposta.Error,
                                Conteudo = "Não foi possivel atualizar o cliente."
                            };
                        }
                    }
                    else
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.BadRequest,
                            Conteudo = "O email está no formato incorreto."
                        };
                    }
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "O json está no formato incorreto."
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

        public RespostaModel ExcluirCliente(string Email, string IdUsuario)
        {
            try
            {
                if (Email == null) throw new Exception("Está faltando o parametro Email.");
                if (Validacao.ValidarEmail(Email))
                {
                    var Resposta = _clienteRepository.ExcluirCliente(Email, IdUsuario);

                    if (Resposta)
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.Sucess,
                            Conteudo = "Cliente excluido com sucesso."
                        };
                    }
                    else
                    {
                        return new RespostaModel
                        {
                            Status = StatusResposta.NotFound,
                            Conteudo = "Registro não localizado em nossa base de dados."
                        };
                    }
                }
                else
                {
                    return new RespostaModel
                    {
                        Status = StatusResposta.BadRequest,
                        Conteudo = "O email está no formato incorreto."
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
