using System;
using System.IO;
using System.Web.UI.WebControls;

public partial class Admin_SiofEditoracao : System.Web.UI.Page
{
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

    protected void GerarLinkButton_Click(object sender, EventArgs e)
    {
        if (Session["FileBytes"] == null)
        {
            Msg.MessageBox(Page, "Não é possivel criar uma edição vazia!", true);
            return;
        }

        if (ViewState["COD_CLIENTE"] == null)
        {
            Msg.MessageBox(Page, "Selecione um cliente!", true);
            return;
        }

        var cliente = Convert.ToInt32(ViewState["COD_CLIENTE"]);

        var modelo_edicao_cliente = new DsAdmin.ModeloEdicao().ConsultarPorCliente(cliente);

        var dataset = new System.Data.DataSet();
        var tb = new DsAdmin.Cliente().Consultar(cliente).Table.Copy();
        tb.TableName = "tb01";
        dataset.Tables.Add(tb);

        var modelo_capa = File.ReadAllBytes(Server.MapPath("~/App_Data/modelo.pdf"));

        var modelo = new Assinador2.IMAPModeloEdicao(modelo_capa, modelo_edicao_cliente);

        var handler = new Assinador2.PDFHandler();

        var bytes = (byte[])Session["FileBytes"];

        var edicao = handler.GerarEdicao(modelo, bytes, dataset, cliente);

        var filename = string.Format("{0}{1}{2}1.pdf", handler.Data.ToString("yyyy_MM_dd"), handler.DocCodigo, cliente);

        var content_disposition = string.Format("attachment; filename={0}", filename);

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", edicao.Length.ToString());
        Response.AddHeader("content-disposition", content_disposition);

        Response.BinaryWrite(edicao);

        CancelarLinkButton_Click(null, null);
    }

    protected void CancelarLinkButton_Click(object sender, EventArgs e)
    {
        Session.Remove("FileBytes");
        Session.Remove("FileName");
        DES_TIPOCLIENTEDropDownList.SelectedIndex = -1;
        DES_UFDropDownList.SelectedIndex = -1;
        DES_CIDADETextBox.Text = "";
        
        grvPrincipal.DataBind();
        UpdatePanel1.Update();
    }

    protected void grvPrincipal_RowCommand(Object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnSelecionar")
        {
            ViewState["COD_CLIENTE"] = e.CommandArgument;
            ((GridViewRow)((ImageButton)e.CommandSource).Parent.Parent).BackColor = System.Drawing.Color.GreenYellow;
        }
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
}