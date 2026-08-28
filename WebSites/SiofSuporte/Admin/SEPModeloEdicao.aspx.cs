using System;
using Microsoft.VisualBasic;
using System.Web.UI.WebControls;

public partial class admin_SEPModeloEdicao : System.Web.UI.Page
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

        //verifica se a ação está nula, 
        //preenche com "cadastrar" no primeiro momento que a tela abrir,
        //para inicializar a variavel
        if (ViewState["acao"] == null && ViewState["acaoModal"] == null)
        {
            ViewState["acao"] = "cadastrar";
            ViewState["acaoModal"] = "cadastrar";
        }
        if (!IsPostBack && ViewState["cod_modelo_edicao_moe"] == null)
        {
            ModalCamposModeloPlaceHolder.Visible = false;
        }
        else if (ViewState["cod_modelo_edicao_moe"] != null)
        {
            ModalCamposModeloPlaceHolder.Visible = true;
        }
        base.OnLoad(e);
    }

    protected void grvPrincipal_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

        //verifica se clicou em editar ou excluir
        if (e.CommandName == "btnExcluir")
        {
            //exclui o item selecionado
            //e chama o cancelar para limpar tela e carregar o grid
            DsAdmin.ModeloEdicao rn = new DsAdmin.ModeloEdicao();
            rn.Excluir(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["usu_codigo"]));
            CancelarLinkButton_Click(null, null);

        }
        else if (e.CommandName == "btnEditar")
        {

            //carrega os dados do item selecionado para permitir a edição
            DsAdmin.ModeloEdicao rn = new DsAdmin.ModeloEdicao();
            System.Data.DataView vw;

            //1) consulta os dados do banco
            vw = rn.Consultar(Convert.ToInt32(e.CommandArgument.ToString()));

            //2) preenche os campos da tela
            des_modelo_edicao_moeTextBox.Text = vw.Table.Rows[0]["des_modelo_edicao_moe"].ToString();
            //3) coloca o codigo do item selecionado no ViewState, para permitir a alteração no botão confirmar (CadastrarLinkButton_Click)
            ViewState["cod_modelo_edicao_moe"] = e.CommandArgument;
            //4) coloca a ação a ser realizada ao clicar no confirmar (CadastrarLinkButton_Click)
            ViewState["acao"] = "alterar";
            //5) altera o texto do botão cadastrar (CadastrarLinkButton), para confirmar
            CadastrarLinkButton.Text = "Confirmar";
        }
        else if (e.CommandName == "btnIncluirCampos")
        {
            ViewState["cod_modelo_edicao_moe"] = e.CommandArgument;
            ModalCamposModeloPlaceHolder.Visible = true;
            TipoCampoRadioButtonList.SelectedValue = "1";
            des_regra_camTextBox.Text = "{ 0 }";
            TextoPlaceholder.Visible = true;
        }
        else if (e.CommandName == "btnGerarEdicao")
        {
            using (DsAdmin.Factory factory = new DsAdmin.Factory())
            {
                Int32 cod_modelo_edicao_moe = Convert.ToInt32(e.CommandArgument);

                //Suposto codigo do documento para vincular o modelo de edição a um cliente
                //Int32 doc_codigo = Convert.ToInt32(Context.Items["doc_codigo"]);

                //Gerenciador de conexção FTP
                DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();

                //Cria os arquivos necessários para a manipulação do PDF.
                System.Data.DataView vw = factory.ModeloEdicao().Consultar(cod_modelo_edicao_moe);
                byte[] model = ftp.DownloadFileFromFTP("ftp://" + ftpHost + "modelo/modelo.pdf", ftpUid, ftpPwd);
                Assinador2.IMAPModeloEdicao modelo = new Assinador2.IMAPModeloEdicao(model, vw);
                byte[] file = ftp.DownloadFileFromFTP("ftp://" + ftpHost + "modelo/edicao.pdf", ftpUid, ftpPwd);

                //Gera o arquivo com o modelo e a edição do diagramador.
                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.DataTable tb = new DsAdmin.Cliente().Consultar(11124).Table.Copy();
                tb.TableName = "tb01";
                ds.Tables.Add(tb);
                ds.Tables.Add(new DsAdmin.Cliente().Consultar(11124).Table.Copy());
                //int ret = 0;
                //string des_certificacao = "";
                byte[] pdf = new Assinador2.PDFHandler().GerarEdicao(modelo, file, "IMAP_IMAP_IMAP_IMAP_IMAP_IMAP", "X", 11124, ds, Convert.ToDateTime(ViewState["dat_diagramacao_dod"]));

                //Cria diretório no repositório ftp e envia uma cópia do arquivo gerado.
                //ftp.CreateDirectoryInFTP("ftp://" + ftpHost + "Documentos/", ftpUid, ftpPwd);
                //ftp.UploadFileToFTP(pdf, ftpHost, "Documentos/", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".pdf", ftpUid, ftpPwd);

                //Envia o arquivo gerado junto a resposta para o cliente.
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", pdf.Length.ToString());
                Response.AddHeader("content-disposition", "attachment; filename=" + "publicação" + ".pdf");
                Response.BinaryWrite(pdf);

                factory.Commit();
                //Inclui as informações no banco
                // alterar procedure para realizar o update identificando o cliente  através do codigo do documento
                //factory.Arquivo().AlterarIoCilente(cod_modelo_edicao_moe, doc_codigo);
            }
        }
        else if (e.CommandName == "btnIncluirCaderno")
        {
            ViewState["cod_modelo_edicao_moe1"] = e.CommandArgument;
            ModalCadernoModeloPlaceHolderPlaceHolder.Visible = true;
        }
    }

    protected void FecharModalCadernoModeloLinkButton_Click(object sender, EventArgs e)
    {
        ModalCadernoModeloPlaceHolderPlaceHolder.Visible = false;
        ViewState["cod_modelo_edicao_moe1"] = null;
    }

    protected void grvCadernoModelo_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        //verifica se clicou em editar ou excluir
        if (e.CommandName == "btnExcluir")
        {
            //exclui o item selecionado
            //e chama o cancelar para limpar tela e carregar o grid
            DsAdmin.CadernoModelo rn = new DsAdmin.CadernoModelo();
            rn.Excluir(Convert.ToInt32(e.CommandArgument.ToString()));
            CancelarCadernoModeloLinkButton_Click(null, null);
        }
    }

    protected void CancelarCadernoModeloLinkButton_Click(object sender, EventArgs e)
    {
        des_caderno_modelo_camTextBox.Text = "";
        num_ordem_camTextBox.Text = "";
        grvCadernoModelo.DataBind();
    }

    protected void CadastrarCadernoModeloLinkButton_Click(object sender, EventArgs e)
    {
        Int32 retorno = 0;
        new DsAdmin.CadernoModelo().Incluir(des_caderno_modelo_camTextBox.Text, Convert.ToInt32(ViewState["cod_modelo_edicao_moe1"]), Convert.ToInt32(num_ordem_camTextBox.Text), ref retorno);
        CancelarCadernoModeloLinkButton_Click(null, null);
    }

    protected void grvCadernoModeloObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["cod_modelo_edicao_moe"] = ViewState["cod_modelo_edicao_moe1"];
    }

    protected void CadastrarLinkButton_Click(Object sender, System.EventArgs e)
    {
        if (Session["ModeloEdicao"] != null && Session["ImagemCapa"] != null)
        {
            //cria uma transação para incluir ou alterar os dados dependendo da ação selecionada
            using (DsAdmin.Factory factory = new DsAdmin.Factory())
            {
                DsAdmin.ModeloEdicao rn = factory.ModeloEdicao();

                Int32 retorno = 0;

                if (ViewState["acao"].ToString() == "cadastrar")
                {
                    //se for incluir, chama o metodo de inclusão e passa os parametros
                    rn.Incluir(Convert.ToInt32(Session["usu_codigo"]), des_modelo_edicao_moeTextBox.Text, Convert.ToInt32(cod_fonte_modelo_fomDropDownList.SelectedValue), ref retorno);

                    DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();
                    //conversão pre upload

                    System.Drawing.Image imagemCapa = System.Drawing.Image.FromStream(new System.IO.MemoryStream((byte[])Session["ImagemCapa"]));

                    System.Drawing.Image redImg = imagemCapa.GetThumbnailImage(86, 122, null, new IntPtr(0));
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    redImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    try
                    {
                        ftp.UploadFileToFTP((byte[])Session["ModeloEdicao"], ftpModel, "modelo/", retorno.ToString() + ".pdf", ftpUid, ftpPwd);
                        //ftp.UploadFileToFTP((byte[])Session["PaginaInicial"], ftpHost, "modelo/", retorno.ToString() + ".pdf", ftpUid, ftpPwd);
                        ftp.UploadFileToFTP(ms.ToArray(), ftpModel, "modelo/", retorno.ToString() + ".jpg", ftpUid, ftpPwd);
                        //ftp.UploadFileToFTP(ConvertImageToJpeg(System.Drawing.Image.FromStream(new System.IO.MemoryStream((byte[])Session["arq_imagem_cam"]))), ftpHost, "Documentos/", ViewState["arq_imagem_cam"] + ".jpg", ftpUid, ftpPwd);
                    }
                    catch (Exception ex)
                    {
                        Msg.MessageBox(Page, "Erro de: " + ex.Message, true);
                        throw ex;
                    }
                    imagemCapa.Dispose();
                    redImg.Dispose();
                }
                else if (ViewState["acao"].ToString() == "alterar")
                {
                    //se for alteração , chama o metodo de alteração e passa os parametros
                    //rn.Alterar(Convert.ToInt32(Session["cod_usuario_usu"]));
                }

                factory.Commit();
            } //fecha transação
        }
        else
        {
            Msg.MessageBox(Page, "Anexe os arquivos de Capa, Primeira Página e Modelo!", true);
        }
        //chama o metodo Cancelar para limpar os campos e atualizar o Grid
        CancelarLinkButton_Click(null, null);

    }

    public byte[] ConvertImageToJpeg(System.Drawing.Image img)
    {
        System.Drawing.Image redImg = img.GetThumbnailImage(img.Width, img.Height, null, new IntPtr(0));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        redImg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
    }

    public static byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
    }

    protected void CancelarLinkButton_Click(Object sender, System.EventArgs e)
    {

        //limpa os campos da tela, atualiza o grid, coloca a ação em modo de cadastrar e altera o texto do botão

        //1) limpa os campos
        des_modelo_edicao_moeTextBox.Text = "";
        //2) coloca a ação cadastrar no ViewState
        ViewState["acao"] = "cadastrar";
        Session["ModeloEdicao"] = null;
        Session["ImagemCapa"] = null;
        Session["cod_campo_modelo_cam"] = null;
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

    protected void grvModalPrincipal_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

        //verifica se clicou em editar ou excluir
        if (e.CommandName == "btnExcluir")
        {
            //exclui o item selecionado
            //e chama o cancelar para limpar tela e carregar o grid
            DsAdmin.CamposModelo rn = new DsAdmin.CamposModelo();
            rn.Excluir(Convert.ToInt32(e.CommandArgument.ToString()));
            CancelarLinkButton_Click(null, null);

        }
        else if (e.CommandName == "btnEditar")
        {

            //carrega os dados do item selecionado para permitir a edição
            DsAdmin.CamposModelo rn = new DsAdmin.CamposModelo();
            System.Data.DataView vw;

            //1) consulta os dados do banco
            vw = rn.Consultar(Convert.ToInt32(e.CommandArgument.ToString()));

            //2) preenche os campos da tela
            des_nome_camTextBox.Text = vw.Table.Rows[0]["des_nome_cam"].ToString();
            des_regra_camTextBox.Text = vw.Table.Rows[0]["des_regra_cam"].ToString();
            num_height_camTextBox.Text = vw.Table.Rows[0]["num_height_cam"].ToString().Replace(".", ",");
            num_width_camTextBox.Text = vw.Table.Rows[0]["num_width_cam"].ToString().Replace(".", ",");
            num_top_camTextBox.Text = vw.Table.Rows[0]["num_top_cam"].ToString().Replace(".", ",");
            num_left_camTextBox.Text = vw.Table.Rows[0]["num_left_cam"].ToString().Replace(".", ",");
            TipoCampoRadioButtonList.SelectedValue = vw.Table.Rows[0]["arq_imagem_cam"].ToString() != "" ? "2" : "1";
            if (TipoCampoRadioButtonList.SelectedValue == "1")
            {
                ImagemPlaceHolder.Visible = false;
                TextoPlaceholder.Visible = true;
            }
            else
            {
                TextoPlaceholder.Visible = false;
                ImagemPlaceHolder.Visible = true;
            }
            //cod_modelo_edicao_moeDropDownList.SelectedValue = vw.Table.Rows[0]["cod_modelo_edicao_moe"].ToString();
            cod_pagina_modelo_pamRadioButtonList.SelectedValue = vw.Table.Rows[0]["cod_pagina_modelo_pam"].ToString();
            cod_fonte_modelo_fomDropDownList.SelectedValue = vw.Table.Rows[0]["cod_fonte_modelo_fom"].ToString();
            num_tamanho_fonte_camTextBox.Text = vw.Table.Rows[0]["num_tamanho_fonte_cam"].ToString();
            tip_alinhamento_fonte_camRadioButtonList.SelectedValue = vw.Table.Rows[0]["tip_alinhamento_fonte_cam"].ToString();
            tip_negrito_fonte_camRadioButtonList.SelectedValue = vw.Table.Rows[0]["tip_negrito_fonte_cam"].ToString();
            tip_sublinhado_fonte_camRadioButtonList.SelectedValue = vw.Table.Rows[0]["tip_sublinhado_fonte_cam"].ToString();
            tip_italico_fonte_camRadioButtonList.SelectedValue = vw.Table.Rows[0]["tip_italico_fonte_cam"].ToString();
            //arq_imagem_camTextBox.Text = vw.Table.Rows[0]["arq_imagem_cam"].ToString();
            //3) coloca o codigo do item selecionado no ViewState, para permitir a alteração no botão confirmar (CadastrarLinkButton_Click)
            Session["cod_campo_modelo_cam"] = e.CommandArgument;
            //4) coloca a ação a ser realizada ao clicar no confirmar (CadastrarLinkButton_Click)
            ViewState["acaoModal"] = "alterar";
            //5) altera o texto do botão cadastrar (CadastrarLinkButton), para confirmar
            CadastrarModalLinkButton.Text = "Confirmar";
        }
    }

    protected void CadastrarModalLinkButton_Click(Object sender, System.EventArgs e)
    {

        //cria uma transação para incluir ou alterar os dados dependendo da ação selecionada
        using (DsAdmin.Factory factory = new DsAdmin.Factory())
        {

            DsAdmin.CamposModelo rn = factory.CamposModelo();

            Int32 retorno = 0;

            if (ViewState["acaoModal"].ToString() == "cadastrar")
            {
                //se for incluir, chama o metodo de inclusão e passa os parametros
                rn.Incluir(
                    des_nome_camTextBox.Text,
                    des_regra_camTextBox.Text,
                    Convert.ToDecimal(Information.IsNumeric(num_height_camTextBox.Text) ? num_height_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToDecimal(Information.IsNumeric(num_width_camTextBox.Text) ? num_width_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToDecimal(Information.IsNumeric(num_top_camTextBox.Text) ? num_top_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToDecimal(Information.IsNumeric(num_left_camTextBox.Text) ? num_left_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToInt32(ViewState["cod_modelo_edicao_moe"]),
                    Convert.ToInt32(Information.IsNumeric(cod_fonte_modelo_fomDropDownList.SelectedValue) ? cod_fonte_modelo_fomDropDownList.SelectedValue : null),
                    Convert.ToInt32(Information.IsNumeric(num_tamanho_fonte_camTextBox.Text) ? num_tamanho_fonte_camTextBox.Text : null),
                    tip_alinhamento_fonte_camRadioButtonList.SelectedValue,
                    tip_negrito_fonte_camRadioButtonList.SelectedValue,
                    tip_sublinhado_fonte_camRadioButtonList.SelectedValue,
                    tip_italico_fonte_camRadioButtonList.SelectedValue,
                    ViewState["arq_imagem_cam"] == null ? null : ViewState["arq_imagem_cam"].ToString(),
                    Convert.ToInt32(Information.IsNumeric(cod_pagina_modelo_pamRadioButtonList.SelectedValue)),
                    ref retorno);
                if (ViewState["arq_imagem_cam"] != null)
                {
                    DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();
                    ftp.CreateDirectoryInFTP(System.IO.Path.GetDirectoryName(ViewState["arq_imagem_cam"].ToString()), ftpUid, ftpPwd);
                    ftp.UploadFileToFTP((byte[])Session["arq_imagem_cam"], ftpHost, "modelo/", System.IO.Path.GetFileName(ViewState["arq_imagem_cam"].ToString()), ftpUid, ftpPwd);
                }
            }
            else if (ViewState["acaoModal"].ToString() == "alterar")
            {
                //se for alteração , chama o metodo de alteração e passa os parametros
                rn.Alterar(
                    des_nome_camTextBox.Text,
                    des_regra_camTextBox.Text,
                    Convert.ToDecimal(Information.IsNumeric(num_height_camTextBox.Text) ? num_height_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToDecimal(Information.IsNumeric(num_width_camTextBox.Text) ? num_width_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToDecimal(Information.IsNumeric(num_top_camTextBox.Text) ? num_top_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToDecimal(Information.IsNumeric(num_left_camTextBox.Text) ? num_left_camTextBox.Text.Replace('.', ',') : null),
                    Convert.ToInt32(ViewState["cod_modelo_edicao_moe"]),
                    Convert.ToInt32(Information.IsNumeric(cod_fonte_modelo_fomDropDownList.SelectedValue) ? cod_fonte_modelo_fomDropDownList.SelectedValue : null),
                    Convert.ToInt32(Information.IsNumeric(num_tamanho_fonte_camTextBox.Text) ? num_tamanho_fonte_camTextBox.Text : null),
                    tip_alinhamento_fonte_camRadioButtonList.SelectedValue,
                    tip_negrito_fonte_camRadioButtonList.SelectedValue,
                    tip_sublinhado_fonte_camRadioButtonList.SelectedValue,
                    tip_italico_fonte_camRadioButtonList.SelectedValue,
                    ViewState["arq_imagem_cam"] == null ? null : ViewState["arq_imagem_cam"].ToString(),
                    Convert.ToInt32(cod_pagina_modelo_pamRadioButtonList.SelectedValue),
                    Convert.ToInt32(Session["cod_campo_modelo_cam"]));
                if (ViewState["arq_imagem_cam"] != null)
                {
                    DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();
                    ftp.CreateDirectoryInFTP(System.IO.Path.GetDirectoryName(ViewState["arq_imagem_cam"].ToString()), ftpUid, ftpPwd);
                    ftp.UploadFileToFTP((byte[])Session["arq_imagem_cam"], ftpHost, "modelo/", System.IO.Path.GetFileName(ViewState["arq_imagem_cam"].ToString()), ftpUid, ftpPwd);
                }
            }
            factory.Commit();
        } //fecha transação

        //chama o metodo Cancelar para limpar os campos e atualizar o Grid
        CancelarModalLinkButton_Click(null, null);

    }

    protected void CancelarModalLinkButton_Click(Object sender, System.EventArgs e)
    {

        //limpa os campos da tela, atualiza o grid, coloca a ação em modo de cadastrar e altera o texto do botão

        //1) limpa os campos
        des_nome_camTextBox.Text = "";
        //des_regra_camTextBox.Text = "";
        num_height_camTextBox.Text = "";
        num_width_camTextBox.Text = "";
        num_top_camTextBox.Text = "";
        num_left_camTextBox.Text = "";
        //cod_modelo_edicao_moeDropDownList.SelectedIndex = -1;
        cod_fonte_modelo_fomDropDownList.SelectedIndex = -1;
        num_tamanho_fonte_camTextBox.Text = "";
        tip_alinhamento_fonte_camRadioButtonList.SelectedIndex = -1;
        tip_negrito_fonte_camRadioButtonList.SelectedIndex = -1;
        tip_sublinhado_fonte_camRadioButtonList.SelectedIndex = -1;
        tip_italico_fonte_camRadioButtonList.SelectedIndex = -1;
        cod_pagina_modelo_pamRadioButtonList.SelectedIndex = -1;
        //arq_imagem_camTextBox.Text = "";
        ViewState["arq_imagem_cam"] = null;
        Session["arq_imagem_cam"] = null;
        TipoCampoRadioButtonList.SelectedValue = "1";
        des_regra_camTextBox.Text = "{ 0 }";
        ImagemPlaceHolder.Visible = false;
        TextoPlaceholder.Visible = true;
        //2) coloca a ação cadastrar no ViewState
        ViewState["acaoModal"] = "cadastrar";
        //3) altera o texto do botão cadastrar
        CadastrarModalLinkButton.Text = "Cadastrar";
        //4) atualiza o Grid
        grvModalPrincipal.DataBind();
    }

    protected void FecharModalCamposModeloLinkButton_Click(object sender, EventArgs e)
    {
        Session["cod_campo_modelo_cam"] = null;
        ViewState["cod_modelo_edicao_moe"] = null;
        ModalCamposModeloPlaceHolder.Visible = false;
    }

    protected void ArquivoModeloEdicao_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (ArquivoModeloEdicao.FileName.EndsWith(".pdf"))
        {
            Session["ModeloEdicao"] = ArquivoModeloEdicao.FileBytes;
        }
        else
        {
            Msg.MessageBox(Page, "O arquivo modelo anexo deve ser PDF!", true);
        }
    }

    protected void arq_imagem_camAsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (arq_imagem_camAsyncFileUpload.FileName.EndsWith(".jpg") || arq_imagem_camAsyncFileUpload.FileName.EndsWith(".jpeg") || arq_imagem_camAsyncFileUpload.FileName.EndsWith(".png") || arq_imagem_camAsyncFileUpload.FileName.EndsWith(".bmp") || arq_imagem_camAsyncFileUpload.FileName.EndsWith(".gif"))
        {
            Session["arq_imagem_cam"] = arq_imagem_camAsyncFileUpload.FileBytes;
            ViewState["arq_imagem_cam"] = "ftp://" + ftpHost + "Documentos/" + arq_imagem_camAsyncFileUpload.FileName;
        }
        else
        {
            throw new Exception("O arquivo deve ter as seguintes extênsões: JPEG/JPG/PNG/BMP/GIF!");
            //Msg.MessageBox(Page, "O arquivo modelo anexo deve ser PDF!", true);
        }
    }

    protected void ArquivoImagemCapa_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (ArquivoImagemCapa.FileName.EndsWith(".jpg") || ArquivoImagemCapa.FileName.EndsWith(".jpeg") || ArquivoImagemCapa.FileName.EndsWith(".png") || ArquivoImagemCapa.FileName.EndsWith(".bmp") || ArquivoImagemCapa.FileName.EndsWith(".gif"))
        {
            Session["ImagemCapa"] = ArquivoImagemCapa.FileBytes;
        }
        else
        {
            Msg.MessageBox(Page, "A imagem da capa deve ser JPEG/JPG/PNG/BMP/GIF!", true);
        }
    }

    protected void grvModalPrincipalObjectDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (ViewState["cod_modelo_edicao_moe"] != null)
        {
            e.InputParameters["cod_modelo_edicao_moe"] = ViewState["cod_modelo_edicao_moe"];
        }
    }

    protected void ArquivoPaginaInicial_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (ArquivoModeloEdicao.FileName.EndsWith(".pdf"))
        {
            Session["PaginaInicial"] = ArquivoModeloEdicao.FileBytes;
        }
        else
        {
            Msg.MessageBox(Page, "O arquivo modelo anexo deve ser PDF!", true);
        }
    }

    protected void TipoCampoRadioButtonList_TextChanged(object sender, EventArgs e)
    {
        if (TipoCampoRadioButtonList.SelectedValue == "1")
        {
            ImagemPlaceHolder.Visible = false;
            TextoPlaceholder.Visible = true;
        }
        else
        {
            TextoPlaceholder.Visible = false;
            ImagemPlaceHolder.Visible = true;
        }
    }
}