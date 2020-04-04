using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ThomasGregAPI.Repository.Data
{
    public class AcessoDb
    {
        static public string ConnectionString
        {
            get
            {    // pega a string de conexão do web.config
                return ConfigurationManager.ConnectionStrings["DbEmpresa"].ToString();
            }
        }

        public int Executar(string Procedure, string Xml)
        {
            try
            {
                using (var CnnSql = new SqlConnection(ConnectionString))
                {
                    CnnSql.Open();
                    using (var Cmd = new SqlCommand(Procedure, CnnSql))
                    {
                        Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("XML", Xml);
                        return Cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw new Exception("Erro inesperado.");
            }
        }

        public int Executar(string Procedure, SqlParameter Param)
        {
            try
            {
                using (var CnnSql = new SqlConnection(ConnectionString))
                {
                    CnnSql.Open();
                    using (var Cmd = new SqlCommand(Procedure, CnnSql))
                    {
                        Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Cmd.Parameters.Add(Param);
                        return Cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw new Exception("Erro inesperado.");
            }
        }

        public int Executar(string Procedure, SqlParameter[] Param)
        {
            try
            {
                using (var CnnSql = new SqlConnection(ConnectionString))
                {
                    CnnSql.Open();
                    using (var Cmd = new SqlCommand(Procedure, CnnSql))
                    {
                        Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Cmd.Parameters.AddRange(Param);
                        return Cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Consultar(string Procedure, SqlParameter Param)
        {
            try
            {
                using (var CnnSql = new SqlConnection(ConnectionString))
                {
                    CnnSql.Open();
                    using (var Cmd = new SqlCommand(Procedure, CnnSql))
                    {
                        Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if(Param.Value.ToString() != "") Cmd.Parameters.Add(Param);
                        var dt = new DataTable();
                        dt.Load(Cmd.ExecuteReader(CommandBehavior.CloseConnection));
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Consultar(string Procedure, SqlParameter[] Param)
        {
            try
            {
                using (var CnnSql = new SqlConnection(ConnectionString))
                {
                    CnnSql.Open();
                    using (var Cmd = new SqlCommand(Procedure, CnnSql))
                    {
                        Cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (Param.Length > 0) Cmd.Parameters.AddRange(Param);
                        var dt = new DataTable();
                        dt.Load(Cmd.ExecuteReader(CommandBehavior.CloseConnection));
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
