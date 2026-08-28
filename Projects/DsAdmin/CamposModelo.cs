using System.Data.Common;

using System;

using System.Data.SqlClient;

using System.Data;

namespace DsAdmin
{

    public class CamposModelo
    {

        private DAO neo;

        private Boolean sessaoInterna = false;

        public CamposModelo()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public CamposModelo(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(String des_nome_cam, String des_regra_cam, Decimal num_height_cam, Decimal num_width_cam, Decimal num_top_cam, Decimal num_left_cam, Int32 cod_modelo_edicao_moe, Int32 cod_fonte_modelo_fom, Int32 num_tamanho_fonte_cam, String tip_alinhamento_fonte_cam, String tip_negrito_fonte_cam, String tip_sublinhado_fonte_cam, String tip_italico_fonte_cam, String arq_imagem_cam, Int32 cod_pagina_modelo_pam, ref Int32 cod_campo_modelo_cam)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirCamposModelo");
            _Command.Parameters.AddWithValue("@des_nome_cam", des_nome_cam == null ? Convert.DBNull : des_nome_cam);
            _Command.Parameters.AddWithValue("@des_regra_cam", des_regra_cam == null ? Convert.DBNull : des_regra_cam);
            _Command.Parameters.AddWithValue("@num_height_cam", num_height_cam == 0 ? Convert.DBNull : num_height_cam);
            _Command.Parameters.AddWithValue("@num_width_cam", num_width_cam == 0 ? Convert.DBNull : num_width_cam);
            _Command.Parameters.AddWithValue("@num_top_cam", num_top_cam == 0 ? Convert.DBNull : num_top_cam);
            _Command.Parameters.AddWithValue("@num_left_cam", num_left_cam == 0 ? Convert.DBNull : num_left_cam);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@num_tamanho_fonte_cam", num_tamanho_fonte_cam == 0 ? Convert.DBNull : num_tamanho_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_alinhamento_fonte_cam", tip_alinhamento_fonte_cam == null ? Convert.DBNull : tip_alinhamento_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_negrito_fonte_cam", tip_negrito_fonte_cam == null ? Convert.DBNull : tip_negrito_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_sublinhado_fonte_cam", tip_sublinhado_fonte_cam == null ? Convert.DBNull : tip_sublinhado_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_italico_fonte_cam", tip_italico_fonte_cam == null ? Convert.DBNull : tip_italico_fonte_cam);
            _Command.Parameters.AddWithValue("@arq_imagem_cam", arq_imagem_cam == null ? Convert.DBNull : arq_imagem_cam);
            _Command.Parameters.AddWithValue("@cod_pagina_modelo_pam", cod_pagina_modelo_pam == 0 ? Convert.DBNull : cod_pagina_modelo_pam);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@cod_campo_modelo_cam", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            cod_campo_modelo_cam = (Int32)_Command.Parameters["@cod_campo_modelo_cam"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(String des_nome_cam, String des_regra_cam, Decimal num_height_cam, Decimal num_width_cam, Decimal num_top_cam, Decimal num_left_cam, Int32 cod_modelo_edicao_moe, Int32 cod_fonte_modelo_fom, Int32 num_tamanho_fonte_cam, String tip_alinhamento_fonte_cam, String tip_negrito_fonte_cam, String tip_sublinhado_fonte_cam, String tip_italico_fonte_cam, String arq_imagem_cam, Int32 cod_pagina_modelo_pam, Int32 cod_campo_modelo_cam)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarCamposModelo");
            _Command.Parameters.AddWithValue("@cod_campo_modelo_cam", cod_campo_modelo_cam == 0 ? Convert.DBNull : cod_campo_modelo_cam);
            _Command.Parameters.AddWithValue("@des_nome_cam", des_nome_cam == null ? Convert.DBNull : des_nome_cam);
            _Command.Parameters.AddWithValue("@des_regra_cam", des_regra_cam == null ? Convert.DBNull : des_regra_cam);
            _Command.Parameters.AddWithValue("@num_height_cam", num_height_cam == 0 ? Convert.DBNull : num_height_cam);
            _Command.Parameters.AddWithValue("@num_width_cam", num_width_cam == 0 ? Convert.DBNull : num_width_cam);
            _Command.Parameters.AddWithValue("@num_top_cam", num_top_cam == 0 ? Convert.DBNull : num_top_cam);
            _Command.Parameters.AddWithValue("@num_left_cam", num_left_cam == 0 ? Convert.DBNull : num_left_cam);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@num_tamanho_fonte_cam", num_tamanho_fonte_cam == 0 ? Convert.DBNull : num_tamanho_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_alinhamento_fonte_cam", tip_alinhamento_fonte_cam == null ? Convert.DBNull : tip_alinhamento_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_negrito_fonte_cam", tip_negrito_fonte_cam == null ? Convert.DBNull : tip_negrito_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_sublinhado_fonte_cam", tip_sublinhado_fonte_cam == null ? Convert.DBNull : tip_sublinhado_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_italico_fonte_cam", tip_italico_fonte_cam == null ? Convert.DBNull : tip_italico_fonte_cam);
            _Command.Parameters.AddWithValue("@arq_imagem_cam", arq_imagem_cam == null ? Convert.DBNull : arq_imagem_cam);
            _Command.Parameters.AddWithValue("@cod_pagina_modelo_pam", cod_pagina_modelo_pam == 0 ? Convert.DBNull : cod_pagina_modelo_pam);

            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Excluir(Int32 cod_campo_modelo_cam)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ExcluirCamposModelo");
            _Command.Parameters.AddWithValue("@cod_campo_modelo_cam", cod_campo_modelo_cam == 0 ? Convert.DBNull : cod_campo_modelo_cam);

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
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarCamposModelo");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_campos_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_campos_modelo"].DefaultView;
        }

        public DataView Consultar(Int32 cod_campo_modelo_cam)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarCamposModelo");
            _Command.Parameters.AddWithValue("@cod_campo_modelo_cam", cod_campo_modelo_cam == 0 ? Convert.DBNull : cod_campo_modelo_cam);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_campos_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_campos_modelo"].DefaultView;
        }

        public DataView ListarPorFiltro(Int32 cod_campo_modelo_cam, String des_nome_cam, String des_regra_cam, Int32 num_height_cam, Int32 num_width_cam, Int32 num_top_cam, Int32 num_left_cam, Int32 cod_modelo_edicao_moe, Int32 cod_fonte_modelo_fom, Int32 num_tamanho_fonte_cam, String tip_alinhamento_fonte_cam, String tip_negrito_fonte_cam, String tip_sublinhado_fonte_cam, String tip_italico_fonte_cam, String arq_imagem_cam, Int32 cod_pagina_modelo_pam)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarCamposModeloPorFiltro");
            _Command.Parameters.AddWithValue("@cod_campo_modelo_cam", cod_campo_modelo_cam == 0 ? Convert.DBNull : cod_campo_modelo_cam);
            _Command.Parameters.AddWithValue("@des_nome_cam", des_nome_cam == null ? Convert.DBNull : des_nome_cam);
            _Command.Parameters.AddWithValue("@des_regra_cam", des_regra_cam == null ? Convert.DBNull : des_regra_cam);
            _Command.Parameters.AddWithValue("@num_height_cam", num_height_cam == 0 ? Convert.DBNull : num_height_cam);
            _Command.Parameters.AddWithValue("@num_width_cam", num_width_cam == 0 ? Convert.DBNull : num_width_cam);
            _Command.Parameters.AddWithValue("@num_top_cam", num_top_cam == 0 ? Convert.DBNull : num_top_cam);
            _Command.Parameters.AddWithValue("@num_left_cam", num_left_cam == 0 ? Convert.DBNull : num_left_cam);
            _Command.Parameters.AddWithValue("@cod_modelo_edicao_moe", cod_modelo_edicao_moe == 0 ? Convert.DBNull : cod_modelo_edicao_moe);
            _Command.Parameters.AddWithValue("@cod_fonte_modelo_fom", cod_fonte_modelo_fom == 0 ? Convert.DBNull : cod_fonte_modelo_fom);
            _Command.Parameters.AddWithValue("@num_tamanho_fonte_cam", num_tamanho_fonte_cam == 0 ? Convert.DBNull : num_tamanho_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_alinhamento_fonte_cam", tip_alinhamento_fonte_cam == null ? Convert.DBNull : tip_alinhamento_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_negrito_fonte_cam", tip_negrito_fonte_cam == null ? Convert.DBNull : tip_negrito_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_sublinhado_fonte_cam", tip_sublinhado_fonte_cam == null ? Convert.DBNull : tip_sublinhado_fonte_cam);
            _Command.Parameters.AddWithValue("@tip_italico_fonte_cam", tip_italico_fonte_cam == null ? Convert.DBNull : tip_italico_fonte_cam);
            _Command.Parameters.AddWithValue("@arq_imagem_cam", arq_imagem_cam == null ? Convert.DBNull : arq_imagem_cam);
            _Command.Parameters.AddWithValue("@cod_pagina_modelo_pam", cod_pagina_modelo_pam == 0 ? Convert.DBNull : cod_pagina_modelo_pam);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_campos_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_campos_modelo"].DefaultView;
        }

    }
}
