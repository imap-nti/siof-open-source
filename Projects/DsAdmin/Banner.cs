using System;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Banner
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Banner()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Banner(DAO conexao)
        {
            neo = conexao;
        }

        public DataView ListarPorFiltro(String des_arquivo_ban, Int32 num_altura_ban, Int32 num_largura_ban, Int32 cod_tipocliente, Int32 cod_baner_ban)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarBannerPorFiltro");
            _Command.Parameters.AddWithValue("@des_arquivo_ban", des_arquivo_ban == null ? Convert.DBNull : des_arquivo_ban);
            _Command.Parameters.AddWithValue("@num_altura_ban", num_altura_ban == 0 ? Convert.DBNull : num_altura_ban);
            _Command.Parameters.AddWithValue("@num_largura_ban", num_largura_ban == 0 ? Convert.DBNull : num_largura_ban);
            _Command.Parameters.AddWithValue("@cod_tipocliente", cod_tipocliente == 0 ? Convert.DBNull : cod_tipocliente);
            _Command.Parameters.AddWithValue("@cod_baner_ban", cod_baner_ban == 0 ? Convert.DBNull : cod_baner_ban);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_banner");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_banner"].DefaultView;
        }
    }
}
