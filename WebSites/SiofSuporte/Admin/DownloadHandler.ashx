<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// Servidor de onde são baixados os arquivos para uso na diagramação.
    /// </summary>
    private string ftpSep = Environment.GetEnvironmentVariable("SIOF_FTP_SEP_HOST") ?? "storage.io.org.br/documentosSep/";

    /// <summary>
    /// Host destino de onde são baixados os arquivos diagramados.
    /// </summary>
    private String ftpHost = Environment.GetEnvironmentVariable("SIOF_FTP_DIAGRAMADOR_HOST") ?? "ftp.sisged.com.br/documentosSiof/";

    /// <summary>
    /// Host de armazenamento utilizado para downloads diretos.
    /// </summary>
    private String ftpStorageHost = Environment.GetEnvironmentVariable("SIOF_STORAGE_HOST") ?? "storage.io.org.br/";

    /// <summary>
    /// Credênciais para autenticação no servidor.
    /// </summary>
    private String ftpUid = Environment.GetEnvironmentVariable("SIOF_FTP_UID") ?? "documentos", ftpPwd = Environment.GetEnvironmentVariable("SIOF_FTP_PASSWORD");

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request["c"] != null)
        {
            try
            {
                Int32 codigo = Convert.ToInt32(context.Request["c"]);
                System.Data.DataView vw = new DsAdmin.Arquivo().ConsultarArquivoPublicacao(codigo);
                string fullpath = "ftp://" + ftpSep + vw.Table.Rows[0]["cod_cliente"].ToString() + "/" + Convert.ToDateTime(vw.Table.Rows[0]["doc_dt_publicacao"]).ToString("yyyy/M/") + vw.Table.Rows[0]["and_caminho"].ToString();

                context.Response.ContentType = "application/x-msdownload";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(fullpath));
                context.Response.BinaryWrite(DownloadFileFromFTP(fullpath, ftpUid, ftpPwd));
                context.Response.End();
            }
            catch
            {
                //Mensagem de file not found
            }
        }
        else if (context.Request["b"] != null)
        {
            try
            {
                Int32 codigo = Convert.ToInt32(context.Request["b"]);
                System.Data.DataView vw = new DsAdmin.Publicacao().ConsultarPublicacao(codigo);
                string fullpath = "ftp://" + ftpHost + vw.Table.Rows[0]["cod_cliente"].ToString() + "/" + Convert.ToDateTime(vw.Table.Rows[0]["dat_diagramacao_dod"]).ToString("yyyy/M/") + vw.Table.Rows[0]["des_arquivo_url_dod"].ToString();

                context.Response.ContentType = "application/x-msdownload";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + vw.Table.Rows[0]["des_arquivo_url_dod"]);
                context.Response.BinaryWrite(DownloadFileFromFTP(fullpath, ftpUid, ftpPwd));
                context.Response.End();
            }
            catch
            {
                //Mensagem de file not found
            }
        }
        else if (context.Request["q"] != null)
        {
            try
            {
                string fullpath = "ftp://" + ftpSep + "modelo/" + context.Request["q"] + ".jpg";
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(DownloadFileFromFTP(fullpath, ftpUid, ftpPwd));
                context.Response.End();
            }
            catch
            {
                //Mensagem de file not found
            }
        }
        else if (context.Request["img"] != null)
        {
            string fullpath = context.Request["img"].ToString();
            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(DownloadFileFromFTP("ftp://" + ftpStorageHost + fullpath, ftpUid, ftpPwd));
            context.Response.End();
        }
        else if (context.Request["u"] != null)
        {
            context.Response.ContentType = "image/jpeg";
            try
            {
                context.Response.BinaryWrite((byte[])new DsAdmin.Cliente().ConsultarBrasao(Convert.ToInt32(context.Request["u"])).Table.Rows[0]["img_brasao_bra"]);
            }
            catch
            {
                try
                {
                    context.Response.BinaryWrite((byte[])context.Session["fIleBytes"]);
                }
                catch { }
            }
            context.Response.End();
        }
        else if (context.Request["l"] != null)
        {
            try
            {
                Int32 codigo = Convert.ToInt32(context.Request["l"]);
                System.Data.DataView vw = new DsAdmin.Editoracao().Consultar(codigo);
                string fullpath = "ftp://" + ftpStorageHost + vw.Table.Rows[0]["des_arquivo_edt"].ToString();

                context.Response.ContentType = "application/x-msdownload";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(fullpath));
                context.Response.BinaryWrite(DownloadFileFromFTP(fullpath, ftpUid, ftpPwd));
                context.Response.End();
            }
            catch
            {
                //Mensagem de file not found
            }
        }
        else if (context.Request["vis"] != null)
        {
            if ((byte[])context.Session["FileBytes"] != null)
            {
                context.Response.ContentType = "application/x-msdownload";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=preview.pdf");
                context.Response.BinaryWrite((byte[])context.Session["FileBytes"]);
                context.Response.End();
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public byte[] DownloadFileFromFTP(string path, string uid, string pwd)
    {
        try
        {
            DsAdmin.FTPManager ftp = new DsAdmin.FTPManager();
            return ftp.DownloadFileFromFTP(path, uid, pwd);
        }
        catch { return null; }
    }
}