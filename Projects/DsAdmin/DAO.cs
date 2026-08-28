using System;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class DAO
    {
        #if DEBUG
        SqlConnection conn = new System.Data.SqlClient.SqlConnection(BuildConnectionString("sql.servidor.producao"));
#else
        SqlConnection conn = new System.Data.SqlClient.SqlConnection(BuildConnectionString("sql.servidor"));
        #endif

        private static string BuildConnectionString(string dataSource)
        {
            string password = Environment.GetEnvironmentVariable("SIOF_DB_PASSWORD");
            return $"Data Source={dataSource};Initial Catalog=transparencia;User ID=utransparencia;Password='{password}';";
        }

        SqlTransaction transaction;
        public DAO()
        {
            conn.Open();
            transaction = conn.BeginTransaction();
        }

        public DbCommand InicializaProcedure(String procedure)
        {

            DbCommand cmd = new System.Data.SqlClient.SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Transaction = transaction;

            return cmd;
        }

        public DataSet ConsultaQueryDataSet(DbCommand comando, String tabela)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter((SqlCommand)comando);
            da.Fill(ds);
            ds.Tables[0].TableName = tabela;
            return ds;
        }

        public void ExecutaNonQuery(DbCommand commando)
        {
            try
            {
                //'commando.Connection.Open()

                commando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //'MsgBox("Erro executando comando." & vbNewLine & ex.Message)
                throw ex;
            }
            finally
            {

                //'commando.Connection.Close()
            }

        }

        public void Fechar()
        {
            conn.Close();
        }

        public void Commit()
        {
            if (transaction.Connection != null)
            {
                transaction.Commit();
            }
        }

        public void Rollback()
        {
            if (transaction.Connection != null)
            {
                transaction.Rollback();
            }
        }
    }
}
