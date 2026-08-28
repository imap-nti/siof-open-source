using System.Data.Common;
using System;
using System.Data.SqlClient;
using System.Data;

namespace DsAdmin
{

    public class SubtipoContasPublicas
    {

        private DAO neo;

        private Boolean sessaoInterna = false;

        public SubtipoContasPublicas()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public SubtipoContasPublicas(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(String des_subtipo_contas_publicas_scp, Int32 cod_tipo_contas_publicas_tcp, Int32 cod_subtipo_contas_publicas_scp)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirSubtipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_subtipo_contas_publicas_scp", cod_subtipo_contas_publicas_scp == 0 ? Convert.DBNull : cod_subtipo_contas_publicas_scp);
            _Command.Parameters.AddWithValue("@des_subtipo_contas_publicas_scp", des_subtipo_contas_publicas_scp == null ? Convert.DBNull : des_subtipo_contas_publicas_scp);
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);


            neo.ExecutaNonQuery(_Command);

            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(String des_subtipo_contas_publicas_scp, Int32 cod_tipo_contas_publicas_tcp, Int32 cod_subtipo_contas_publicas_scp)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarSubtipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_subtipo_contas_publicas_scp", cod_subtipo_contas_publicas_scp == 0 ? Convert.DBNull : cod_subtipo_contas_publicas_scp);
            _Command.Parameters.AddWithValue("@des_subtipo_contas_publicas_scp", des_subtipo_contas_publicas_scp == null ? Convert.DBNull : des_subtipo_contas_publicas_scp);
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Excluir(Int32 cod_subtipo_contas_publicas_scp)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ExcluirSubtipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_subtipo_contas_publicas_scp", cod_subtipo_contas_publicas_scp == 0 ? Convert.DBNull : cod_subtipo_contas_publicas_scp);

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
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarSubtipoContasPublicas");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_subtipo_contas_publicas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_subtipo_contas_publicas"].DefaultView;
        }

        public DataView Consultar(Int32 cod_subtipo_contas_publicas_scp)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarSubtipoContasPublicas");
            _Command.Parameters.AddWithValue("@cod_subtipo_contas_publicas_scp", cod_subtipo_contas_publicas_scp == 0 ? Convert.DBNull : cod_subtipo_contas_publicas_scp);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_subtipo_contas_publicas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_subtipo_contas_publicas"].DefaultView;
        }

        public DataView ListarPorFiltro(Int32 cod_subtipo_contas_publicas_scp, String des_subtipo_contas_publicas_scp, Int32 cod_tipo_contas_publicas_tcp)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarSubtipoContasPublicasPorFiltro");
            _Command.Parameters.AddWithValue("@cod_subtipo_contas_publicas_scp", cod_subtipo_contas_publicas_scp == 0 ? Convert.DBNull : cod_subtipo_contas_publicas_scp);
            _Command.Parameters.AddWithValue("@des_subtipo_contas_publicas_scp", des_subtipo_contas_publicas_scp == null ? Convert.DBNull : des_subtipo_contas_publicas_scp);
            _Command.Parameters.AddWithValue("@cod_tipo_contas_publicas_tcp", cod_tipo_contas_publicas_tcp == 0 ? Convert.DBNull : cod_tipo_contas_publicas_tcp);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_subtipo_contas_publicas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_subtipo_contas_publicas"].DefaultView;
        }

    }
}
