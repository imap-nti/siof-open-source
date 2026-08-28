using System;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Usuario
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Usuario()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Usuario(DAO conexao)
        {
            neo = conexao;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarUsuario");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "SI_USUARIO");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["SI_USUARIO"].DefaultView;
        }

        public DataView Consultar(Int32 usu_codigo)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarUsuario");
            _Command.Parameters.AddWithValue("@usu_codigo", usu_codigo == 0 ? Convert.DBNull : usu_codigo);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "SI_USUARIO");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["SI_USUARIO"].DefaultView;
        }

        public DataView ListarUsuarioPorSistema(Int32 sis_codigo)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarUsuarioPorSistema");
            _Command.Parameters.AddWithValue("@sis_codigo", sis_codigo == 0 ? Convert.DBNull : sis_codigo);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "SI_USUARIO");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["SI_USUARIO"].DefaultView;
        }

        //public DataView ConsultarPorCpfEmail(String des_login_usu, String des_email_usu)
        //{

        //    DataSet lRetorno;
        //    SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_vig_ConsultarUsuarioPorCpfEmail");
        //    _Command.Parameters.AddWithValue("@des_login_usu", des_login_usu == null ? Convert.DBNull : des_login_usu);
        //    _Command.Parameters.AddWithValue("@des_email_usu", des_email_usu == null ? Convert.DBNull : des_email_usu);

        //    lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_vig_usuario");
        //    return lRetorno.Tables["tb_vig_usuario"].DefaultView;

        //}

        //public void AlterarSenha(String des_senha_usu, Int32 cod_usuario_usu, Int64 cod_municipio_mun, String des_login_usu)
        //{
        //    SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_vig_AlterarSenhaUsuario");
        //    _Command.Parameters.AddWithValue("@cod_usuario_usu", cod_usuario_usu == 0 ? Convert.DBNull : cod_usuario_usu);
        //    _Command.Parameters.AddWithValue("@des_senha_usu", des_senha_usu == null ? Convert.DBNull : Util.MD5("vigilancia-" + cod_municipio_mun + "-" + des_login_usu + "-" + des_senha_usu));
        //    neo.ExecutaNonQuery(_Command);
        //    if (sessaoInterna)
        //    {
        //        neo.Commit();
        //    }
        //}
    }
}
