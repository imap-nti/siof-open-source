using System.Data.Common;

using System;

using System.Data.SqlClient;

using System.Data;

namespace DsAdmin
{

    public class FonteModelo
    {

        private DAO neo;

        private Boolean sessaoInterna = false;

        public FonteModelo()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public FonteModelo(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(String des_fonte_modelo_fom, Int32 num_tamanho_minimo_fom, ref Int32 cod_fonte_modelo_fom)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirFonteModelo");
            _Command.Parameters.AddWithValue("@des_fonte_modelo_fom", des_fonte_modelo_fom == null ? Convert.DBNull : des_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@num_tamanho_minimo_fom", num_tamanho_minimo_fom == 0 ? Convert.DBNull : num_tamanho_minimo_fom);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@cod_fonte_modelo_fom", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            cod_fonte_modelo_fom = (Int32)_Command.Parameters["@cod_fonte_modelo_fom"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(String des_fonte_modelo_fom, Int32 num_tamanho_minimo_fom, Int32 cod_fonte_modelo_fom)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarFonteModelo");
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@des_fonte_modelo_fom", des_fonte_modelo_fom == null ? Convert.DBNull : des_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@num_tamanho_minimo_fom", num_tamanho_minimo_fom == 0 ? Convert.DBNull : num_tamanho_minimo_fom);
            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Excluir(Int32 cod_fonte_modelo_fom)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ExcluirFonteModelo");
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);

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
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarFonteModelo");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_fonte_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_fonte_modelo"].DefaultView;
        }

        public DataView Consultar(Int32 cod_fonte_modelo_fom)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarFonteModelo");
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_fonte_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_fonte_modelo"].DefaultView;
        }

        public DataView ListarPorFiltro(Int32 cod_fonte_modelo_fom, String des_fonte_modelo_fom, Int32 num_tamanho_minimo_fom)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarFonteModeloPorFiltro");
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@des_fonte_modelo_fom", des_fonte_modelo_fom == null ? Convert.DBNull : des_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@num_tamanho_minimo_fom", num_tamanho_minimo_fom == 0 ? Convert.DBNull : num_tamanho_minimo_fom);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_fonte_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_fonte_modelo"].DefaultView;
        }

    }
}
