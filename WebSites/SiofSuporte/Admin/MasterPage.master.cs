using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Count == 0 || Session["des_nome_usu"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            des_nome_usuLabel.Text = Session["des_nome_usu"].ToString();
            des_perfil_perLabel.Text = Session["des_perfil_per"].ToString();
        }

    }

    protected void exit_system_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
}


