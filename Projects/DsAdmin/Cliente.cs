using System;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Cliente
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Cliente()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Cliente(DAO conexao)
        {
            neo = conexao;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarClientes");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_CLIENTE");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_CLIENTE"].DefaultView;
        }

        public void Incluir(String DES_CIDADE, String DES_UF, String TIP_ATIVO, String DES_ENDERECO, String DES_BAIRRO, String DES_CEP,
            String DES_TELEFONE, String DES_EMAIL, String DES_CONTATO, String DES_AUTORIDADE, Int32 COD_TIPOCLIENTE, String NOM_EDITOR,
            String NOM_SECRETARIO, Int32 tip_site_ativo, String des_url_alternativa, String TIP_CAPA_INDICE, String TIP_PAGINA_MULTIPLO_4,
            String TIP_INCLUIR_PUBLICIDADE, Int32 cod_modelo_edicao_moe, String tip_assinatura_digital, String tip_carimbo_tempo, ref Int32 COD_CLIENTE)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirCliente");
            _Command.Parameters.AddWithValue("@DES_CIDADE", DES_CIDADE == null ? Convert.DBNull : DES_CIDADE);
            _Command.Parameters.AddWithValue("@DES_UF", DES_UF == null ? Convert.DBNull : DES_UF);
            _Command.Parameters.AddWithValue("@TIP_ATIVO", TIP_ATIVO == null ? Convert.DBNull : TIP_ATIVO);
            _Command.Parameters.AddWithValue("@DES_ENDERECO", DES_ENDERECO == null ? Convert.DBNull : DES_ENDERECO);
            _Command.Parameters.AddWithValue("@DES_BAIRRO", DES_BAIRRO == null ? Convert.DBNull : DES_BAIRRO);
            _Command.Parameters.AddWithValue("@DES_CEP", DES_CEP == null ? Convert.DBNull : DES_CEP);

            _Command.Parameters.AddWithValue("@DES_TELEFONE", DES_TELEFONE == null ? Convert.DBNull : DES_TELEFONE);
            _Command.Parameters.AddWithValue("@DES_EMAIL", DES_EMAIL == null ? Convert.DBNull : DES_EMAIL);
            _Command.Parameters.AddWithValue("@DES_CONTATO", DES_CONTATO == null ? Convert.DBNull : DES_CONTATO);
            _Command.Parameters.AddWithValue("@DES_AUTORIDADE", DES_AUTORIDADE == null ? Convert.DBNull : DES_AUTORIDADE);
            _Command.Parameters.AddWithValue("@COD_TIPOCLIENTE", COD_TIPOCLIENTE == 0 ? Convert.DBNull : COD_TIPOCLIENTE);
            _Command.Parameters.AddWithValue("@NOM_EDITOR", NOM_EDITOR == null ? Convert.DBNull : NOM_EDITOR);

            _Command.Parameters.AddWithValue("@NOM_SECRETARIO", NOM_SECRETARIO == null ? Convert.DBNull : NOM_SECRETARIO);
            _Command.Parameters.AddWithValue("@tip_site_ativo", tip_site_ativo == 0 ? Convert.DBNull : tip_site_ativo);
            _Command.Parameters.AddWithValue("@des_url_alternativa", des_url_alternativa == null ? Convert.DBNull : des_url_alternativa);
            _Command.Parameters.AddWithValue("@TIP_CAPA_INDICE", TIP_CAPA_INDICE == null ? Convert.DBNull : TIP_CAPA_INDICE);
            _Command.Parameters.AddWithValue("@TIP_PAGINA_MULTIPLO_4", TIP_PAGINA_MULTIPLO_4 == null ? Convert.DBNull : TIP_PAGINA_MULTIPLO_4);

            _Command.Parameters.AddWithValue("@TIP_INCLUIR_PUBLICIDADE", TIP_INCLUIR_PUBLICIDADE == null ? Convert.DBNull : TIP_INCLUIR_PUBLICIDADE);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@tip_assinatura_digital", tip_assinatura_digital == null ? Convert.DBNull : tip_assinatura_digital);
            _Command.Parameters.AddWithValue("@tip_carimbo_tempo", tip_carimbo_tempo == null ? Convert.DBNull : tip_carimbo_tempo);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@COD_CLIENTE", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            COD_CLIENTE = (Int32)_Command.Parameters["@COD_CLIENTE"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(//String DES_CIDADE, //String DES_UF, 
            String TIP_ATIVO, String DES_ENDERECO, String DES_BAIRRO, String DES_CEP,
            String DES_TELEFONE, String DES_EMAIL, String DES_CONTATO, String DES_AUTORIDADE, Int32 COD_TIPOCLIENTE, String NOM_EDITOR,
            String NOM_SECRETARIO, Int32 tip_site_ativo, String des_url_alternativa, String TIP_CAPA_INDICE, //String TIP_PAGINA_MULTIPLO_4, String TIP_INCLUIR_PUBLICIDADE,
            Int32 cod_modelo_edicao_moe, String tip_assinatura_digital, String tip_carimbo_tempo, Int32 COD_CLIENTE)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarCliente");
            //_Command.Parameters.AddWithValue("@DES_CIDADE", DES_CIDADE == null ? Convert.DBNull : DES_CIDADE);
            //_Command.Parameters.AddWithValue("@DES_UF", DES_UF == null ? Convert.DBNull : DES_UF);
            _Command.Parameters.AddWithValue("@TIP_ATIVO", TIP_ATIVO == null ? Convert.DBNull : TIP_ATIVO);
            _Command.Parameters.AddWithValue("@DES_ENDERECO", DES_ENDERECO == null ? Convert.DBNull : DES_ENDERECO);
            _Command.Parameters.AddWithValue("@DES_BAIRRO", DES_BAIRRO == null ? Convert.DBNull : DES_BAIRRO);
            _Command.Parameters.AddWithValue("@DES_CEP", DES_CEP == null ? Convert.DBNull : DES_CEP);

            _Command.Parameters.AddWithValue("@DES_TELEFONE", DES_TELEFONE == null ? Convert.DBNull : DES_TELEFONE);
            _Command.Parameters.AddWithValue("@DES_EMAIL", DES_EMAIL == null ? Convert.DBNull : DES_EMAIL);
            _Command.Parameters.AddWithValue("@DES_CONTATO", DES_CONTATO == null ? Convert.DBNull : DES_CONTATO);
            _Command.Parameters.AddWithValue("@DES_AUTORIDADE", DES_AUTORIDADE == null ? Convert.DBNull : DES_AUTORIDADE);
            //_Command.Parameters.AddWithValue("@COD_TIPOCLIENTE", COD_TIPOCLIENTE == 0 ? Convert.DBNull : COD_TIPOCLIENTE);
            _Command.Parameters.AddWithValue("@NOM_EDITOR", NOM_EDITOR == null ? Convert.DBNull : NOM_EDITOR);

            _Command.Parameters.AddWithValue("@NOM_SECRETARIO", NOM_SECRETARIO == null ? Convert.DBNull : NOM_SECRETARIO);
            _Command.Parameters.AddWithValue("@tip_site_ativo", tip_site_ativo == 0 ? Convert.DBNull : tip_site_ativo);
            _Command.Parameters.AddWithValue("@des_url_alternativa", des_url_alternativa == null ? Convert.DBNull : des_url_alternativa);
            _Command.Parameters.AddWithValue("@TIP_CAPA_INDICE", TIP_CAPA_INDICE == null ? Convert.DBNull : TIP_CAPA_INDICE);
            //_Command.Parameters.AddWithValue("@TIP_PAGINA_MULTIPLO_4", TIP_PAGINA_MULTIPLO_4 == null ? Convert.DBNull : TIP_PAGINA_MULTIPLO_4);

            //_Command.Parameters.AddWithValue("@TIP_INCLUIR_PUBLICIDADE", TIP_INCLUIR_PUBLICIDADE == null ? Convert.DBNull : TIP_INCLUIR_PUBLICIDADE);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@tip_assinatura_digital", tip_assinatura_digital == null ? Convert.DBNull : tip_assinatura_digital);
            _Command.Parameters.AddWithValue("@tip_carimbo_tempo", tip_carimbo_tempo == null ? Convert.DBNull : tip_carimbo_tempo);
            _Command.Parameters.AddWithValue("@COD_CLIENTE", COD_CLIENTE == 0 ? Convert.DBNull : COD_CLIENTE);

            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public DataView ListarPorFiltro(String DES_CIDADE, String DES_UF, String TIP_ATIVO, String DES_ENDERECO, String DES_BAIRRO, String DES_CEP,
            String DES_TELEFONE, String DES_EMAIL, String DES_CONTATO, String DES_AUTORIDADE, Int32 COD_TIPOCLIENTE, String NOM_EDITOR,
            String NOM_SECRETARIO, Int32 tip_site_ativo, String des_url_alternativa, String TIP_CAPA_INDICE, String TIP_PAGINA_MULTIPLO_4,
            String TIP_INCLUIR_PUBLICIDADE, Int32 cod_modelo_edicao_moe, String tip_assinatura_digital, String tip_carimbo_tempo, Int32 COD_CLIENTE)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarClientePorFiltro");
            _Command.Parameters.AddWithValue("@DES_CIDADE", DES_CIDADE == "" || DES_CIDADE == null ? Convert.DBNull : DES_CIDADE);
            _Command.Parameters.AddWithValue("@DES_UF", DES_UF == null ? Convert.DBNull : DES_UF);
            _Command.Parameters.AddWithValue("@TIP_ATIVO", TIP_ATIVO == null ? Convert.DBNull : TIP_ATIVO);
            _Command.Parameters.AddWithValue("@DES_ENDERECO", DES_ENDERECO == null ? Convert.DBNull : DES_ENDERECO);
            _Command.Parameters.AddWithValue("@DES_BAIRRO", DES_BAIRRO == null ? Convert.DBNull : DES_BAIRRO);
            _Command.Parameters.AddWithValue("@DES_CEP", DES_CEP == null ? Convert.DBNull : DES_CEP);

            _Command.Parameters.AddWithValue("@DES_TELEFONE", DES_TELEFONE == null ? Convert.DBNull : DES_TELEFONE);
            _Command.Parameters.AddWithValue("@DES_EMAIL", DES_EMAIL == null ? Convert.DBNull : DES_EMAIL);
            _Command.Parameters.AddWithValue("@DES_CONTATO", DES_CONTATO == null ? Convert.DBNull : DES_CONTATO);
            _Command.Parameters.AddWithValue("@DES_AUTORIDADE", DES_AUTORIDADE == null ? Convert.DBNull : DES_AUTORIDADE);
            _Command.Parameters.AddWithValue("@COD_TIPOCLIENTE", COD_TIPOCLIENTE == 0 ? Convert.DBNull : COD_TIPOCLIENTE);
            _Command.Parameters.AddWithValue("@NOM_EDITOR", NOM_EDITOR == null ? Convert.DBNull : NOM_EDITOR);

            _Command.Parameters.AddWithValue("@NOM_SECRETARIO", NOM_SECRETARIO == null ? Convert.DBNull : NOM_SECRETARIO);
            _Command.Parameters.AddWithValue("@tip_site_ativo", tip_site_ativo == 0 ? Convert.DBNull : tip_site_ativo);
            _Command.Parameters.AddWithValue("@des_url_alternativa", des_url_alternativa == null ? Convert.DBNull : des_url_alternativa);
            _Command.Parameters.AddWithValue("@TIP_CAPA_INDICE", TIP_CAPA_INDICE == null ? Convert.DBNull : TIP_CAPA_INDICE);
            _Command.Parameters.AddWithValue("@TIP_PAGINA_MULTIPLO_4", TIP_PAGINA_MULTIPLO_4 == null ? Convert.DBNull : TIP_PAGINA_MULTIPLO_4);

            _Command.Parameters.AddWithValue("@TIP_INCLUIR_PUBLICIDADE", TIP_INCLUIR_PUBLICIDADE == null ? Convert.DBNull : TIP_INCLUIR_PUBLICIDADE);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@tip_assinatura_digital", tip_assinatura_digital == null ? Convert.DBNull : tip_assinatura_digital);
            _Command.Parameters.AddWithValue("@tip_carimbo_tempo", tip_carimbo_tempo == null ? Convert.DBNull : tip_carimbo_tempo);
            _Command.Parameters.AddWithValue("@COD_CLIENTE", COD_CLIENTE == 0 ? Convert.DBNull : COD_CLIENTE);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_CLIENTE");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_CLIENTE"].DefaultView;
        }

        public DataView Consultar(Int32 COD_CLIENTE)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarCliente");
            _Command.Parameters.AddWithValue("@COD_CLIENTE", COD_CLIENTE == 0 ? Convert.DBNull : COD_CLIENTE);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_CLIENTE");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_CLIENTE"].DefaultView;
        }

        public DataView ConsultarClientePorDocumento(Int32 doc_codigo)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarClientePorDocumento");
            _Command.Parameters.AddWithValue("@doc_codigo", doc_codigo == 0 ? Convert.DBNull : doc_codigo);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "IO_CLIENTE");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["IO_CLIENTE"].DefaultView;
        }

        public DataView ConsultarBrasao(Int32 COD_CLIENTE)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarBrasao");
            _Command.Parameters.AddWithValue("@COD_CLIENTE", COD_CLIENTE == 0 ? Convert.DBNull : COD_CLIENTE);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sup_brasao");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sup_brasao"].DefaultView;
        }
    }
}
