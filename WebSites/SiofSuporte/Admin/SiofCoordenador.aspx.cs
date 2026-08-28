using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SiofCoordenador : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        }
    }

    protected void FecharModalDetalhamentoLinkButton_Click(object sender, EventArgs e)
    {
        ViewState["contadorCheckboxList"] = 0;
        ViewState["cod_publicacao_pub"] = null;
    }
}