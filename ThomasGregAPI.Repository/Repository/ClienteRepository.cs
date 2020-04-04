using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ThomasGregAPI.Model.Entidade;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Repository.Data;
using ThomasGregAPI.Repository.Interface;

namespace ThomasGregAPI.Repository.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        public bool CadastrarCliente(ClienteModel Cliente, string IdUsuario)
        {
            try
            {
                var Data = new AcessoDb();
                var Param = new SqlParameter[2];
                Param[0] = new SqlParameter("IdUsuario", IdUsuario);

                var XmlModelo = new XmlSerializer(typeof(ClienteModel));
                using (var XmlFinal = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(XmlFinal))
                    {
                        XmlModelo.Serialize(writer, Cliente);
                        Param[1] = new SqlParameter("Xml", XmlFinal.ToString().Replace("utf-16","utf-8"));
                        if (Data.Executar("PROC_IN_CLIENTE", Param) > 0) return true;
                        else return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ClienteModel> ConsultarCliente(string IdUsuario, string Email = "")
        {
            try
            {
                var Clientes = new List<ClienteModel>();
                var Data = new AcessoDb();


                DataTable SDRCliente;
                if (Email != "")
                {
                    var param = new SqlParameter[2];
                    param[0] = new SqlParameter("IdUsuario", IdUsuario);
                    param[1] = new SqlParameter("Email", Email);
                    SDRCliente = Data.Consultar("PROC_SE_CLIENTE", param);
                }
                else
                {
                    SDRCliente = Data.Consultar("PROC_SE_CLIENTE", new SqlParameter("IdUsuario", IdUsuario));
                }

                foreach (DataRow linha in SDRCliente.Rows)
                {
                    var param = new SqlParameter[2];
                    param[0] = new SqlParameter("IdUsuario", IdUsuario);
                    param[1] = new SqlParameter("Email", linha[0].ToString());

                    var SDRLogradouros = Data.Consultar("PROC_SE_LOGRADOURO", param);
                    var Logradouros = new List<LogradouroModel>();

                    foreach (DataRow Linha in SDRLogradouros.Rows)
                    {
                        Logradouros.Add(new LogradouroModel
                        {
                            Id = Linha[0].ToString(),
                            Logradouro = Linha[1].ToString()
                        });
                    }

                    Clientes.Add(new ClienteModel
                    {
                        Email = SDRCliente.Rows[0][0].ToString(),
                        Nome = SDRCliente.Rows[0][2].ToString(),
                        Logotipo = SDRCliente.Rows[0][3].ToString(),
                        Logradouro = Logradouros
                    });
                }

                return Clientes;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AlterarCliente(ClienteModel Cliente, string IdUsuario)
        {
            try
            {
                var Data = new AcessoDb();

                SqlParameter[] Param;

                if (Cliente.Logotipo != "" && Cliente.Nome == "")
                {
                    Param = new SqlParameter[2];
                    Param[0] = new SqlParameter("Email", Cliente.Email);
                    Param[1] = new SqlParameter("Logotipo", Cliente.Logotipo);
                }
                else if (Cliente.Logotipo != "" && Cliente.Nome == "")
                {
                    Param = new SqlParameter[2];
                    Param[0] = new SqlParameter("Email", Cliente.Email);
                    Param[1] = new SqlParameter("Nome", Cliente.Nome);
                }
                else if(Cliente.Logotipo != "" && Cliente.Nome != "")
                {
                    Param = new SqlParameter[3];
                    Param[0] = new SqlParameter("Email", Cliente.Email);
                    Param[1] = new SqlParameter("Nome", Cliente.Nome);
                    Param[2] = new SqlParameter("Logotipo", Cliente.Logotipo);
                }
                else
                {
                    throw new Exception("Escolha um campo para editar Logotipo ou Nome");
                }

                if (Data.Executar("PROC_UP_CLIENTE", Param) > 0) return true;
                else return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ExcluirCliente(string Email, string IdUsuario)
        {
            try
            {
                var Data = new AcessoDb();

                var Param = new SqlParameter[2];
                Param[0] = new SqlParameter("Email", Email);
                Param[1] = new SqlParameter("IdUsuario", IdUsuario);

                if (Data.Executar("PROC_DE_CLIENTE", Param) > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
