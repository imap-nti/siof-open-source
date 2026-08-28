using System;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Uf
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Uf()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Uf(DAO conexao)
        {
            neo = conexao;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarUf");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_Uf");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_Uf"].DefaultView;
        }
    }
}
