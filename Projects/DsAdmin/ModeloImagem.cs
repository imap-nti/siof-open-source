using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class ModeloImagem
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public ModeloImagem()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public ModeloImagem(DAO conexao)
        {
            neo = conexao;
        }

        public void Incluir(String des_path_imagem_imm, ref Int32 cod_imagem_modelo_imm)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_IncluirModeloImagem");

            _Command.Parameters.AddWithValue("@des_path_imagem_imm", des_path_imagem_imm == "" ? Convert.DBNull : des_path_imagem_imm);

            SqlParameter retorno = new SqlParameter();
            retorno = _Command.Parameters.Add("@cod_imagem_modelo_imm", SqlDbType.Int);
            retorno.Direction = ParameterDirection.Output;

            neo.ExecutaNonQuery(_Command);
            cod_imagem_modelo_imm = (Int32)_Command.Parameters["@cod_imagem_modelo_imm"].Value;
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }

        public DataView Consultar(Int32 cod_imagem_modelo_imm)
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ConsultarModeloImagem");
            _Command.Parameters.AddWithValue("@cod_imagem_modelo_imm", cod_imagem_modelo_imm == 0 ? Convert.DBNull : cod_imagem_modelo_imm);
            lRetorno = neo.ConsultaQueryDataSet(_Command, "temp_multi_tb_publicacoes");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["temp_multi_tb_publicacoes"].DefaultView;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarModeloImagem");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "temp_multi_tb_publicacoes");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["temp_multi_tb_publicacoes"].DefaultView;
        }
    }
}
