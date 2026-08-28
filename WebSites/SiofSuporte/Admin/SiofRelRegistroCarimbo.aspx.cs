using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class Admin_SiofRelRegistroCarimbo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GerarRelatorioLinkButton_Click(object sender, EventArgs e)
    {
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ObjectDataSource1.Select()));
        ReportViewer1.LocalReport.Refresh();
        //Response.Clear();
        //Response.ContentType = "application/x-msdownload";
        //Response.AddHeader("content-disposition", "attachment; filename=Relatorio.pdf");
        //Response.BinaryWrite(ReportViewer1.LocalReport.Render("PDF"));
        //Response.End();
    }

    protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (dat_inicioTextBox.Text != "" || dat_fimTextBox.Text != "")
        {
            e.InputParameters["data_inicio"] = Convert.ToDateTime(dat_inicioTextBox.Text);
            e.InputParameters["data_fim"] = Convert.ToDateTime(dat_fimTextBox.Text);
        }
    }
}