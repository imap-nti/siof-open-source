using System.Data.Common;
using System;
using System.Data.SqlClient;
using System.Data;

namespace DsAdmin
{
    public class CadernoModelo
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public CadernoModelo()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public CadernoModelo(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(String des_caderno_modelo_cam, Int32 cod_modelo_edicao_moe, Int32 num_ordem_cam, ref Int32 cod_caderno_modelo_cam)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirCadernoModelo");
            _Command.Parameters.AddWithValue("@des_caderno_modelo_cam", des_caderno_modelo_cam == null ? Convert.DBNull : des_caderno_modelo_cam);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@num_ordem_cam", num_ordem_cam == 0 ? Convert.DBNull : num_ordem_cam);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@cod_caderno_modelo_cam", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            cod_caderno_modelo_cam = (Int32)_Command.Parameters["@cod_caderno_modelo_cam"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public DataView Consultar(Int32 cod_modelo_edicao_moe)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarCadernoModelo");
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_caderno_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_caderno_modelo"].DefaultView;
        }

        public DataView ListarPorFiltro(String des_caderno_modelo_cam, Int32 cod_modelo_edicao_moe, Int32 num_ordem_cam, Int32 cod_caderno_modelo_cam)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarCadernoModeloPorFiltro");
            _Command.Parameters.AddWithValue("@des_caderno_modelo_cam", des_caderno_modelo_cam == null ? Convert.DBNull : des_caderno_modelo_cam);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@num_ordem_cam", num_ordem_cam == 0 ? Convert.DBNull : num_ordem_cam);
            _Command.Parameters.AddWithValue("@cod_caderno_modelo_cam", cod_caderno_modelo_cam == 0 ? Convert.DBNull : cod_caderno_modelo_cam);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_caderno_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_caderno_modelo"].DefaultView;
        }

        public void Excluir(Int32 cod_caderno_modelo_cam)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ExcluirCadernoModelo");
            _Command.Parameters.AddWithValue("@cod_caderno_modelo_cam", cod_caderno_modelo_cam == 0 ? Convert.DBNull : cod_caderno_modelo_cam);

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
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarCadernoModelo");

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_caderno_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_caderno_modelo"].DefaultView;
        }

        public DataView ListarPorCliente(Int32 cod_cliente)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarCadernoModeloPorCliente");
            _Command.Parameters.AddWithValue("@cod_cliente", cod_cliente == 0 ? Convert.DBNull : cod_cliente);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_caderno_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_caderno_modelo"].DefaultView;
        }
    }
}
