using System.Data.Common;
using System;
using System.Data.SqlClient;
using System.Data;

namespace DsAdmin
{

    public class TipoContasPublicas
    {

        private DAO neo;

        private Boolean sessaoInterna = false;

        public TipoContasPublicas()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public TipoContasPublicas(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(String des_tipo_contas_publicas_tcp, String tip_apenas_camara_tcp, Int32 cod_tipo_contas_publicas_tcp)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirTipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);
            _Command.Parameters.AddWithValue("@des_tipo_contas_publicas_tcp", des_tipo_contas_publicas_tcp == null ? Convert.DBNull : des_tipo_contas_publicas_tcp);
            _Command.Parameters.AddWithValue("@tip_apenas_camara_tcp", tip_apenas_camara_tcp == null ? Convert.DBNull : tip_apenas_camara_tcp);


            neo.ExecutaNonQuery(_Command);

            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(String des_tipo_contas_publicas_tcp, String tip_apenas_camara_tcp, Int32 cod_tipo_contas_publicas_tcp)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarTipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);
            _Command.Parameters.AddWithValue("@des_tipo_contas_publicas_tcp", des_tipo_contas_publicas_tcp == null ? Convert.DBNull : des_tipo_contas_publicas_tcp);
            _Command.Parameters.AddWithValue("@tip_apenas_camara_tcp", tip_apenas_camara_tcp == null ? Convert.DBNull : tip_apenas_camara_tcp);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Excluir(Int32 cod_tipo_contas_publicas_tcp)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ExcluirTipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);

            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarTipoContasPublicas");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_tipo_contas_publicas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_tipo_contas_publicas"].DefaultView;
        }

        public DataView Consultar(Int32 cod_tipo_contas_publicas_tcp)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarTipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_tipo_contas_publicas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_tipo_contas_publicas"].DefaultView;
        }

        public DataView ListarPorFiltro(Int32 cod_tipo_contas_publicas_tcp, String des_tipo_contas_publicas_tcp, String tip_apenas_camara_tcp)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarTipoContasPublicasPorFiltro");
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);
            _Command.Parameters.AddWithValue("@des_tipo_contas_publicas_tcp", des_tipo_contas_publicas_tcp == null ? Convert.DBNull : des_tipo_contas_publicas_tcp);
            _Command.Parameters.AddWithValue("@tip_apenas_camara_tcp", tip_apenas_camara_tcp == null ? Convert.DBNull : tip_apenas_camara_tcp);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_tipo_contas_publicas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_tipo_contas_publicas"].DefaultView;
        }

    }
}
