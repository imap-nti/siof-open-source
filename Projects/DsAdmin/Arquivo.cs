using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Arquivo
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Arquivo()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Arquivo(DAO conexao)
        {
            neo = conexao;
        }

        public DataView ConsultarArquivoPublicacao(Int32 and_codigo)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarArquivoPublicacao");
            _Command.Parameters.AddWithValue("@and_codigo", and_codigo == 0 ? Convert.DBNull : and_codigo);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "multi_temp_tb_demandas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["multi_temp_tb_demandas"].DefaultView;
        }

        public DataView ListarArquivoPublicacao(Int32 cod_publicacao_pub)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarArquivoPublicacao");
            _Command.Parameters.AddWithValue("@cod_publicacao_pub", cod_publicacao_pub == 0 ? Convert.DBNull : cod_publicacao_pub);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "multi_temp_tb_demandas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["multi_temp_tb_demandas"].DefaultView;
        }

        public DataView ListarArquivoPublicacaoPorCliente(Int32 cod_cliente)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarArquivoPublicacaoPorCliente");
            _Command.Parameters.AddWithValue("@cod_cliente", cod_cliente == 0 ? Convert.DBNull : cod_cliente);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "multi_temp_tb_demandas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["multi_temp_tb_demandas"].DefaultView;
        }

        public void AlterarTipUtilizadoEditoracao(Int32 cod_publicacao_pub, String tip_utilizado_diagramacao_and)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarTipUtilizadoEditoracao");
            _Command.Parameters.AddWithValue("@cod_publicacao_pub", cod_publicacao_pub == 0 ? Convert.DBNull : cod_publicacao_pub);
            _Command.Parameters.AddWithValue("@tip_utilizado_diagramacao_and", tip_utilizado_diagramacao_and == null ? Convert.DBNull : tip_utilizado_diagramacao_and);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void AlterarTipUtilizadoRevisao(Int32 cod_publicacao_pub, String tip_revisado_diagramacao_and)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarTipUtilizadoRevisao");
            _Command.Parameters.AddWithValue("@cod_publicacao_pub", cod_publicacao_pub == 0 ? Convert.DBNull : cod_publicacao_pub);
            _Command.Parameters.AddWithValue("@tip_revisado_diagramacao_and", tip_revisado_diagramacao_and == null ? Convert.DBNull : tip_revisado_diagramacao_and);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void AlterarIoCilente(Int32 cod_modelo_edicao_moe, Int32 cod_cliente)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarIoCliente");
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@cod_cliente", cod_cliente == 0 ? Convert.DBNull : cod_cliente);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }
    }
}
