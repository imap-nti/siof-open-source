using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SiofDiagramador : System.Web.UI.Page
{
#if false
    /// <summary>
    /// Host destino para onde são enviados os arquivos.
    /// </summary>
    private string ftpHost = Environment.GetEnvironmentVariable("SIOF_FTP_HOMOLOGA_HOST") ?? "net.servidor/documentosSiof/";

    private String ftpModel = Environment.GetEnvironmentVariable("SIOF_FTP_HOMOLOGA_MODEL_HOST") ?? "net.servidor/documentosSEP/modelo/";

    /// <summary>
    /// Privilégios de login ao repositório FTP.
    /// </summary>
    private String ftpUid = Environment.GetEnvironmentVariable("SIOF_FTP_HOMOLOGA_UID") ?? "ftp_homologa", ftpPwd = Environment.GetEnvironmentVariable("SIOF_FTP_HOMOLOGA_PASSWORD");
#else
    /// <summary>
    /// Host destino para onde são enviados os arquivos.
    /// </summary>
    private String ftpHost = Environment.GetEnvironmentVariable("SIOF_FTP_HOST") ?? "storage.io.org.br/documentosSiof/";

    private String ftpModel = Environment.GetEnvironmentVariable("SIOF_FTP_MODEL_HOST") ?? "storage.io.org.br/documentosSEP/modelo/";

    /// <summary>
    /// Privilégios de login ao repositório FTP.
    /// </summary>
    private String ftpUid = Environment.GetEnvironmentVariable("SIOF_FTP_UID") ?? "documentos", ftpPwd = Environment.GetEnvironmentVariable("SIOF_FTP_PASSWORD");
#endif

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack && !ItemContasPublicasFileUpload.IsInFileUploadPostBack)
        {
            ModalDetalhamentoPlaceholder.Visible = false;
            ModalArquivosContasPublicasPlaceHolder.Visible = false;
            ModalContaPublicaPlaceHolder.Visible = false;
        }

        if (ViewState["acao"] == null)
        {
            ViewState["acao"] = "cadastrar";
        }
        base.OnLoad(e);
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

    protected void grvPrincipal_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnDetalhar")
        {
            ViewState["cod_publicacao_pub"] = e.CommandArgument;
            ModalDetalhamentoPlaceholder.Visible = true;
            System.Data.DataView vw = new DsAdmin.Arquivo().ListarArquivoPublicacao(Convert.ToInt32(e.CommandArgument));
            //ViewState["and_codigo"] = vw.Table.Rows[0]["and_codigo"].ToString();
            Session["cod_cliente"] = vw.Table.Rows[0]["cod_cliente"].ToString();
            des_motivo_devolucao_label.Text = (vw.Table.Rows[0]["des_motivo_devolucao_dod"] == DBNull.Value ? "" : "<br /><strong>Motivo Devolução:</strong> " + vw.Table.Rows[0]["des_motivo_devolucao_dod"].ToString() + "<br />");
            UpdatePanel2.Update();
        }
    }

    protected void ItensUtilizadosGridView_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnUpload")
        {
            ModalArquivosContasPublicasPlaceHolder.Visible = true;
            Session["and_codigo"] = e.CommandArgument;
            Session["listFileBytes"] = new List<byte[]>();
            Session["listFileNames"] = new List<string>();
            UpdatePanel3.Update();
        }
    }

    protected void PublicarContasPublicasLinkButton_Click(object sender, EventArgs e)
    {
        ModalContaPublicaPlaceHolder.Visible = true;
        //ViewState["cod_cliente"] = new DsAdmin.Publicacao().ConsultarPublicacao(Convert.ToInt32(Session["and_codigo"])).Table.Rows[0]["cod_cliente"].ToString();
        UpdatePanel4.Update();
    }

    protected void FecharModalArquivosContasPublicasLinkButton_Click(object sender, EventArgs e)
    {
        ModalArquivosContasPublicasPlaceHolder.Visible = false;
    }

    protected void ItemContasPublicasFileUpload_UploadCompleteAll(object sender, AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs e)
    {
        using (DsAdmin.Factory factory = new DsAdmin.Factory())
        {
            DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();
            //Para cada arquivo contido na variável de sessão serão:
            ((List<byte[]>)Session["listFileBytes"]).ForEach(file =>
                {
                    //Encontrados os nomes correspondentes aos arquivos em questão.
                    string fileName = ((List<string>)Session["listFileNames"])[((List<byte[]>)Session["listFileBytes"]).FindIndex(f => { return (f == file); })];

                    //Incuidos os dados prévios ao contas públicas.
                    Int32 retorno = 0;
                    DsAdmin.ContasPublicas rn = factory.ContasPublicas();
                    rn.Incluir(0, 0, null, Convert.ToInt32(Session["cod_cliente"]), fileName, 0, 0, 0, null,
                        Convert.ToInt32(Session["usu_codigo"]), Convert.ToInt32(Session["and_codigo"]), ref retorno);

                    //Criados novos diretórios caso ainda não existam.
                    ftp.CreateDirectoryInFTP("ftp://" + ftpHost + "ContasPublicas/" + Session["cod_cliente"].ToString() + "/" + DateTime.Now.ToString("yyyy/MM/"), ftpUid, ftpPwd);

                    //Enviados os arquivos para o diretório destino.
                    ftp.UploadFileToFTP(file, ftpHost, "ContasPublicas/" + Session["cod_cliente"].ToString() + "/" + DateTime.Now.ToString("yyyy/MM/"), fileName, ftpUid, ftpPwd);

                });
            //Apagadas as sessões pertinentes a esta inclusão.
            Session["listFileBytes"] = new List<byte[]>();
            Session["listFileNames"] = new List<string>();
        }
    }

    protected void ItemContasPublicasFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        ((List<byte[]>)Session["listFileBytes"]).Add(e.GetContents());
        ((List<string>)Session["listFileNames"]).Add(e.FileName);
    }

    //protected void ItensUtilizadosGridView_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "btnBaixar")
    //    {
    //        Int32 index = Convert.ToInt32(e.CommandArgument);
    //        System.Data.DataView vw = new DsAdmin.Arquivo().ListarArquivoPublicacao(index);

    //        ViewState["doc_dt_publicacao"] = vw.Table.Rows[0]["doc_dt_publicacao"].ToString();
    //        ViewState["cod_cliente"] = vw.Table.Rows[0]["cod_cliente"].ToString();
    //        ViewState["and_caminho"] = vw.Table.Rows[0]["and_caminho"].ToString();

    //        //Container.DataItemIndex
    //        //ViewState["doc_dt_publicacao"] = ((GridView)sender).DataKeys[index]["doc_dt_publicacao"].ToString();
    //        //ViewState["cod_cliente"] = ((GridView)sender).DataKeys[index]["cod_cliente"].ToString();
    //        //ViewState["and_caminho"] = ((GridView)sender).DataKeys[index]["and_caminho"].ToString();
    //        Response.Redirect("DownloadHandler.ashx?ftpfullpath=" + "ftp://" + ftpSep + ViewState["cod_cliente"].ToString() + "/" + Convert.ToDateTime(ViewState["doc_dt_publicacao"]).ToString("yyyy/M/") + ViewState["and_caminho"].ToString() + "&ftpUid=" + ftpUid + "&ftpPwd=" + ftpPwd);
    //    }
    //}

    protected void FecharModalContaPublicaLinkButton_Click(object sender, EventArgs e)
    {
        ModalContaPublicaPlaceHolder.Visible = false;
        CancelarLinkButton_Click(null, null);
    }

    protected void ContasPublicasGridview_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnEditar")
        {
            //carrega os dados do item selecionado para permitir a edição
            DsAdmin.ContasPublicas rn = new DsAdmin.ContasPublicas();
            System.Data.DataView vw;

            //1) consulta os dados do banco
            vw = rn.Consultar(Convert.ToInt32(e.CommandArgument.ToString()));

            //2) preenche os campos da tela
            TipoContasPublicasDropDownList.SelectedValue = vw.Table.Rows[0]["cod_tipopublicacao"] == DBNull.Value ? "0" : vw.Table.Rows[0]["cod_tipopublicacao"].ToString();
            SubtipoContasPublicasDropDownList.SelectedValue = vw.Table.Rows[0]["cod_tiposubpublicacao"] == DBNull.Value ? "0" : vw.Table.Rows[0]["cod_tiposubpublicacao"].ToString();
            //num_ano_referenciaTextBox.Text = vw.Table.Rows[0]["num_ano_referencia"] == DBNull.Value ? "" : vw.Table.Rows[0]["num_ano_referencia"].ToString();
            des_nomepublicacaoTextBox.Text = vw.Table.Rows[0]["des_nomepublicacao"] == DBNull.Value ? "" : vw.Table.Rows[0]["des_nomepublicacao"].ToString();
            //SubtipoContasPublicasDropDownList.DataSourceID = "SubtipoContasPublicasObjectDataSource";
            TipoContasPublicasDropDownList_SelectedIndexChanged(TipoContasPublicasDropDownList, null);
            //3) coloca o codigo do item selecionado no ViewState, para permitir a alteração no botão confirmar (CadastrarLinkButton_Click)
            ViewState["cod_publicacao"] = e.CommandArgument;
            ViewState["and_codigo"] = vw.Table.Rows[0]["and_codigo"].ToString();
            //4) coloca a ação a ser realizada ao clicar no confirmar (CadastrarLinkButton_Click)
            ViewState["acao"] = "alterar";
            //5) altera o texto do botão cadastrar (CadastrarLinkButton), para confirmar
            CadastrarLinkButton.Visible = true;
            UpdatePanel4.Update();
        }
        else if (e.CommandName == "btnExcluir")
        {
            System.Data.DataView vw = new DsAdmin.ContasPublicas().Consultar(Convert.ToInt32(e.CommandArgument));

            if (new DsAdmin.FTPManager().DeleteFileOnFtp(ftpHost + "ContasPublicas/" + Convert.ToDateTime(vw.Table.Rows[0]["dat_publicacao"]).ToString("yyyy/MM/") + vw.Table.Rows[0]["des_nomearquivo"].ToString(), ftpUid, ftpPwd))
            {
                try
                {
                    new DsAdmin.ContasPublicas().Excluir(Convert.ToInt32(e.CommandArgument));
                    ContasPublicasGridview.DataBind();
                }
                catch (Exception ex)
                {
                    Msg.MessageBox(Page, ex.StackTrace, true);
                }
            }
        }
    }

    protected void ArquivosContasPublicasGridview_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnExcluir")
        {
            System.Data.DataView vw = new DsAdmin.ContasPublicas().Consultar(Convert.ToInt32(e.CommandArgument));

            if (new DsAdmin.FTPManager().DeleteFileOnFtp(ftpHost + "ContasPublicas/" + Convert.ToDateTime(vw.Table.Rows[0]["dat_publicacao"]).ToString("yyyy/MM/") + vw.Table.Rows[0]["des_nomearquivo"].ToString(), ftpUid, ftpPwd))
            {
                try
                {
                    new DsAdmin.ContasPublicas().Excluir(Convert.ToInt32(e.CommandArgument));
                    ArquivosContasPublicasGridView.DataBind();
                }
                catch (Exception ex)
                {
                    Msg.MessageBox(Page, ex.StackTrace, true);
                }
            }
        }
    }

    protected void ArquivosContasPublicasObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (Session["and_codigo"] != null)
        {
            e.InputParameters["and_codigo"] = Session["and_codigo"];
        }
    }

    protected void ContasPublicasObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (Session["cod_cliente"] != null)
        {
            e.InputParameters["cod_cliente"] = Session["cod_cliente"];
        }
    }

    protected void CadastrarLinkButton_Click(object sender, EventArgs e)
    {
        //DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();
        //if ((string)ViewState["acao"] == "cadastrar")
        //{
        //    using (DsAdmin.Factory factory = new DsAdmin.Factory())
        //    {
        //        Int32 retorno = 0;
        //        DsAdmin.ContasPublicas rn = factory.ContasPublicas();
        //        rn.Incluir(Convert.ToInt32(TipoContasPublicasDropDownList.SelectedValue),
        //            Convert.ToInt32(SubtipoContasPublicasDropDownList.SelectedValue),
        //            //des_nomepublicacaoTextBox.Text,
        //            Convert.ToInt32(ViewState["cod_cliente"]),
        //            //des_nomearquivoTextBox.Text,
        //            Convert.ToInt32(num_ano_referenciaTextBox.Text), 0, 0, "",
        //            Convert.ToInt32(ViewState["and_codigo"]), Convert.ToInt32(Session["usu_codigo"]), ref retorno);

        //        ftp.CreateDirectoryInFTP("ftp://" + ftpHost + "/ContasPublicas/" + DateTime.Now.ToString("yyyy/MM/"), ftpUid, ftpPwd);
        //        ftp.UploadFileToFTP((byte[])Session["fileBytes"], ftpHost, "/ContasPublicas/" + DateTime.Now.ToString("yyyy/MM/"), Session["fileName"].ToString(), ftpUid, ftpPwd);
        //    }
        //}
        //else if ((string)ViewState["acao"] == "alterar")
        //{
        using (DsAdmin.Factory factory = new DsAdmin.Factory())
        {
            DsAdmin.ContasPublicas rn = factory.ContasPublicas();
            rn.Alterar(Convert.ToInt32(TipoContasPublicasDropDownList.SelectedValue),
                Convert.ToInt32(SubtipoContasPublicasDropDownList.SelectedValue),
                des_nomepublicacaoTextBox.Text, //Convert.ToInt32(num_ano_referenciaTextBox.Text),
                0, 0, null,
                //Convert.ToInt32(Session["usu_codigo"]), Convert.ToInt32(ViewState["and_codigo"]), 
                Convert.ToInt32(ViewState["cod_publicacao"]));
        }
        //}
        CancelarLinkButton_Click(null, null);
    }

    protected void SubtipoContasPublicasObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (ViewState["cod_tipo_contas_publicas_tcp"] != null)
        {
            e.InputParameters["cod_tipo_contas_publicas_tcp"] = ViewState["cod_tipo_contas_publicas_tcp"];
        }
    }

    protected void TipoContasPublicasDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        object obj = ((DropDownList)sender).SelectedValue;
        if (obj.ToString() != "0")
        {
            ViewState["cod_tipo_contas_publicas_tcp"] = obj;
            SubtipoContasPublicasDropDownList.DataSourceID = "SubtipoContasPublicasObjectDataSource";
            SubtipoContasPublicasDropDownList.DataBind();
        }
        else
        {
            SubtipoContasPublicasDropDownList.DataSourceID = "";
            SubtipoContasPublicasDropDownList.DataBind();
        }
    }

    protected void CancelarLinkButton_Click(object sender, EventArgs e)
    {
        TipoContasPublicasDropDownList.SelectedIndex = -1;
        SubtipoContasPublicasDropDownList.SelectedIndex = -1;
        //num_ano_referenciaTextBox.Text = "";
        des_nomepublicacaoTextBox.Text = "";
        ContasPublicasGridview.DataBind();
        CadastrarLinkButton.Visible = false;
        SubtipoContasPublicasDropDownList.DataSourceID = "";
        UpdatePanel4.Update();
    }

    protected void FecharModalDetalhamentoLinkButton_Click(object sender, EventArgs e)
    {
        ViewState["cod_publicacao_pub"] = null;
        Session["doc_dt_publicacao"] = null;
        Session["cod_cliente"] = null;
        Session["UploadArquivo"] = null;
        Session.Remove("fileBytes");
        Session.Remove("fileName");
        ModalDetalhamentoPlaceholder.Visible = false;
    }

    protected bool ItensUtilizadosCheckBoxList_Cheks()
    {
        int contador = 0;
        for (int x = 0; x < ItensUtilizadosGridView.Rows.Count; x++)
        {
            if (((CheckBox)ItensUtilizadosGridView.Rows[x].Cells[0].FindControl("Item")).Checked)
            {
                contador += 1;
            }
        }

        if (contador == ItensUtilizadosGridView.Rows.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void EnviarRevisaoLinkButton_Click(object sender, EventArgs e)
    {
        if (Session["UploadArquivo"] == null ? false : (bool)Session["UploadArquivo"])
        {
            if (ItensUtilizadosCheckBoxList_Cheks())
            {
                if (Session["fileName"] != null && Session["fileName"].ToString().EndsWith(".pdf"))
                {
                    try
                    {
                        using (DsAdmin.Factory factory = new DsAdmin.Factory())
                        {
                            DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();
                            ftp.CreateDirectoryInFTP("ftp://" + ftpHost + Session["cod_cliente"].ToString() + "/" + DateTime.Now.ToString("yyyy/M/"), ftpUid, ftpPwd);
                            ftp.UploadFileToFTP((byte[])Session["fileBytes"], ftpHost, Session["cod_cliente"].ToString() + "/" + DateTime.Now.ToString("yyyy/M/"), Session["fileName"].ToString(), ftpUid, ftpPwd);
                            factory.Diagramador().EnviarArquivoDiagramador(1, Convert.ToInt32(ViewState["cod_publicacao_pub"]), Convert.ToInt32(Session["usu_codigo"]), des_observacao_ediTextBox.Text, Session["fileName"].ToString());
                            factory.Arquivo().AlterarTipUtilizadoEditoracao(Convert.ToInt32(ViewState["cod_publicacao_pub"]), "S");
                        }
                        FecharModalDetalhamentoLinkButton_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        Msg.MessageBox(Page, "Erro ao realizar a ação!\nTente novamente.\n" + ex.Message, true);
                        Session["UploadArquivo"] = null;
                    }
                }
                else
                {
                    Msg.MessageBox(Page, "Anexe a edição final em PDF", true);
                    return;
                }
            }
            else
            {
                Msg.MessageBox(Page, "Todos os arquivos dispostos precisam ser utilizados!", true);
            }
        }
        else
        {
            Msg.MessageBox(Page, "Anexe a edição final em PDF!", true);
        }
    }

    protected void ArquivoFinalFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (ArquivoFinalFileUpload.PostedFile.ContentType == "application/pdf")
        {
            Session.Add("fileBytes", ArquivoFinalFileUpload.FileBytes);
            Session.Add("fileName", DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_final.pdf");//"ftp://" + ftpHost + ViewState["cod_publicacao_pub"].ToString() + DateTime.Now.ToString("yyyy/M/") + 
            Session["UploadArquivo"] = true;
            //ModalArquivoFinalFileUpload.Visible = false;
        }
        else
        {
            throw new Exception("O arquivo deve ser PDF!");
        }
    }

    protected void ArquivoFinalFileUpload_UploadedError(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        Msg.MessageBox(Page, e.StatusMessage, true);
    }

    protected void DemandaObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (ViewState["cod_publicacao_pub"] != null)
        {
            e.InputParameters["cod_publicacao_pub"] = ViewState["cod_publicacao_pub"];
        }
    }
}