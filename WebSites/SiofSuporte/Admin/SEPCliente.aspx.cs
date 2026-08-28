using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_SEPCliente : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        //verifica se a ação está nula, 
        //preenche com "cadastrar" no primeiro momento que a tela abrir,
        //para inicializar a variavel
        if (ViewState["acao"] == null)
        {
            ViewState["acao"] = "cadastrar";
        }
        base.OnLoad(e);
    }

    protected void grvPrincipal_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnEditar")
        {

            //carrega os dados do item selecionado para permitir a edição
            DsAdmin.Cliente rn = new DsAdmin.Cliente();
            System.Data.DataView vw;

            //1) consulta os dados do banco
            vw = rn.Consultar(Convert.ToInt32(e.CommandArgument.ToString()));

            //2) preenche os campos da tela
            DES_CIDADETextBox.Text = vw.Table.Rows[0]["DES_CIDADE"].ToString();
            TIP_ATIVORadioButtonList.SelectedValue = vw.Table.Rows[0]["TIP_ATIVO"] == DBNull.Value ? "-1" : vw.Table.Rows[0]["TIP_ATIVO"].ToString();
            DES_ENDERECOTextBox.Text = vw.Table.Rows[0]["DES_ENDERECO"].ToString();
            DES_BAIRROTextBox.Text = vw.Table.Rows[0]["DES_BAIRRO"].ToString();
            DES_CEPTextBox.Text = vw.Table.Rows[0]["DES_CEP"].ToString();
            DES_TELEFONETextBox.Text = vw.Table.Rows[0]["DES_TELEFONE"].ToString();
            DES_EMAILTextBox.Text = vw.Table.Rows[0]["DES_EMAIL"].ToString();
            DES_CONTATOTextBox.Text = vw.Table.Rows[0]["DES_CONTATO"].ToString();
            COD_TIPOCLIENTEDropDownList.SelectedValue = vw.Table.Rows[0]["COD_TIPOCLIENTE"].ToString();
            NOM_EDITORTextBox.Text = vw.Table.Rows[0]["NOM_EDITOR"].ToString();
            NOM_SECRETARIOTextBox.Text = vw.Table.Rows[0]["NOM_SECRETARIO"].ToString();
            tip_site_ativoRadioButtonList.SelectedValue = vw.Table.Rows[0]["tip_site_ativo"] == DBNull.Value ? "-1" : vw.Table.Rows[0]["tip_site_ativo"].ToString();
            des_url_alternativaTextBox.Text = vw.Table.Rows[0]["des_url_alternativa"].ToString();
            DES_AUTORIDADETextBox.Text = vw.Table.Rows[0]["DES_AUTORIDADE"].ToString();
            TIP_CAPA_INDICERadioButtonList.SelectedValue = vw.Table.Rows[0]["TIP_CAPA_INDICE"].ToString();
            //TIP_PAGINA_MULTIPLO_4CheckBox.Checked = vw.Table.Rows[0]["TIP_PAGINA_MULTIPLO_4"] == DBNull.Value ? false : true;
            //TIP_INCLUIR_PUBLICIDADECheckBox.Checked = vw.Table.Rows[0]["TIP_INCLUIR_PUBLICIDADE"] == DBNull.Value ? false : true;
            cod_modelo_edicao_moeDropDownList.SelectedValue = vw.Table.Rows[0]["cod_modelo_edicao_moe"].ToString();
            tip_assinatura_digitalCheckBox.Checked = vw.Table.Rows[0]["tip_assinatura_digital"] == DBNull.Value ? false : (vw.Table.Rows[0]["tip_assinatura_digital"].ToString() == "N" ? false : true);
            tip_carimbo_tempoCheckBox.Checked = vw.Table.Rows[0]["tip_carimbo_tempo"] == DBNull.Value ? false : (vw.Table.Rows[0]["tip_carimbo_tempo"].ToString() == "N" ? false : true);
            COD_CLIENTETextBox.Text = vw.Table.Rows[0]["COD_CLIENTE"].ToString();

            BrasaoImage.ImageUrl = "~/Admin/DownloadHandler.ashx?u=" + e.CommandArgument;
            BrasaoImage.DataBind();
            //3) coloca o codigo do item selecionado no ViewState, para permitir a alteração no botão confirmar (CadastrarLinkButton_Click)
            ViewState["COD_CLIENTE"] = e.CommandArgument;
            //4) coloca a ação a ser realizada ao clicar no confirmar (CadastrarLinkButton_Click)
            ViewState["acao"] = "alterar";
            CadastrarLinkButton.Visible = true;
            //5) altera o texto do botão cadastrar (CadastrarLinkButton), para confirmar
            CadastrarLinkButton.Text = "Confirmar";
        }
    }
    protected void CadastrarLinkButton_Click(Object sender, System.EventArgs e)
    {
        //cria uma transação para incluir ou alterar os dados dependendo da ação selecionada
        using (DsAdmin.Factory factory = new DsAdmin.Factory())
        {
            DsAdmin.Cliente rn = factory.Cliente();

            //if (ViewState["acao"].ToString() == "cadastrar")
            //{
            //    //se for incluir, chama o metodo de inclusão e passa os parametros
            //    Int32 retorno = 0;
            //    rn.Incluir(DES_CIDADETextBox.Text, DES_UFTextBox.Text, TIP_ATIVOCheckBox.Checked ? "S" : "N", DES_ENDERECOTextBox.Text, DES_BAIRROTextBox.Text,
            //        DES_CEPTextBox.Text, DES_TELEFONETextBox.Text, DES_EMAILTextBox.Text, DES_CONTATOTextBox.Text, DES_AUTORIDADETextBox.Text,
            //        Convert.ToInt32(COD_TIPOCLIENTEDropDownList.SelectedValue), NOM_EDITORTextBox.Text, NOM_SECRETARIOTextBox.Text,
            //        tip_site_ativoCheckBox.Checked ? 1 : 0, des_url_alternativaTextBox.Text, TIP_CAPA_INDICERadioButtonList.SelectedValue,
            //        TIP_PAGINA_MULTIPLO_4CheckBox.Checked ? "S" : "N", TIP_INCLUIR_PUBLICIDADECheckBox.Checked ? "S" : "N",
            //        Convert.ToInt32(cod_modelo_edicao_moeDropDownList.SelectedValue), tip_assinatura_digitalCheckBox.Checked ? "S" : "N",
            //        tip_carimbo_tempoCheckBox.Checked ? "S" : "N", ref retorno);
            //}
            //else 
            if (ViewState["acao"].ToString() == "alterar")
            {
                //se for alteração , chama o metodo de alteração e passa os parametros
                rn.Alterar(//DES_CIDADETextBox.Text, //DES_UFTextBox.Text,
                    TIP_ATIVORadioButtonList.SelectedValue, DES_ENDERECOTextBox.Text, DES_BAIRROTextBox.Text,
                    DES_CEPTextBox.Text, DES_TELEFONETextBox.Text, DES_EMAILTextBox.Text, DES_CONTATOTextBox.Text, DES_AUTORIDADETextBox.Text,
                    Convert.ToInt32(COD_TIPOCLIENTEDropDownList.SelectedValue), NOM_EDITORTextBox.Text, NOM_SECRETARIOTextBox.Text,
                    Convert.ToInt32(tip_site_ativoRadioButtonList.SelectedValue), des_url_alternativaTextBox.Text, TIP_CAPA_INDICERadioButtonList.SelectedValue,
                    //TIP_PAGINA_MULTIPLO_4CheckBox.Checked ? "S" : "N", TIP_INCLUIR_PUBLICIDADECheckBox.Checked ? "S" : "N",
                    Convert.ToInt32(cod_modelo_edicao_moeDropDownList.SelectedValue), tip_assinatura_digitalCheckBox.Checked ? "S" : "N",
                    tip_carimbo_tempoCheckBox.Checked ? "S" : "N", Convert.ToInt32(ViewState["COD_CLIENTE"]));
                if (Session["fileBytes"] != null)
                {
                    factory.Brasao().Alterar(Convert.ToInt32(ViewState["COD_CLIENTE"]), (byte[])Session["fileBytes"]);
                }
            }
            factory.Commit();
        } //fecha transação

        //chama o metodo Cancelar para limpar os campos e atualizar o Grid
        CancelarLinkButton_Click(null, null);
    }

    protected void CancelarLinkButton_Click(Object sender, System.EventArgs e)
    {
        //limpa os campos da tela, atualiza o grid, coloca a ação em modo de cadastrar e altera o texto do botão

        //1) limpa os campos
        //des_modelo_edicao_moeTextBox.Text ;
        DES_CIDADETextBox.Text = "";
        TIP_ATIVORadioButtonList.SelectedIndex = -1;
        DES_ENDERECOTextBox.Text = "";
        DES_BAIRROTextBox.Text = "";
        DES_CEPTextBox.Text = "";
        DES_TELEFONETextBox.Text = "";
        DES_EMAILTextBox.Text = "";
        DES_CONTATOTextBox.Text = "";
        DES_AUTORIDADETextBox.Text = "";
        COD_TIPOCLIENTEDropDownList.SelectedIndex = -1;
        NOM_EDITORTextBox.Text = "";
        NOM_SECRETARIOTextBox.Text = "";
        tip_site_ativoRadioButtonList.SelectedIndex = -1;
        des_url_alternativaTextBox.Text = "";
        TIP_CAPA_INDICERadioButtonList.SelectedIndex = -1;
        //TIP_PAGINA_MULTIPLO_4CheckBox.Checked = false;
        //TIP_INCLUIR_PUBLICIDADECheckBox.Checked = false;
        cod_modelo_edicao_moeDropDownList.SelectedIndex = -1;
        tip_assinatura_digitalCheckBox.Checked = false;
        tip_carimbo_tempoCheckBox.Checked = false;
        COD_CLIENTETextBox.Text = "";
        CadastrarLinkButton.Visible = false;
        BrasaoImage.ImageUrl = "";
        BrasaoImage.DataBind();
        //2) coloca a ação cadastrar no ViewState
        ViewState["acao"] = "cadastrar";
        ViewState["COD_CLIENTE"] = null;
        Session["fileBytes"] = null;
        //3) altera o texto do botão cadastrar
        CadastrarLinkButton.Text = "Cadastrar";
        //4) atualiza o Grid
        grvPrincipal.DataBind();
    }

    protected void grvPrincipal_PreRender(object sender, EventArgs e)
    {
        //força o atributo de acessibilidade
        grvPrincipal.UseAccessibleHeader = true;

        //adiciona <thead> e <tbody>
        if (grvPrincipal.HeaderRow != null)
        {
            grvPrincipal.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void BrasaoAsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        Session["fileBytes"] = BrasaoAsyncFileUpload.FileBytes;
        //BrasaoImage.Src= "~/Admin/DownloadHandler.ashx?u=-1";
        BrasaoImage.ImageUrl = "~/Admin/DownloadHandler.ashx?u=-1";
        BrasaoImage.DataBind();
        //UpdatePanel1.Update();
    }
}