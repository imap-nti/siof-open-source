using System.Data.Common;
using System;
using System.Data.SqlClient;
using System.Data;

namespace DsAdmin
{

    public class ContasPublicas
    {

        private DAO neo;

        private Boolean sessaoInterna = false;

        public ContasPublicas()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public ContasPublicas(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(Int32 cod_tipopublicacao, Int32 cod_tiposubpublicacao, String des_nomepublicacao, Int32 cod_cliente,
            //DateTime? dat_publicacao,
            String des_nomearquivo, Int32 num_ano_referencia, Int32 COD_CLASSE, Int32 COD_SUBCLASSE, String DES_CERTIFICACAO,
            //DateTime? DAT_CADASTRO,
            Int32 usu_codigo, Int32 and_codigo, ref Int32 cod_publicacao)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirIoContaspublicas");
            _Command.Parameters.AddWithValue("@cod_tipopublicacao", cod_tipopublicacao == 0 ? Convert.DBNull : cod_tipopublicacao);
            _Command.Parameters.AddWithValue("@cod_tiposubpublicacao", cod_tiposubpublicacao == 0 ? Convert.DBNull : cod_tiposubpublicacao);
            _Command.Parameters.AddWithValue("@des_nomepublicacao", des_nomepublicacao == null ? Convert.DBNull : des_nomepublicacao);
            //_Command.Parameters.AddWithValue("@dat_publicacao", !dat_publicacao.HasValue ? Convert.DBNull : dat_publicacao);
            _Command.Parameters.AddWithValue("@cod_cliente", cod_cliente == 0 ? Convert.DBNull : cod_cliente);
            _Command.Parameters.AddWithValue("@des_nomearquivo", des_nomearquivo == null ? Convert.DBNull : des_nomearquivo);
            _Command.Parameters.AddWithValue("@num_ano_referencia", num_ano_referencia == 0 ? Convert.DBNull : num_ano_referencia);
            _Command.Parameters.AddWithValue("@COD_CLASSE", COD_CLASSE == 0 ? Convert.DBNull : COD_CLASSE);
            _Command.Parameters.AddWithValue("@COD_SUBCLASSE", COD_SUBCLASSE == 0 ? Convert.DBNull : COD_SUBCLASSE);
            _Command.Parameters.AddWithValue("@DES_CERTIFICACAO", DES_CERTIFICACAO == null ? Convert.DBNull : DES_CERTIFICACAO);
            //_Command.Parameters.AddWithValue("@DAT_CADASTRO", !DAT_CADASTRO.HasValue ? Convert.DBNull : DAT_CADASTRO);
            _Command.Parameters.AddWithValue("@and_codigo", and_codigo == 0 ? Convert.DBNull : and_codigo);
            _Command.Parameters.AddWithValue("@usu_codigo", usu_codigo == 0 ? Convert.DBNull : usu_codigo);


            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@cod_publicacao", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            cod_publicacao = (Int32)_Command.Parameters["@cod_publicacao"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(Int32 cod_tipopublicacao, Int32 cod_tiposubpublicacao, String des_nomepublicacao,
            //DateTime? dat_publicacao, String des_nomearquivo, Int32 num_ano_referencia,
            Int32 COD_CLASSE, Int32 COD_SUBCLASSE, String DES_CERTIFICACAO,
            //DateTime? DAT_CADASTRO, 
            // Int32 usu_codigo, Int32 and_codigo,
            Int32 cod_publicacao)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarIoContaspublicas");
            _Command.Parameters.AddWithValue("@cod_publicacao", cod_publicacao == 0 ? Convert.DBNull : cod_publicacao);
            _Command.Parameters.AddWithValue("@cod_tipopublicacao", cod_tipopublicacao == 0 ? Convert.DBNull : cod_tipopublicacao);
            _Command.Parameters.AddWithValue("@cod_tiposubpublicacao", cod_tiposubpublicacao == 0 ? Convert.DBNull : cod_tiposubpublicacao);
            _Command.Parameters.AddWithValue("@des_nomepublicacao", des_nomepublicacao == null ? Convert.DBNull : des_nomepublicacao);
            //_Command.Parameters.AddWithValue("@dat_publicacao", !dat_publicacao.HasValue ? Convert.DBNull : dat_publicacao);
            //_Command.Parameters.AddWithValue("@des_nomearquivo", des_nomearquivo == null ? Convert.DBNull : des_nomearquivo);
            //_Command.Parameters.AddWithValue("@num_ano_referencia", num_ano_referencia == 0 ? Convert.DBNull : num_ano_referencia);
            _Command.Parameters.AddWithValue("@COD_CLASSE", COD_CLASSE == 0 ? Convert.DBNull : COD_CLASSE);
            _Command.Parameters.AddWithValue("@COD_SUBCLASSE", COD_SUBCLASSE == 0 ? Convert.DBNull : COD_SUBCLASSE);
            _Command.Parameters.AddWithValue("@DES_CERTIFICACAO", DES_CERTIFICACAO == null ? Convert.DBNull : DES_CERTIFICACAO);
            //_Command.Parameters.AddWithValue("@DAT_CADASTRO", !DAT_CADASTRO.HasValue ? Convert.DBNull : DAT_CADASTRO);
            //_Command.Parameters.AddWithValue("@usu_codigo", usu_codigo == 0 ? Convert.DBNull : usu_codigo);
            //_Command.Parameters.AddWithValue("@and_codigo", and_codigo == 0 ? Convert.DBNull : and_codigo);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Excluir(Int32 cod_publicacao)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ExcluirIoContaspublicas");
            _Command.Parameters.AddWithValue("@cod_publicacao", cod_publicacao == 0 ? Convert.DBNull : cod_publicacao);

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
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarIoContaspublicas");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_CONTASPUBLICAS");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_CONTASPUBLICAS"].DefaultView;
        }

        public DataView Consultar(Int32 cod_publicacao)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarIoContaspublicas");
            _Command.Parameters.AddWithValue("@cod_publicacao", cod_publicacao == 0 ? Convert.DBNull : cod_publicacao);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_CONTASPUBLICAS");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_CONTASPUBLICAS"].DefaultView;
        }

        public DataView ListarPorFiltro(Int32 cod_publicacao, Int32 cod_tipopublicacao, Int32 cod_tiposubpublicacao,
            String des_nomepublicacao, DateTime? dat_publicacao, String des_nomearquivo, Int32 num_ano_referencia,
            Int32 COD_CLASSE, Int32 COD_SUBCLASSE, String DES_CERTIFICACAO, DateTime? DAT_CADASTRO, Int32 cod_cliente, 
            Int32 and_codigo, Int32 usu_codigo)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarIoContaspublicasPorFiltro");
            _Command.Parameters.AddWithValue("@cod_publicacao", cod_publicacao == 0 ? Convert.DBNull : cod_publicacao);
            _Command.Parameters.AddWithValue("@cod_tipopublicacao", cod_tipopublicacao == 0 ? Convert.DBNull : cod_tipopublicacao);
            _Command.Parameters.AddWithValue("@cod_tiposubpublicacao", cod_tiposubpublicacao == 0 ? Convert.DBNull : cod_tiposubpublicacao);
            _Command.Parameters.AddWithValue("@des_nomepublicacao", des_nomepublicacao == null ? Convert.DBNull : des_nomepublicacao);
            _Command.Parameters.AddWithValue("@dat_publicacao", !dat_publicacao.HasValue ? Convert.DBNull : dat_publicacao);
            _Command.Parameters.AddWithValue("@des_nomearquivo", des_nomearquivo == null ? Convert.DBNull : des_nomearquivo);
            _Command.Parameters.AddWithValue("@num_ano_referencia", num_ano_referencia == 0 ? Convert.DBNull : num_ano_referencia);
            _Command.Parameters.AddWithValue("@COD_CLASSE", COD_CLASSE == 0 ? Convert.DBNull : COD_CLASSE);
            _Command.Parameters.AddWithValue("@COD_SUBCLASSE", COD_SUBCLASSE == 0 ? Convert.DBNull : COD_SUBCLASSE);
            _Command.Parameters.AddWithValue("@DES_CERTIFICACAO", DES_CERTIFICACAO == null ? Convert.DBNull : DES_CERTIFICACAO);
            _Command.Parameters.AddWithValue("@DAT_CADASTRO", !DAT_CADASTRO.HasValue ? Convert.DBNull : DAT_CADASTRO);
            _Command.Parameters.AddWithValue("@cod_cliente", cod_cliente == 0 ? Convert.DBNull : cod_cliente);
            _Command.Parameters.AddWithValue("@usu_codigo", usu_codigo == 0 ? Convert.DBNull : usu_codigo);
            _Command.Parameters.AddWithValue("@and_codigo", and_codigo == 0 ? Convert.DBNull : and_codigo);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_CONTASPUBLICAS");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_CONTASPUBLICAS"].DefaultView;
        }

    }
}
