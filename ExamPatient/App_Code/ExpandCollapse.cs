using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace Exam
{
    public class ExpandCollapse : Control
    {
        LinkButton lnkDocket;
        HyperLink lnkExpand;
        HyperLink lnkCollapse;

        public bool IsDocketRequired { get; set; }
        public string docketClick { get; set; }
        public string ExpandCSS { get; set; }
        private bool _showExpandCollapse = true;

        public bool ShowExpandCollapse
        {
            get { return _showExpandCollapse; }
            set { _showExpandCollapse = value; }
        }
        
        public List<string> PanelList { get; set; }
        protected override void Render(HtmlTextWriter writer)
        {
            if (ShowExpandCollapse == true)
            {
                System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
                string strJson = jss.Serialize(base.Context.Items["ExpandCollapse"]);
                lnkExpand.Attributes.Add("onclick", "return expandcollapse('" + strJson + "',0)");
                lnkCollapse.Attributes.Add("onclick", "return expandcollapse('" + strJson + "',1)");
            } 
            base.Render(writer);
        }

        protected override void CreateChildControls()
        {           
            lnkExpand = new HyperLink();
            lnkExpand.ID = "lnkExpand" + this.ClientID;
            lnkExpand.Text = "Expand All" + "&nbsp; / &nbsp;";
            lnkExpand.NavigateUrl = "#";
            

            lnkCollapse = new HyperLink();
            lnkCollapse.ID = "lnkCollapse" + this.ClientID;
            lnkCollapse.Text = "Collapse All";
            lnkCollapse.NavigateUrl = "#";
            

            lnkDocket = new LinkButton();
            lnkDocket.ID = "lnkDocketSearch" + this.ClientID;
            lnkDocket.Text = "Change Docket" + "&nbsp; / &nbsp;";
            lnkDocket.OnClientClick = docketClick;
            lnkDocket.Visible = IsDocketRequired;
            if (string.IsNullOrEmpty(ExpandCSS))
            {
                ExpandCSS = "divExpand";
            }
            Controls.Add(new LiteralControl("<table style='float: right;'>  <tr>    <td class='"+ExpandCSS+"'>"));
            Controls.Add(lnkDocket);            
            Controls.Add(lnkExpand);
            Controls.Add(lnkCollapse);
            Controls.Add(new LiteralControl(" </td>    </tr>  </table>"));
            Controls.Add(new LiteralControl(" <br />"));
            Controls.Add(new LiteralControl("<div style='padding-top:5px;'>"));
            Controls.Add(new LiteralControl("</div>"));
        }
        protected override void OnPreRender(EventArgs e)
        {
            lnkExpand.Visible = ShowExpandCollapse;
            lnkCollapse.Visible = ShowExpandCollapse;
            if (!ShowExpandCollapse)
            {
                lnkDocket.Text = "Change Docket";
            }
        }
    }
}
