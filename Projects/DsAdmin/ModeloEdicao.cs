using System.Data.Common;

using System;

using System.Data.SqlClient;

using System.Data;

namespace DsAdmin
{

    public class ModeloEdicao
    {

        private DAO neo;

        private Boolean sessaoInterna = false;

        public ModeloEdicao()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public ModeloEdicao(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(Int32 cod_usuario_usu, String des_modelo_edicao_moe, Int32 cod_fonte_modelo_fom, ref Int32 cod_modelo_edicao_moe)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirModeloEdicao");
            _Command.Parameters.AddWithValue("@cod_usuario_usu", cod_usuario_usu == 0 ? Convert.DBNull : cod_usuario_usu);
            _Command.Parameters.AddWithValue("@des_modelo_edicao_moe", des_modelo_edicao_moe == null ? Convert.DBNull : des_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@cod_modelo_edicao_moe", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            cod_modelo_edicao_moe = (Int32)_Command.Parameters["@cod_modelo_edicao_moe"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(DateTime? dat_criacao_modelo_moe, Int32 cod_usuario_usu, DateTime? dat_exclusao_modelo_moe, Int32 cod_usuario_exclusao_usu, String des_modelo_edicao_moe, Int32 cod_fonte_modelo_fom, Int32 cod_modelo_edicao_moe)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarModeloEdicao");
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@dat_criacao_modelo_moe", !dat_criacao_modelo_moe.HasValue ? Convert.DBNull : dat_criacao_modelo_moe);
            _Command.Parameters.AddWithValue("@cod_usuario_usu", cod_usuario_usu == 0 ? Convert.DBNull : cod_usuario_usu);
            _Command.Parameters.AddWithValue("@dat_exclusao_modelo_moe", !dat_exclusao_modelo_moe.HasValue ? Convert.DBNull : dat_exclusao_modelo_moe);
            _Command.Parameters.AddWithValue("@cod_usuario_exclusao_usu", cod_usuario_exclusao_usu == 0 ? Convert.DBNull : cod_usuario_exclusao_usu);
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@des_modelo_edicao_moe", des_modelo_edicao_moe == null ? Convert.DBNull : des_modelo_edicao_moe);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Excluir(Int32 cod_modelo_edicao_moe, Int32 cod_usuario_usu)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ExcluirModeloEdicao");
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@cod_usuario_usu", cod_usuario_usu == 0 ? Convert.DBNull : cod_usuario_usu);

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
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarModeloEdicao");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_modelo_edicao");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_modelo_edicao"].DefaultView;
        }

        public DataView Consultar(Int32 cod_modelo_edicao_moe)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarModeloEdicao");
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_modelo_edicao");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_modelo_edicao"].DefaultView;
        }

        public DataView ConsultarPorCliente(Int32 COD_CLIENTE)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarModeloEdicaoPorCliente");
            _Command.Parameters.AddWithValue("@COD_CLIENTE", COD_CLIENTE == 0 ? Convert.DBNull : COD_CLIENTE);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_modelo_edicao");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_modelo_edicao"].DefaultView;
        }

        public DataView ListarPorFiltro(Int32 cod_modelo_edicao_moe, DateTime? dat_criacao_modelo_moe, Int32 cod_usuario_usu, DateTime? dat_exclusao_modelo_moe, String des_modelo_edicao_moe, Int32 cod_usuario_exclusao_usu)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarModeloEdicaoPorFiltro");
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@dat_criacao_modelo_moe", !dat_criacao_modelo_moe.HasValue ? Convert.DBNull : dat_criacao_modelo_moe);
            _Command.Parameters.AddWithValue("@cod_usuario_usu", cod_usuario_usu == 0 ? Convert.DBNull : cod_usuario_usu);
            _Command.Parameters.AddWithValue("@dat_exclusao_modelo_moe", !dat_exclusao_modelo_moe.HasValue ? Convert.DBNull : dat_exclusao_modelo_moe);
            _Command.Parameters.AddWithValue("@cod_usuario_exclusao_usu", cod_usuario_exclusao_usu == 0 ? Convert.DBNull : cod_usuario_exclusao_usu);
            _Command.Parameters.AddWithValue("@des_modelo_edicao_moe", des_modelo_edicao_moe == null ? Convert.DBNull : des_modelo_edicao_moe);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_modelo_edicao");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_modelo_edicao"].DefaultView;
        }

    }
}
