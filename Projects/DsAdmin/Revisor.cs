using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Revisor
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Revisor()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Revisor(DAO conexao)
        {
            neo = conexao;
        }

        public DataView ListarDemandaPorFiltro(DateTime? txtDtIni, DateTime? txtDtFim, Int32 codUsuario, String chkOutros, String cboCidadeFiltro = null, Int32 cboStatus = 0,
            Int32 cboCidadeCliente = 0, Int32 cboTipo = 0, String txtComprovante = null, Int32 cboTipoDocumento = 0, Int32 cboUsuario = 0, Int32 cboOperador = 0,
            String cboUf = null, Int32 NIVELRELPUBLICACOES = 0)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarDemandaPorFiltro");
            _Command.Parameters.AddWithValue("@txtDtIni", !txtDtIni.HasValue ? Convert.DBNull : txtDtIni);
            _Command.Parameters.AddWithValue("@txtDtFim", !txtDtFim.HasValue ? Convert.DBNull : txtDtFim);
            _Command.Parameters.AddWithValue("@cboCidadeFiltro", cboCidadeFiltro == null ? Convert.DBNull : cboCidadeFiltro);
            _Command.Parameters.AddWithValue("@codUsuario", codUsuario == 0 ? Convert.DBNull : codUsuario);
            _Command.Parameters.AddWithValue("@cboStatus", cboStatus == 0 ? Convert.DBNull : cboStatus);
            _Command.Parameters.AddWithValue("@cboCidadeCliente", cboCidadeCliente == 0 ? Convert.DBNull : cboCidadeCliente);
            _Command.Parameters.AddWithValue("@cboTipo", cboTipo == 0 ? Convert.DBNull : cboTipo);
            _Command.Parameters.AddWithValue("@cboTipoDocumento", cboTipoDocumento == 0 ? Convert.DBNull : cboTipoDocumento);
            _Command.Parameters.AddWithValue("@txtComprovante", txtComprovante == null ? Convert.DBNull : txtComprovante);
            _Command.Parameters.AddWithValue("@cboUsuario", cboUsuario == 0 ? Convert.DBNull : cboUsuario);
            _Command.Parameters.AddWithValue("@cboOperador", cboOperador == 0 ? Convert.DBNull : cboOperador);
            _Command.Parameters.AddWithValue("@cboUf", cboUf == null ? Convert.DBNull : cboUf);
            _Command.Parameters.AddWithValue("@NIVELRELPUBLICACOES", NIVELRELPUBLICACOES == 0 ? Convert.DBNull : NIVELRELPUBLICACOES);
            _Command.Parameters.AddWithValue("@chkOutros", chkOutros == null ? Convert.DBNull : chkOutros);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "multi_temp_tb_demandas");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["multi_temp_tb_demandas"].DefaultView;
        }

        public void PublicarDocumentoRevisado(Int32 cod_status_diagramacao_std, Int32 usu_codigo_revisor, String des_arquivo_final_dod, Int32 doc_codigo, ref DateTime? dat_revisao_dod)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_PublicarDocumentoRevisado");
            _Command.Parameters.AddWithValue("@cod_status_diagramacao_std", cod_status_diagramacao_std == 0 ? Convert.DBNull : cod_status_diagramacao_std);
            _Command.Parameters.AddWithValue("@usu_codigo_revisor", usu_codigo_revisor == 0 ? Convert.DBNull : usu_codigo_revisor);
            _Command.Parameters.AddWithValue("@des_arquivo_final_dod", des_arquivo_final_dod == null ? Convert.DBNull : des_arquivo_final_dod);
            _Command.Parameters.AddWithValue("@doc_codigo", doc_codigo == 0 ? Convert.DBNull : doc_codigo);
            
            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@dat_revisao_dod", SqlDbType.DateTime);
            retorno.Direction = ParameterDirection.Output;
            
            neo.ExecutaNonQuery(_Command);
            dat_revisao_dod = (DateTime)_Command.Parameters["@dat_revisao_dod"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void PublicarDocumentoAlterado(Int32 cod_status_diagramacao_std, Int32 usu_codigo_revisor, String des_arquivo_final_dod, String tip_mudou_arquivo_dod, String des_arquivo_url_dod, Int32 doc_codigo, ref DateTime? dat_revisao_dod)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_PublicarDocumentoAlterado");
            _Command.Parameters.AddWithValue("@cod_status_diagramacao_std", cod_status_diagramacao_std == 0 ? Convert.DBNull : cod_status_diagramacao_std);
            _Command.Parameters.AddWithValue("@usu_codigo_revisor", usu_codigo_revisor == 0 ? Convert.DBNull : usu_codigo_revisor);
            _Command.Parameters.AddWithValue("@des_arquivo_final_dod", des_arquivo_final_dod == null ? Convert.DBNull : des_arquivo_final_dod);
            _Command.Parameters.AddWithValue("@tip_mudou_arquivo_dod", tip_mudou_arquivo_dod == null ? Convert.DBNull : tip_mudou_arquivo_dod);
            _Command.Parameters.AddWithValue("@des_arquivo_url_dod", des_arquivo_url_dod == null ? Convert.DBNull : des_arquivo_url_dod);
            _Command.Parameters.AddWithValue("@doc_codigo", doc_codigo == 0 ? Convert.DBNull : doc_codigo);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@dat_revisao_dod", SqlDbType.DateTime);
            retorno.Direction = ParameterDirection.Output;
            
            neo.ExecutaNonQuery(_Command);
            dat_revisao_dod = (DateTime)_Command.Parameters["@dat_revisao_dod"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void DevolverPublicacao(Int32 cod_status_diagramacao_std, Int32 usu_codigo_revisor, Int32 cod_publicacao_pub, String des_motivo_devolucao_dod)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_DevolverPublicacao");
            _Command.Parameters.AddWithValue("@cod_publicacao_pub", cod_publicacao_pub == 0 ? Convert.DBNull : cod_publicacao_pub);
            _Command.Parameters.AddWithValue("@usu_codigo_revisor", usu_codigo_revisor == 0 ? Convert.DBNull : usu_codigo_revisor);
            _Command.Parameters.AddWithValue("@cod_status_diagramacao_std", cod_status_diagramacao_std == 0 ? Convert.DBNull : cod_status_diagramacao_std);
            _Command.Parameters.AddWithValue("@des_motivo_devolucao_dod", des_motivo_devolucao_dod == null ? Convert.DBNull : des_motivo_devolucao_dod);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }
    }
}
