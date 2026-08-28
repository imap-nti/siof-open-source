using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace DsAdmin
{
    public class Publicacao
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Publicacao()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Publicacao(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(Int32 QTD_PAGINAS, Int32 COD_CLIENTE, DateTime? DAT_CRIACAO, Int32 COD_TIPODOCUMENTO,
            ref String DES_ANOEDICAO, String DES_OBSERVACAO, Int32 COD_STATUS, String DES_ARQUIVOFONTE, String DES_ARQUIVOA4, String DES_ARQUIVOA3, String DES_OPERADOR,
            String DES_RESPONSAVEL, String DES_IP, String DES_MAQUINA, Int32 tip_site_ativo, System.Xml.XmlDocument xml_carimbo, ref Int32 COD_DOCUMENTO, bool tip_amostra_gratis_doc = false, string DES_TEXTO_PDF = null)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirDocumento");
            //_Command.Parameters.AddWithValue("@COD_DOCUMENTO", COD_DOCUMENTO == 0 ? Convert.DBNull : COD_DOCUMENTO);
            _Command.Parameters.AddWithValue("@QTD_PAGINAS", QTD_PAGINAS == 0 ? Convert.DBNull : QTD_PAGINAS);
            _Command.Parameters.AddWithValue("@COD_CLIENTE", COD_CLIENTE == 0 ? Convert.DBNull : COD_CLIENTE);
            _Command.Parameters.AddWithValue("@DES_CERTIFICACAO", "");
            _Command.Parameters.AddWithValue("@DAT_CRIACAO", !DAT_CRIACAO.HasValue ? Convert.DBNull : DAT_CRIACAO);
            _Command.Parameters.AddWithValue("@COD_TIPODOCUMENTO", COD_TIPODOCUMENTO == 0 ? Convert.DBNull : COD_TIPODOCUMENTO);

            //_Command.Parameters.AddWithValue("@DES_ANOEDICAO", DES_ANOEDICAO == null ? Convert.DBNull : DES_ANOEDICAO);

            _Command.Parameters.AddWithValue("@DES_OBSERVACAO", DES_OBSERVACAO == null ? Convert.DBNull : DES_OBSERVACAO);
            _Command.Parameters.AddWithValue("@COD_STATUS", COD_STATUS == 0 ? Convert.DBNull : COD_STATUS);
            _Command.Parameters.AddWithValue("@DES_ARQUIVOFONTE", DES_ARQUIVOFONTE == null ? Convert.DBNull : DES_ARQUIVOFONTE);
            _Command.Parameters.AddWithValue("@DES_ARQUIVOA4", DES_ARQUIVOA4 == null ? Convert.DBNull : DES_ARQUIVOA4);
            _Command.Parameters.AddWithValue("@DES_ARQUIVOA3", DES_ARQUIVOA3 == null ? Convert.DBNull : DES_ARQUIVOA3);
            _Command.Parameters.AddWithValue("@DES_OPERADOR", DES_OPERADOR == null ? Convert.DBNull : DES_OPERADOR);
            _Command.Parameters.AddWithValue("@DES_RESPONSAVEL", DES_RESPONSAVEL == null ? Convert.DBNull : DES_RESPONSAVEL);
            _Command.Parameters.AddWithValue("@DES_IP", DES_IP == null ? Convert.DBNull : DES_IP);
            _Command.Parameters.AddWithValue("@DES_MAQUINA", DES_MAQUINA == null ? Convert.DBNull : DES_MAQUINA);
            _Command.Parameters.AddWithValue("@tip_site_ativo", tip_site_ativo == 0 ? Convert.DBNull : tip_site_ativo);
            _Command.Parameters.Add("@xml_carimbo", SqlDbType.Xml).Value = xml_carimbo.InnerXml.Replace("utf-8", "utf-16");
            //_Command.Parameters.AddWithValue("@xml_carimbo", xml_carimbo == null ? Convert.DBNull : xml_carimbo);
            
            _Command.Parameters.AddWithValue("@tip_amostra_gratis_doc", tip_amostra_gratis_doc);
            _Command.Parameters.AddWithValue("@DES_TEXTO_PDF", DES_TEXTO_PDF == null ? Convert.DBNull : DES_TEXTO_PDF);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@COD_DOCUMENTO", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            SqlParameter retorno1 = new SqlParameter();
            retorno1 = _Command.Parameters.Add("@DES_ANOEDICAO", SqlDbType.VarChar);
            retorno1.Size = 8;
            retorno1.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            COD_DOCUMENTO = (Int32)_Command.Parameters["@COD_DOCUMENTO"].Value;
            DES_ANOEDICAO = (String)_Command.Parameters["@DES_ANOEDICAO"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void AlterarDocumento(XmlDocument XML_CARIMBO, byte[] ARQ_CARIMBO, String des_hash, String DES_CERTIFICACAO, Int32 COD_DOCUMENTO, Int32 cod_cliente)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarDocumento");
            _Command.Parameters.Add("@XML_CARIMBO", SqlDbType.Xml).Value = XML_CARIMBO.InnerXml == "" ? Convert.DBNull : XML_CARIMBO.InnerXml;
            _Command.Parameters.Add("@ARQ_CARIMBO", SqlDbType.VarBinary).Value = ARQ_CARIMBO == null ? Convert.DBNull : ARQ_CARIMBO;
            _Command.Parameters.AddWithValue("@des_hash", des_hash == null ? Convert.DBNull : des_hash);
            _Command.Parameters.AddWithValue("@DES_CERTIFICACAO", DES_CERTIFICACAO == null ? Convert.DBNull : DES_CERTIFICACAO);
            _Command.Parameters.AddWithValue("@COD_DOCUMENTO", COD_DOCUMENTO == 0 ? Convert.DBNull : COD_DOCUMENTO);
            _Command.Parameters.AddWithValue("@cod_cliente", cod_cliente == 0 ? Convert.DBNull : cod_cliente);

            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public DataView ConsultarPublicacao(Int32 cod_publicacao_pub)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarPublicacao");
            _Command.Parameters.AddWithValue("@cod_publicacao_pub", cod_publicacao_pub == 0 ? Convert.DBNull : cod_publicacao_pub);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "temp_multi_tb_publicacoes");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["temp_multi_tb_publicacoes"].DefaultView;
        }

        public DataView ListarPublicacaoPorCliente(Int32 cod_cliente)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarPublicacaoPorCliente");
            _Command.Parameters.AddWithValue("@cod_cliente", cod_cliente == 0 ? Convert.DBNull : cod_cliente);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "temp_multi_tb_publicacoes");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["temp_multi_tb_publicacoes"].DefaultView;
        }
    }
}
