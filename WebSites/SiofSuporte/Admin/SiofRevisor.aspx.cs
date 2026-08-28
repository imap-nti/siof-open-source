using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SiofRevisor : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            ModalDetalhamentoPlaceholder.Visible = false;
            ModalArquivoAlteradoPlaceHolder.Visible = false;
            ModalArquivoRejeitadoPlaceHolder.Visible = false;
            ModalGerarPublicacaoPlaceHolder.Visible = false;
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

            DsAdmin.Publicacao rn = new DsAdmin.Publicacao();
            System.Data.DataView vw = rn.ConsultarPublicacao(Convert.ToInt32(e.CommandArgument));
            ViewState["cod_cliente"] = vw.Table.Rows[0]["cod_cliente"].ToString();
            ViewState["cod_modelo_edicao_moe"] = 4;//vw.Table.Rows[0]["cod_modelo_edicao_moe"].ToString();
            ViewState["linkArquivo"] = vw.Table.Rows[0]["des_arquivo_url_dod"].ToString();
            HyperLinkArquivoFinal.Text = ViewState["linkArquivo"].ToString();
            ViewState["dat_diagramacao_dod"] = vw.Table.Rows[0]["dat_diagramacao_dod"].ToString();
            des_observacao_ediTextBox.Text = vw.Table.Rows[0]["des_arquivo_final_dod"].ToString();
            UpdatePanel2.Update();
        }
    }

    protected void FecharModalDetalhamentoLinkButton_Click(object sender, EventArgs e)
    {
        ModalGerarPublicacaoPlaceHolder.Visible = false;
        ModalDetalhamentoPlaceholder.Visible = false;
        ViewState["cod_publicacao_pub"] = null;
        UpdatePanel2.Update();
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

    protected void DevolverLinkButton_Click(object sender, EventArgs e)
    {
        ModalArquivoRejeitadoPlaceHolder.Visible = true;
    }

    protected void GerEdicaoLinkButton_Click(object sender, EventArgs e)
    {
        HyperLinkArquivoFinal.Text = ViewState["linkArquivo"].ToString();
        ModalGerarPublicacaoPlaceHolder.Visible = true;
        System.Data.DataView vw = new DsAdmin.Cliente().Consultar(Convert.ToInt32(ViewState["cod_cliente"]));
        tip_assinatura_digitalCheckBox.Checked = vw.Table.Rows[0]["tip_assinatura_digital"] != DBNull.Value ? (vw.Table.Rows[0]["tip_assinatura_digital"].ToString() == "S" ? true : false) : false;
        tip_carimbo_tempoCheckBox.Checked = vw.Table.Rows[0]["tip_carimbo_tempo"] != DBNull.Value ? (vw.Table.Rows[0]["tip_carimbo_tempo"].ToString() == "S" ? true : false) : false;
        UpdatePanel5.Update();
    }

    protected void VisualizarLinkButton_Click(object sender, EventArgs e)
    {
        //Codigo do documento e modelo usados na idêntificação. 
        Int32 cod_publicacao_pub = Convert.ToInt32(ViewState["cod_publicacao_pub"]);
        Int32 cod_modelo_edicao_moe = Convert.ToInt32(ViewState["cod_modelo_edicao_moe"]);

        //Preenche um dataset com duas tabelas contendo dados sobre o cliente e a publicação. 
        System.Data.DataSet ds = new System.Data.DataSet();
        ds.Tables.Add(new DsAdmin.Arquivo().ListarArquivoPublicacao(cod_publicacao_pub).Table.Copy());
        ds.Tables.Add(new DsAdmin.Cliente().Consultar(Convert.ToInt32(ViewState["cod_cliente"])).Table.Copy());

        //Cria os arquivos necessários para a manipulação do PDF.
        System.Data.DataView vw = new DsAdmin.ModeloEdicao().Consultar(cod_modelo_edicao_moe);

        //Baixa arquivos enviados pelo diagramador junto ao modelo de capa e contra-capa a serem agregados numa unica publicação.
        DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();

        byte[] model = ftp.DownloadFileFromFTP("ftp://" + ftpModel + "modelo.pdf", ftpUid, ftpPwd);

        Assinador2.IMAPModeloEdicao modelo = new Assinador2.IMAPModeloEdicao(model, vw);
        byte[] file = ftp.DownloadFileFromFTP("ftp://" + ftpHost + ViewState["cod_cliente"].ToString() + "/" + Convert.ToDateTime(ViewState["dat_diagramacao_dod"]).ToString("yyyy/M/") + ViewState["linkArquivo"].ToString(), ftpUid, ftpPwd); //Observar caminho amanhã

        //Gera o arquivo com o modelo e a edição do diagramador.
        byte[] pdf = new Assinador2.PDFHandler().GerarEdicao(modelo, file, "IMAP_IMAP_IMAP_IMAP_IMAP_IMAP", "11124", 11124, ds, DateTime.Now);

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", pdf.Length.ToString());
        Response.AddHeader("content-disposition", "attachment; filename=" + "publicação" + ".pdf");
        Response.BinaryWrite(pdf);
    }

    protected void CancelarLinkButton_Click(object sender, EventArgs e)
    {

    }

    protected void FecharModalGerarPublicacaoLinkButton_Click(object sender, EventArgs e)
    {
        ModalGerarPublicacaoPlaceHolder.Visible = false;
    }

    protected void PublicarLinkButton_Click(object sender, EventArgs e)
    {
        if (ItensUtilizadosCheckBoxList_Cheks())
        {
            //Codigo do documento e modelo usados nas idêntificações. 
            Int32 cod_publicacao_pub = Convert.ToInt32(ViewState["cod_publicacao_pub"]);
            Int32 cod_modelo_edicao_moe = Convert.ToInt32(ViewState["cod_modelo_edicao_moe"]);
            Int32 cod_cliente = Convert.ToInt32(ViewState["cod_cliente"]);
            DateTime data = Convert.ToDateTime(ViewState["dat_diagramacao_dod"]);

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.Tables.Add(new DsAdmin.Arquivo().ListarArquivoPublicacao(cod_publicacao_pub).Table.Copy());
            ds.Tables.Add(new DsAdmin.Cliente().Consultar(cod_cliente).Table.Copy());
            using (DsAdmin.Factory factory = new DsAdmin.Factory())
            {
                DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();

                //Altera campos no banco e retorna a data da criação do documento
                DateTime? dat_revisao_dod = null;
                if (Session["UpdatedFileName"] == null)
                {
                    factory.Revisor().PublicarDocumentoRevisado(2, Convert.ToInt32(Session["usu_codigo"]),
                        des_observacao_ediTextBox.Text, Convert.ToInt32(ViewState["cod_publicacao_pub"]), ref dat_revisao_dod);
                }
                else
                {
                    //Cria um diretório com hierarquia de pastas análoga aos dados do cliente e envia o arquivo alterado pelo revisor ao repositório.
                    ftp.CreateDirectoryInFTP("ftp://" + ftpHost + cod_cliente + "/" + DateTime.Now.ToString("yyyy/M/"), ftpUid, ftpPwd);
                    ftp.UploadFileToFTP((byte[])Session["UpdatedFileBytes"],
                        ftpHost, cod_cliente + "/" + DateTime.Now.ToString("yyyy/M/"), Session["UpdatedFileName"].ToString(), ftpUid, ftpPwd);

                    factory.Revisor().PublicarDocumentoAlterado(2, Convert.ToInt32(Session["usu_codigo"]), des_motivo_motTextBox.Text, "S",
                        System.IO.Path.GetFileName(Session["UpdatedFileName"].ToString()), Convert.ToInt32(ViewState["cod_publicacao_pub"]), ref dat_revisao_dod);
                }

                factory.Arquivo().AlterarTipUtilizadoRevisao(Convert.ToInt32(ViewState["cod_publicacao_pub"]), "S");

                //Cria os arquivos necessários para a manipulação do PDF.
                System.Data.DataView vw = factory.ModeloEdicao().Consultar(cod_modelo_edicao_moe);

                //Baixa arquivos enviados pelo diagramador junto ao modelo de capa e contra-capa a serem agregados numa unica publicação.
                byte[] model = ftp.DownloadFileFromFTP("ftp://" + ftpModel + "modelo.pdf", ftpUid, ftpPwd); //Observar caminho amanhã
                Assinador2.IMAPModeloEdicao modelo = new Assinador2.IMAPModeloEdicao(model, vw);
                byte[] file = ftp.DownloadFileFromFTP("ftp://" + ftpHost + ViewState["cod_cliente"].ToString() + "/" + Convert.ToDateTime(ViewState["dat_diagramacao_dod"]).ToString("yyyy/M/") + ViewState["linkArquivo"].ToString(), ftpUid, ftpPwd); //Observar caminho amanhã

                // Inclusão do documento 
                string des_anoedicao = "";
                Int32 doc_codigo = 0;
                factory.Publicacao().Incluir(Assinador2.PDFHandler.getPages(file),
                    cod_cliente, dat_revisao_dod, 1, ref des_anoedicao, des_observacao_ediTextBox.Text, 2, "des_arquivofonte",
                    "des_arquivoa4", "des_arquivoa3", Session["usu_codigo"].ToString(), "des_responsavel",
                    Request.UserHostAddress, Request.UserHostName, 1, new System.Xml.XmlDocument(), ref doc_codigo);

                //Gera o arquivo com o modelo e a edição do diagramador.
                string des_certificacao = Assinador2.PDFHandler.pegaHash(data.Day.ToString(), data.ToString("MMMM"), data.Year.ToString(), cod_cliente, doc_codigo.ToString());
                byte[] pdf = new Assinador2.PDFHandler().GerarEdicao(modelo, file, des_certificacao,
                   des_anoedicao, doc_codigo, ds, Convert.ToDateTime(ViewState["dat_diagramacao_dod"]));

                //Upload do documento pdf publicado
                ftp.CreateDirectoryInFTP("ftp://" + ftpHost + "Documentos/", ftpUid, ftpPwd);
                ftp.UploadFileToFTP(tip_assinatura_digitalCheckBox.Checked ? new Assinador2.Signer().AssinarDocumento(pdf) : pdf, ftpHost, "Documentos/", DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + ".pdf", ftpUid, ftpPwd);

                //Método para obter o hash ( base64 sha1 ) do arquivo pdf para carimbo de tempo
                Assinador2.Stamper stamper = new Assinador2.Stamper();
                string hash_file = stamper.GetBase64SHA1HashFromFile(pdf);

                //Método para carimbar hash do arquivo e baixar o carimbo de tempo
                Assinador2.CarimboTempo carimbo = new Assinador2.CarimboTempo(hash_file);
                byte[] carimbo_tempo = carimbo.Carimbar();

                //Método para obter hash ( base64 sha1 ) do carimbo de tempo para assinatura xmldsign
                string hash_tst = stamper.GetBase64SHA1HashFromFile(carimbo_tempo);//stamper.GetBase64SHA1HashFromFile(System.IO.File.ReadAllBytes(@"C:\Users\Danilo-PC\Desktop\CarimboDeTempo.tst"));

                System.Xml.XmlDocument xml = stamper.GerarXMLCarimboTempo(hash_tst);

                xml.InnerXml = xml.InnerXml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");

                //Inclusão dos dados pertinentes a assinatura xmldsign, carimbo e hash da publicação no banco de dados
                factory.Publicacao().AlterarDocumento(xml, carimbo_tempo, hash_file, des_certificacao, doc_codigo, cod_cliente);//new Assinador2.Signer().AssinarXMLDsign(hash_tst)
            }
            FecharModalDetalhamentoLinkButton_Click(null, null);
        }
        else
        {
            Msg.MessageBox(Page, "Todos os arquivos dispostos precisam ser revisados!", true);
        }
    }

    protected void AlterarLinkButton_Click(object sender, EventArgs e)
    {
        des_motivo_motTextBox.Text = des_observacao_ediTextBox.Text;
        ModalArquivoAlteradoPlaceHolder.Visible = true;
        HyperLinkArquivoFinal.Text = ViewState["linkArquivo"].ToString();
        UpdatePanel3.Update();
    }

    protected void FecharModalArquivoAlteradoLinkButton_Click(object sender, EventArgs e)
    {
        ModalArquivoAlteradoPlaceHolder.Visible = false;
        Session["UpdatedFileName"] = null;
    }

    protected void PublicarArquivoAlteradoLinkButton_Click(object sender, EventArgs e)
    {
        try
        {
            //Transação para a publicação.
            using (DsAdmin.Factory factory = new DsAdmin.Factory())
            {
                DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();

                //Codigo do documento e modelo usados na idêntificação. 
                Int32 cod_publicacao_pub = Convert.ToInt32(ViewState["cod_publicacao_pub"]);
                Int32 cod_modelo_edicao_moe = Convert.ToInt32(ViewState["cod_modelo_edicao_moe"]);
                Int32 cod_cliente = Convert.ToInt32(ViewState["cod_cliente"]);
                DateTime data = Convert.ToDateTime(ViewState["dat_diagramacao_dod"]);
                Int32 doc_codigo = 0;

                //Cria um diretório com hierarquia de pastas análoga aos dados do cliente.
                ftp.CreateDirectoryInFTP("ftp://" + ftpHost + Session["cod_cliente"].ToString() + "/" + DateTime.Now.ToString("yyyy/M/"), ftpUid, ftpPwd);
                ftp.UploadFileToFTP((byte[])Session["UpdatedFileBytes"],
                    ftpHost, Session["cod_cliente"].ToString() + "/" + DateTime.Now.ToString("yyyy/M/"), Session["UpdatedFileName"].ToString(), ftpUid, ftpPwd);

                //Altera campos do banco.
                DateTime? dat_revisao_dod = null;
                factory.Revisor().PublicarDocumentoAlterado(2, Convert.ToInt32(Session["usu_codigo"]),
                    des_motivo_motTextBox.Text, "S", System.IO.Path.GetFileName(Session["UpdatedFileName"].ToString()), Convert.ToInt32(ViewState["cod_publicacao_pub"]), ref dat_revisao_dod);
                factory.Arquivo().AlterarTipUtilizadoRevisao(cod_publicacao_pub, "S");

                //Cria os arquivos necessários para a manipulação do PDF.
                System.Data.DataView vw = factory.ModeloEdicao().Consultar(cod_modelo_edicao_moe);

                //Baixa arquivos enviados pelo diagramador junto ao modelo de capa e contra-capa a serem agregados numa unica publicação.
                byte[] model = ftp.DownloadFileFromFTP("ftp://" + ftpHost + "modelo/modelo.pdf", ftpUid, ftpPwd);
                Assinador2.IMAPModeloEdicao modelo = new Assinador2.IMAPModeloEdicao(model, vw);
                byte[] file = ftp.DownloadFileFromFTP("ftp://" + ftpHost + "modelo/edicao.pdf", ftpUid, ftpPwd);

                //Gera o arquivo com o modelo e a edição do diagramador.
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.Tables.Add(new DsAdmin.Arquivo().ListarArquivoPublicacao(cod_publicacao_pub).Table.Clone());
                ds.Tables.Add(new DsAdmin.Cliente().ConsultarClientePorDocumento(cod_publicacao_pub).Table.Clone());

                // Inclusão do documento 
                string des_anoedicao = "";
                factory.Publicacao().Incluir(Assinador2.PDFHandler.getPages(file),
                    cod_cliente, dat_revisao_dod, 1, ref des_anoedicao, des_observacao_ediTextBox.Text, 2, "des_arquivofonte",
                    "des_arquivoa4", "des_arquivoa3", Session["usu_codigo"].ToString(), "des_responsavel",
                    Request.UserHostAddress, Request.UserHostName, 1, new System.Xml.XmlDocument(), ref doc_codigo);

                //Gera o arquivo com o modelo e a edição do diagramador.
                byte[] pdf = new Assinador2.PDFHandler().GerarEdicao(modelo, file,
                    Assinador2.PDFHandler.pegaHash(data.Day.ToString(), data.ToString("MMMM"), data.Year.ToString(), cod_cliente, doc_codigo.ToString()),
                   des_anoedicao, doc_codigo, ds, Convert.ToDateTime(ViewState["dat_diagramacao_dod"]));

                //Cria diretório no repositório ftp e envia uma cópia do arquivo gerado.
                ftp.CreateDirectoryInFTP("ftp://" + ftpHost + "Documentos/", ftpUid, ftpPwd);
                ftp.UploadFileToFTP(pdf, ftpHost, "Documentos/", DateTime.Now.ToString("yyyy_MM_dd") + ".pdf", ftpUid, ftpPwd);
            }
            FecharModalArquivoAlteradoLinkButton_Click(null, null);
            ModalDetalhamentoPlaceholder.Visible = false;
            UpdatePanel2.Update();
        }
        catch (Exception ex)
        {
            Msg.MessageBox(Page, ex.Message, true);
        }
    }

    protected void FecharModalArquivoRejeitadoLinkButton_Click(object sender, EventArgs e)
    {
        ModalArquivoRejeitadoPlaceHolder.Visible = false;
        //ModalDetalhamentoPlaceholder.Visible = false;
        des_ArquivoRejeitadoTextBox.Text = "";
        //UpdatePanel3.Update();
    }

    protected void ArquivoRejeitadoLinkButton_Click(object sender, EventArgs e)
    {
        using (DsAdmin.Factory factory = new DsAdmin.Factory())
        {
            factory.Revisor().DevolverPublicacao(3, Convert.ToInt32(Session["usu_codigo"]), Convert.ToInt32(ViewState["cod_publicacao_pub"]), des_ArquivoRejeitadoTextBox.Text);
            factory.Arquivo().AlterarTipUtilizadoRevisao(Convert.ToInt32(ViewState["cod_publicacao_pub"]), "S");
        }
        FecharModalArquivoRejeitadoLinkButton_Click(null, null);
        FecharModalDetalhamentoLinkButton_Click(null, null);
    }

    protected void DemandaObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (ViewState["cod_publicacao_pub"] != null)
        {
            e.InputParameters["cod_publicacao_pub"] = ViewState["cod_publicacao_pub"];
        }
    }

    protected void ArquivoAlteradoFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (ArquivoAlteradoFileUpload.PostedFile.ContentType == "application/pdf")
        {
            Session.Add("UpdatedFileBytes", ArquivoAlteradoFileUpload.FileBytes);
            Session.Add("UpdatedFileName", DateTime.Now.ToString("yyyy_MM_dd_HHmmss") + "_final_rev.pdf");
        }
        else
        {
            throw new Exception("O arquivo deve ser PDF!");
        }
    }

    protected void ArquivoAlteradoFileUpload_UploadedError(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        Msg.MessageBox(Page, e.StatusMessage, true);
    }

    protected void HyperLinkArquivoFinal_Click(object sender, EventArgs e)
    {
        Response.Redirect("DownloadHandler.ashx?b=" + ViewState["cod_publicacao_pub"].ToString());
    }
}