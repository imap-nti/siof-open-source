using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Assinador2
{
    public class IMAPModeloEdicao
    {
        //private string capa, contraCapa;
        private byte[] modelo;
        private List<IMAPCampoModelo> campos;

        public IMAPModeloEdicao(byte[] modelo, DataView vw)
        {
            //this.capa = @"C:\Users\Danilo-PC\Desktop\Capa.jpg";
            //this.contraCapa = @"C:\Users\Danilo-PC\Desktop\Contra-Capa.jpg";
            this.modelo = modelo;
            PreencherLista(vw);
        }

        public void PreencherLista(DataView vw)
        {
            campos = new List<IMAPCampoModelo>();


            for (int x = 0; x < vw.Table.Rows.Count; x++)
            {
                IMAPCampoModelo campo = new IMAPCampoModelo();
                campo.cod_pagina_modelo_pam = vw.Table.Rows[x]["cod_pagina_modelo_pam"] == DBNull.Value ? 0 : Convert.ToInt32(vw.Table.Rows[x]["cod_pagina_modelo_pam"]);
                campo.cod_campo_modelo_cam = vw.Table.Rows[x]["cod_campo_modelo_cam"] == DBNull.Value ? 0 : Convert.ToInt32(vw.Table.Rows[x]["cod_campo_modelo_cam"]);
                campo.num_height_cam = vw.Table.Rows[x]["num_height_cam"] == DBNull.Value ? 0 : Convert.ToDecimal(vw.Table.Rows[x]["num_height_cam"]);
                campo.num_width_cam = vw.Table.Rows[x]["num_width_cam"] == DBNull.Value ? 0 : Convert.ToDecimal(vw.Table.Rows[x]["num_width_cam"]);
                campo.num_top_cam = vw.Table.Rows[x]["num_top_cam"] == DBNull.Value ? 0 : Convert.ToDecimal(vw.Table.Rows[x]["num_top_cam"]);
                campo.num_left_cam = vw.Table.Rows[x]["num_left_cam"] == DBNull.Value ? 0 : Convert.ToDecimal(vw.Table.Rows[x]["num_left_cam"]);
                campo.cod_modelo_edicao_moe = vw.Table.Rows[x]["cod_modelo_edicao_moe"] == DBNull.Value ? 0 : Convert.ToInt32(vw.Table.Rows[x]["cod_modelo_edicao_moe"]);
                campo.des_nome_cam = vw.Table.Rows[x]["des_nome_cam"] == DBNull.Value ? null : vw.Table.Rows[x]["des_nome_cam"].ToString();
                campo.des_regra_cam = vw.Table.Rows[x]["des_regra_cam"] == DBNull.Value ? null : vw.Table.Rows[x]["des_regra_cam"].ToString();
                campo.tip_alinhamento_fonte_cam = vw.Table.Rows[x]["tip_alinhamento_fonte_cam"] == DBNull.Value ? null : vw.Table.Rows[x]["tip_alinhamento_fonte_cam"].ToString();

                if (vw.Table.Rows[x]["num_tamanho_fonte_cam"].ToString() != "")
                {
                    campo.num_tamanho_fonte_cam = vw.Table.Rows[x]["num_tamanho_fonte_cam"] == DBNull.Value ? 0 : Convert.ToInt32(vw.Table.Rows[x]["num_tamanho_fonte_cam"]);
                    campo.cod_fonte_modelo_fom = Convert.ToInt32(vw.Table.Rows[x]["cod_fonte_modelo_fom"].ToString() == "" ? 0 : vw.Table.Rows[x]["cod_fonte_modelo_fom"]);
                    campo.tip_negrito_fonte_cam = vw.Table.Rows[x]["tip_negrito_fonte_cam"] == DBNull.Value ? null : vw.Table.Rows[x]["tip_negrito_fonte_cam"].ToString();
                    campo.tip_sublinhado_fonte_cam = vw.Table.Rows[x]["tip_sublinhado_fonte_cam"] == DBNull.Value ? null : vw.Table.Rows[x]["tip_sublinhado_fonte_cam"].ToString();
                    campo.tip_italico_fonte_cam = vw.Table.Rows[x]["tip_italico_fonte_cam"] == DBNull.Value ? null : vw.Table.Rows[x]["tip_italico_fonte_cam"].ToString();
                    campo.des_fonte_modelo_fom = vw.Table.Rows[x]["des_fonte_modelo_fom"] == DBNull.Value ? null : vw.Table.Rows[x]["des_fonte_modelo_fom"].ToString();
                }
                else
                {
                    campo.arq_imagem_cam = vw.Table.Rows[x]["arq_imagem_cam"] == DBNull.Value ? null : vw.Table.Rows[x]["arq_imagem_cam"].ToString();
                }
                campos.Add(campo);
            }
        }

        //public byte[] Brasao { get { return this.brasao; } set { this.brasao = value; } }

        public byte[] Modelo { get { return this.modelo; } set { this.modelo = value; } }

        public List<IMAPCampoModelo> Campos { get { return this.campos; } set { this.campos = value; } }

        //public string Capa { get { return this.capa; } set { this.capa = value; } }

        //public string ContraCapa { get { return this.contraCapa; } set { this.contraCapa = value; } }
    }
}
