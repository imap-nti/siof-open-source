using System;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class Brasao
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public Brasao()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public Brasao(DAO conexao)
        {
            neo = conexao;
        }

        public void Alterar(Int32 cod_cliente_cli, byte[] img_brasao_bra)
        {
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_AlterarBrasao");
            _Command.Parameters.AddWithValue("@img_brasao_bra", img_brasao_bra == null ? Convert.DBNull : img_brasao_bra);
            _Command.Parameters.AddWithValue("@COD_CLIENTE", cod_cliente_cli == 0 ? Convert.DBNull : cod_cliente_cli);

            neo.ExecutaNonQuery(_Command);
            if (sessaoInterna)
            {
                neo.Commit();
                neo.Fechar();
            }
        }
    }
}
