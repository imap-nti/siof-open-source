using System;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class StatusPublicacao
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public StatusPublicacao()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public StatusPublicacao(DAO conexao)
        {
            neo = conexao;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarStatusPublicacao");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_status_publicacao");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_status_publicacao"].DefaultView;
        }
    }
}