using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DsAdmin
{
    public class PaginaModelo
    {
        private DAO neo;

        private Boolean sessaoInterna = false;

        public PaginaModelo()
        {
            sessaoInterna = true;
            neo = new DAO();
        }

        public PaginaModelo(DAO conexao)
        {
            neo = conexao;
        }

        public DataView Listar()
        {
            DataSet lRetorno;
            SqlCommand _Command = (SqlCommand)neo.InicializaProcedure("usp_sep_ListarPaginaModelo");
            lRetorno = neo.ConsultaQueryDataSet(_Command, "tb_sep_pagina_modelo");
            if (sessaoInterna)
            {
                neo.Fechar();
            }
            return lRetorno.Tables["tb_sep_pagina_modelo"].DefaultView;
        }
    }
}
