using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ThomasGregAPI.Model.Entidades;
using ThomasGregAPI.Repository.Data;
using ThomasGregAPI.Repository.Interface;

namespace ThomasGregAPI.Repository.Repository
{
    public class LogradouroRepository : ILogradouroRepository
    {
        public bool AlterarLogradouro(string Email, string Id, string Logradouro, string IdUsuario)
        {
            try
            {
                var Data = new AcessoDb();

                var Param = new SqlParameter[4];
                Param[0] = new SqlParameter("Email", Email);
                Param[1] = new SqlParameter("IdUsuario", IdUsuario);
                Param[2] = new SqlParameter("Id", Id);
                Param[3] = new SqlParameter("NovoValor", Logradouro);

                if (Data.Executar("PROC_UP_LOGRADOURO", Param) > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CadastrarLogradouro(List<LogradouroModel> Logradouro, string Email, string IdUsuario)
        {
            try
            {
                var Data = new AcessoDb();
                var Param = new SqlParameter[3];
                Param[0] = new SqlParameter("Email", Email);
                Param[1] = new SqlParameter("IdUsuario", IdUsuario);

                var XmlModelo = new XmlSerializer(typeof(List<LogradouroModel>));
                using (var XmlFinal = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(XmlFinal))
                    {
                        XmlModelo.Serialize(writer, Logradouro);
                        Param[2] = new SqlParameter("Xml", XmlFinal.ToString());
                        if (Data.Executar("PROC_IN_LOGRADOURO", Param) > 0) return true;
                        else return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LogradouroModel> ConsultarLogradouro(string Email, string IdUsuario, string Id)
        {
            try
            {
                var Data = new AcessoDb();

                SqlParameter[] Param;
                if(Id != "")
                {
                    Param = new SqlParameter[3];
                    Param[0] = new SqlParameter("Email", Email);
                    Param[1] = new SqlParameter("IdUsuario", IdUsuario);
                    Param[2] = new SqlParameter("Id", Id);
                }
                else
                {
                    Param = new SqlParameter[2];
                    Param[0] = new SqlParameter("Email", Email);
                    Param[1] = new SqlParameter("IdUsuario", IdUsuario);
                }
                var SDRLogradouros = Data.Consultar("PROC_SE_LOGRADOURO", Param);
                var Logradouros = new List<LogradouroModel>();

                foreach (DataRow Linha in SDRLogradouros.Rows)
                {
                    Logradouros.Add(new LogradouroModel
                    {
                        Id = Linha[0].ToString(),
                        Logradouro = Linha[1].ToString()
                    });
                }

                return Logradouros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ExcluirLogradouro(string Email, string IdUsuario, string Id)
        {
            try
            {
                var Data = new AcessoDb();

                var Param = new SqlParameter[3];
                Param[0] = new SqlParameter("Email", Email);
                Param[1] = new SqlParameter("IdUsuario", IdUsuario);
                Param[2] = new SqlParameter("Id", Id);

                if(Data.Executar("PROC_DE_LOGRADOURO", Param) > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
