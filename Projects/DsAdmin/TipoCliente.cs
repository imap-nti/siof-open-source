using System;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class TipoCliente
    {
                private DAO neo;

        private Boolean sessaoInterna = false;

        public TipoCliente()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public TipoCliente(DAO conexao)
        {
            neo = conexao;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarTipoCliente");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_TIPOCLIENTE");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_TIPOCLIENTE"].DefaultView;
        }

        public DataView Consultar(Int32 COD_TIPOCLIENTE)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultartarTipoCliente");
            _Command.Parameters.AddWithValue("@COD_TIPOCLIENTE", COD_TIPOCLIENTE == 0 ? Convert.DBNull : COD_TIPOCLIENTE);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "COD_TIPOCLIENTE");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_TIPOCLIENTE"].DefaultView;
        }
    }
}
