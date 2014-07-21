using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Popup"] == null || Request.QueryString["Popup"] == "")
            pnlMasterHeader.Visible = true;
        else
            pnlMasterHeader.Visible = false;
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Context.Items["ShowTitle"] != null)
        {
            lblTitle.Text = Page.Title;
        }
        if (Context.Items["HideMenu"] != null)
        {
            HideMenu = true;
        }
    }

    public bool HideMenu
    {
        set
        {
            if (value)
                pnlMenu.Visible = false;
        }

    }

    public bool HideMasterHeader
    {
        set
        {
            if (value)
                pnlMasterHeader.Visible = false;
        }

    }
}
