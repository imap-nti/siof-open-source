using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SEPStamp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hash_code.Value = Request["hash_code"];

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "form1.submit();", true);
    }
}