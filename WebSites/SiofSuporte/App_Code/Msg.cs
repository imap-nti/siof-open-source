using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

/// <summary>
/// Summary description for Msg
/// </summary>
public class Msg
{
    public static void MessageBox(Page page, string Mensagem, bool AjaxCommand)
    {
        if (AjaxCommand)
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(page, page.GetType(), "", "window.alert('" + Mensagem + "');", true);
        else
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "window.alert('" + Mensagem + "');", true);
    }

    public static void RegisterScript(Page page, string script, bool AjaxCommand)
    {
        if (AjaxCommand)
            AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(page, page.GetType(), "", script, true);
        else
            page.ClientScript.RegisterStartupScript(page.GetType(), "", script, true);
    }
}