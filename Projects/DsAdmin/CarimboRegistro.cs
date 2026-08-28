using System.Data.Common;
using System;
using System.Data.SqlClient;
using System.Data;

namespace DsAdmin
{
    public class CarimboRegistro
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public CarimboRegistro()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public CarimboRegistro(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(Int32 cod_usuario_usu, ref DateTime? dat_carimbo_car, Int32 cod_sistema_sis, String tip_carimbado_car, ref Int32 cod_carimbo_registro_car)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirCarimboRegistro");
            _Command.Parameters.AddWithValue("@cod_usuario_usu", cod_usuario_usu == 0 ? Convert.DBNull : cod_usuario_usu);
            _Command.Parameters.AddWithValue("@cod_sistema_sis", cod_sistema_sis == 0 ? Convert.DBNull : cod_sistema_sis);
            _Command.Parameters.AddWithValue("@tip_carimbado_car", tip_carimbado_car == null ? Convert.DBNull : tip_carimbado_car);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@cod_carimbo_registro_car", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            SqlParameter dat_retorno = new SqlParameter();
            dat_retorno = _Command.Parameters.Add("@dat_carimbo_car", SqlDbType.DateTime);
            dat_retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            cod_carimbo_registro_car = (Int32)_Command.Parameters["@cod_carimbo_registro_car"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(String tip_carimbado_car, Int32 cod_carimbo_registro_car)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarCarimboRegistro");
            _Command.Parameters.AddWithValue("@tip_carimbado_car", tip_carimbado_car == null ? Convert.DBNull : tip_carimbado_car);
            _Command.Parameters.AddWithValue("@cod_carimbo_registro_car", cod_carimbo_registro_car == 0 ? Convert.DBNull : cod_carimbo_registro_car);

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
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarCarimboRegistro");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_carimbo_registro");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_carimbo_registro"].DefaultView;
        }

        public DataView ConsultarRelatorioCarimboTempo(DateTime? data_inicio, DateTime? data_fim)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarRelatorioCarimboTempo");
            _Command.Parameters.AddWithValue("@data_inicio", !data_inicio.HasValue ? Convert.DBNull : data_inicio);
            _Command.Parameters.AddWithValue("@data_fim", !data_fim.HasValue ? Convert.DBNull : data_fim);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_carimbo_registro");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_carimbo_registro"].DefaultView;
        }
    }
}
