//using ImageMagick;
using diof_api;
using PdfSharp.Pdf.IO;


using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using WebSupergoo.ABCpdf7;

public partial class Admin_TEMPSEPRevisor : System.Web.UI.Page
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
    private String ftpHost = Environment.GetEnvironmentVariable("SIOF_FTP_TEMPSEP_HOST") ?? "storage.io.org.br/documentos/";

    /// <summary>
    /// Privilégios de login ao repositório FTP.
    /// </summary>
    private String ftpUid = Environment.GetEnvironmentVariable("SIOF_FTP_UID") ?? "documentos", ftpPwd = Environment.GetEnvironmentVariable("SIOF_FTP_PASSWORD");
#endif

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Remove("FileBytes");
            Session.Remove("FileName");
        }

        base.OnLoad(e);
    }

    protected void ArquivoFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (ArquivoFileUpload.PostedFile.ContentType == "application/pdf")
        {
            Session.Add("FileBytes", ArquivoFileUpload.FileBytes);
            Session.Add("FileName", ArquivoFileUpload.FileName);
        }
        else
        {
            throw new Exception("O arquivo deve ser PDF!");
        }
    }

    protected void ButtonSelecionar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["FileBytes"] != null && Session["FileName"] != null)
            {
                string cod_cliente = Session["FileName"].ToString();
                string num_edicao = Session["FileName"].ToString();

                num_edicao = num_edicao.Remove(0, 10);
                num_edicao = num_edicao.Replace(".pdf", "");
                num_edicao = num_edicao.Remove((num_edicao.Length - 6));

                cod_cliente = cod_cliente.Replace(".pdf", "");
                cod_cliente = cod_cliente.Remove((cod_cliente.Length - 1));
                cod_cliente = cod_cliente.Remove(0, cod_cliente.Length - 5);

                var pages = new Assinador2.PDFHandler().getPageCount((byte[])Session["FileBytes"]);

                System.Data.DataView vw = new DsAdmin.Cliente().Consultar(Convert.ToInt32(cod_cliente));
                FilePreview.Text = "<a class='button_style icon_viewDoc' href='DownloadHandler.ashx?vis=FileBytes'>Visualizar</a>";
                Dados_Cliente.Text = "<br /><ul><li><strong>Cliente: </strong>" + vw.Table.Rows[0]["des_cidade"] + "</li><li><strong>Tipo: </strong>" + vw.Table.Rows[0]["des_tipocliente"] + "</li><li><strong>Número da Edição: </strong>" + num_edicao + "</li><li><strong>Quantidade de páginas: </strong>" + pages + "</li></ul>";
                tip_carimbo_tempoCheckBox.Checked = vw.Table.Rows[0]["tip_carimbo_tempo"].ToString() == "S" ? true : false;
                tip_assinatura_digitalCheckBox.Checked = vw.Table.Rows[0]["tip_assinatura_digital"].ToString() == "S" ? true : false;
                tip_amostra_gratis_docCheckBox.Checked = vw.Table.Rows[0]["tip_amostra_gratis_doc"].ToString() == "S" ? true : false;
                UpdatePanel1.Update();
            }
            else
            {
                Msg.MessageBox(Page, "Anexe um arquivo de edição antes de prosseguir.", true);
            }
        }
        catch
        {
            Msg.MessageBox(Page, "O arquivo selecionado é inválido!", true);
            CancelarLinkButton_Click(null, null);
        }
    }

    protected void ButtonVisualizar_Click(object sender, EventArgs e)
    {
        Response.Redirect("DownloadHandler.ashx?vis=FileBytes");
    }

    protected void PublicarLinkButton_Click(object sender, EventArgs e)
    {
        if (Session["FileBytes"] != null && Session["FileName"] != null)
        {
            try
            {
                byte[] signed = null;
                string des_arquivoA4 = string.Empty;

                using (DsAdmin.Factory factory = new DsAdmin.Factory())
                {
                    Int32 retorno = 0;
                    String ano_edicao = "";
                    var fileBytes = RemoveProperties(Session["FileBytes"] as byte[]);

                    byte[] carimbo_tempo = null;

                    if (tip_amostra_gratis_docCheckBox.Checked)
                    {
                        var document = new Doc();

                        document.Read(fileBytes);

                        var image = new XImage();

                        image.SetFile(Server.MapPath("../App_Data/edicao-de-teste-watermark.png"));

                        var image_id = document.AddImageObject(image, true);

                        for (var i = 1; i <= document.PageCount; i++)
                        {
                            document.PageNumber = i;
                            document.Layer = document.LayerCount + 1;

                            document.AddImageCopy(image_id);
                        }

                        var stream = new MemoryStream();

                        document.Save(stream);

                        fileBytes = stream.ToArray();

                        document.Clear();
                    }

                    string des_texto_pdf = null;

                    if (tip_texto_extraidoCheckBox.Checked)
                    {
                        /*var path = Path.Combine("W:\\", DateTime.Now.Ticks.ToString());

                        try
                        {
                            Directory.CreateDirectory(path);

                            ExtractImages(path, fileBytes);

                            des_texto_pdf = DoOCR(path);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            Directory.Delete(path, true);
                        }*/
                    }

                    System.Xml.XmlDocument xml = new System.Xml.XmlDocument();

                    Assinador2.Signer signer = new Assinador2.Signer();
                    Assinador2.Stamper stamper = new Assinador2.Stamper();

                    if (tip_assinatura_digitalCheckBox.Checked)
                    {
                        fileBytes = signer.AssinarDocumento(fileBytes);
                    }

                    //Dados para inclusão
                    int page_count = new Assinador2.PDFHandler().getPageCount(fileBytes);
                    string fileName = Session["FileName"].ToString();

                    string num_edicao = Session["FileName"].ToString();
                    num_edicao = num_edicao.Remove(0, 10);
                    num_edicao = num_edicao.Replace(".pdf", "");
                    num_edicao = num_edicao.Remove((num_edicao.Length - 6));

                    string cod_cliente = Session["FileName"].ToString();
                    cod_cliente = cod_cliente.Replace(".pdf", "");
                    cod_cliente = cod_cliente.Remove((cod_cliente.Length - 1));
                    cod_cliente = cod_cliente.Remove(0, cod_cliente.Length - 5);

                    DateTime data_publicacao = Convert.ToDateTime(fileName.Substring(0, 10).Replace("_", "/"));

                    string pathFile = Convert.ToInt32(cod_cliente) + "/" + data_publicacao.ToString("yyyy/MM/") + "edicoes/";

                    des_arquivoA4 = pathFile + Session["FileName"].ToString();

                    var des_motivo_mot_splitted = des_motivo_motTextBox.Text.Split('\n');
                    var des_motivo_mot = "";

                    foreach (var motivo in des_motivo_mot_splitted)
                    {
                        if (!string.IsNullOrEmpty(motivo))
                            des_motivo_mot += "-" + motivo + "<br />";
                    }

                    factory.Publicacao().Incluir(page_count, Convert.ToInt32(cod_cliente), data_publicacao, 1, ref ano_edicao, des_motivo_mot,
                         2, "des_arquivofonte", des_arquivoA4, "des_arquivoa3", Session["des_nome_usu"].ToString(), "des_responsavel",
                         Request.UserHostAddress, Request.UserHostName, 1, new System.Xml.XmlDocument(), ref retorno, tip_amostra_gratis_docCheckBox.Checked, des_texto_pdf);

                    if (retorno != Convert.ToInt32(num_edicao)) { throw new Exception("Arquivo selecionado é inválido!"); }

                    string hash_signed_file = null;

                    if (tip_carimbo_tempoCheckBox.Checked)
                    {
                        //TimeStamp(fileBytes, out carimbo_tempo, out xml, stamper, out hash_signed_file);

                        //fileBytes = AddLtv(fileBytes);
                    }
                    ;

                    signed = fileBytes;

                    string des_certificacao = Assinador2.PDFHandler.pegaHash(data_publicacao.Day.ToString(), data_publicacao.ToString("MMMM"),
                        data_publicacao.Year.ToString(), Convert.ToInt32(cod_cliente), retorno.ToString());
                    factory.Publicacao().AlterarDocumento(xml, carimbo_tempo, hash_signed_file, des_certificacao, retorno, Convert.ToInt32(cod_cliente));

                    DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();

                    ftp.CreateDirectoryInFTP("ftp://" + ftpHost + pathFile, ftpUid, ftpPwd);
                    ftp.UploadFileToFTP(fileBytes, ftpHost, pathFile, Session["FileName"].ToString(), ftpUid, ftpPwd);

                    factory.Commit();

                    Msg.MessageBox(Page, "Edição publicada com sucesso!", true);
                }

                var bucket = "bucket-diof";
                var @namespace = "grkuksxbdsvb";
                var object_name = "documentos/" + des_arquivoA4;

                var storage = new ObjectStorage();

                storage.PutObject(bucket, @namespace, object_name, signed);

                if (LimparAoPublicarCheckBox.Checked)
                {
                    CancelarLinkButton_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                des_motivo_motTextBox.Text = string.Format("{0} \r\n {1}", ex.Message, ex.StackTrace);
                if (ex.InnerException != null)
                    des_motivo_motTextBox.Text += string.Format("\r\n {0} \r\n {1}", ex.InnerException.Message, ex.InnerException.StackTrace);

                Msg.MessageBox(Page, ex.Message, true);
            }
        }
        else
        {
            Msg.MessageBox(Page, "Anexe um arquivo de edição e clique em \"Selecionar\".", true);
        }
    }

    private string DoOCR(string path)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = string.Format("/c c:\\tesseract\\tesseract.bat \"{0}\"", path)
            }
        };

        process.Start();

        var result = process.StandardOutput.ReadToEnd();

        result = Regex.Replace(result, @"\r\n[ ]{1,}\r\n", "");
        result = Regex.Replace(result, @"(\r\n){3,}", "");

        result += process.StandardError.ReadToEnd();

        return result;
    }

    //public string ExtractImages(string path, byte[] @in)
    //{
    //    var settings = new MagickReadSettings
    //    {
    //        Density = new Density(300, 300)
    //    };

    //    using (var images = new MagickImageCollection())
    //    {
    //        images.Read(@in, settings);

    //        int page = 1;
    //        foreach (var image in images)
    //        {
    //            image.Grayscale();

    //            image.Write(Path.Combine(path, string.Format("{0:0000000}", page++) + ".png"));
    //        }
    //    }

    //    return path;
    //}

    public byte[] AddLtv(byte[] doc)
    {
        IOcspClient ocsp = new OcspClientBouncyCastle();
        ICrlClient crl = new CrlClientOnline();
        ITSAClient tsa = new TSAClientBouncyCastle("https://act.bry.com.br", Environment.GetEnvironmentVariable("SIOF_TSA_BRY_UID"), Environment.GetEnvironmentVariable("SIOF_TSA_BRY_PASSWORD"));

        using (PdfReader reader = new PdfReader(doc))
        {
            using (var stream = new MemoryStream())
            {
                using (PdfStamper stamper = PdfStamper.CreateSignature(reader, stream, '\0', null, true))
                {
                    LtvVerification verification = stamper.LtvVerification;

                    AcroFields fields = stamper.AcroFields;

                    List<string> names = fields.GetSignatureNames();

                    string sigName = names[names.Count - 1];

                    PdfPKCS7 pkcs7 = fields.VerifySignature(sigName);

                    if (pkcs7.IsTsp)
                    {
                        verification.AddVerification(sigName, ocsp, crl,
                            LtvVerification.CertificateOption.SIGNING_CERTIFICATE,
                            LtvVerification.Level.OCSP_CRL,
                            LtvVerification.CertificateInclusion.NO);
                    }
                    else
                    {
                        foreach (string name in names)
                        {
                            verification.AddVerification(name, ocsp, crl,
                                LtvVerification.CertificateOption.WHOLE_CHAIN,
                                LtvVerification.Level.OCSP_CRL,
                                LtvVerification.CertificateInclusion.NO);
                        }
                    }

                    PdfSignatureAppearance sap = stamper.SignatureAppearance;

                    int cod_carimbo_registro_car = 0;
                    DateTime? dat_carimbo_car = null;

                    new DsAdmin.CarimboRegistro().Incluir(Convert.ToInt32(Session["usu_codigo"]), ref dat_carimbo_car, 1, "N", ref cod_carimbo_registro_car);

                    LtvTimestamp.Timestamp(sap, tsa, null);

                    new DsAdmin.CarimboRegistro().Alterar("S", cod_carimbo_registro_car);

                    return stream.ToArray();
                }
            }
        }
    }

    public byte[] RemoveProperties(byte[] doc)
    {
        using (var reader = new PdfReader(doc))
        {
            using (var stream = new MemoryStream())
            {
                using (var stamper = new PdfStamper(reader, stream))
                {
                    var info = reader.Info;

                    info["Title"] = "";
                    info["CreationDate"] = "";
                    info["Producer"] = "";
                    info["ModDate"] = "";
                    info["Author"] = "";
                    info["Creator"] = "";

                    stamper.MoreInfo = info;
                }

                return stream.ToArray();
            }
        }
    }

    public byte[] RemoveProperties(byte[] doc)
    {
        using (var input = new MemoryStream(doc))
        {
            // Modify mode is required to change metadata
            var pdf = PdfReader.Open(input, PdfDocumentOpenMode.Modify);

            // Clear document info fields
            pdf.Info.Title = string.Empty;
            /*pdf.Info.Author = string.Empty;
            pdf.Info.Subject = string.Empty;
            pdf.Info.Keywords = string.Empty;
            pdf.Info.Creator = string.Empty;*/

            pdf.Info.CreationDate = System.DateTime.MinValue;
            pdf.Info.ModificationDate = System.DateTime.MinValue;

            using (var output = new MemoryStream())
            {
                pdf.Save(output, false);
                return output.ToArray();
            }
        }
    }

    private void TimeStamp(byte[] fileBytes, out byte[] carimbo_tempo, out System.Xml.XmlDocument xml, Assinador2.Stamper stamper, out string hash_signed_file)
    {
        hash_signed_file = stamper.GetBase64SHA1HashFromFile(fileBytes);

        //Assinador2.CarimboTempo carimbo = new Assinador2.CarimboTempo(hash_signed_file);

        Int32 cod_carimbo_registro_car = 0;
        DateTime? dat_carimbo_car = null;

        //Registra na tabela o uso do recurso de carimbo.
        new DsAdmin.CarimboRegistro().Incluir(Convert.ToInt32(Session["usu_codigo"]), ref dat_carimbo_car, 1, "N", ref cod_carimbo_registro_car);
        carimbo_tempo = Assinador2.CarimboTempo.Carimbar(fileBytes); //carimbo.Carimbar();
        new DsAdmin.CarimboRegistro().Alterar("S", cod_carimbo_registro_car);

        string hash_tst = stamper.GetBase64SHA1HashFromFile(carimbo_tempo);

        xml = stamper.GerarXMLCarimboTempo(hash_tst);

        xml.InnerXml = xml.InnerXml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
    }

    protected void CancelarLinkButton_Click(object sender, EventArgs e)
    {
        Dados_Cliente.Text = "";
        FilePreview.Text = "";
        des_motivo_motTextBox.Text = "";
        //LimparAoPublicarCheckBox.Checked = false;
        tip_carimbo_tempoCheckBox.Checked = false;
        tip_amostra_gratis_docCheckBox.Checked = false;
        //tip_assinatura_digitalRadioButtonList.SelectedIndex = -1;
        //tip_carimbo_tempoRadioButtonList.SelectedIndex = -1;
        Session.Remove("FileBytes");
        Session.Remove("FileName");
    }
}