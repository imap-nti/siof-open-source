using System;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Maquina
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Maquina()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Maquina(DAO conexao)
        {
            neo = conexao;
        }

        public DataView Consultar(Int32 cod_maquina)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_io3_ConsultarMaquina");
            _Command.Parameters.AddWithValue("@cod_maquina", cod_maquina == 0 ? Convert.DBNull : cod_maquina);

            lRetorno = neo.ConsultaQueryDataSet(_Command, "io_maquina");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["io_maquina"].DefaultView;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_io3_ListarMaquina");

            lRetorno = neo.ConsultaQueryDataSet(_Command, "io_maquina");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["io_maquina"].DefaultView;
        }

        public void Incluir(String des_mac, String des_descricao)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_io3_IncluirMaquina");

            _Command.Parameters.AddWithValue("@des_mac", des_mac == null ? Convert.DBNull : des_mac);
            _Command.Parameters.AddWithValue("@des_descricao", des_descricao == null ? Convert.DBNull : des_descricao);

            neo.ExecutaNonQuery(_Command);

            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Alterar(String des_mac, String des_descricao, Int32 cod_maquina)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_io3_AlterarMaquina");

            _Command.Parameters.AddWithValue("@des_mac", des_mac == null ? Convert.DBNull : des_mac);
            _Command.Parameters.AddWithValue("@des_descricao", des_descricao == null ? Convert.DBNull : des_descricao);

            _Command.Parameters.AddWithValue("@cod_maquina", cod_maquina == 0 ? Convert.DBNull : cod_maquina);

            neo.ExecutaNonQuery(_Command);

            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public void Excluir(Int32 cod_maquina)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_io3_ExcluirMaquina");

            _Command.Parameters.AddWithValue("@cod_maquina", cod_maquina == 0 ? Convert.DBNull : cod_maquina);

            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }
    }
}
