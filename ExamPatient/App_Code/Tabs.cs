using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace Exam
{
    public class Tabs : Panel
    {

        Image imgtopTab;
        Image imgbotTab;
        public Tabs()
            : base()
        {
            base.Controls.Add(new Literal());
            imgtopTab = new Image();
            imgtopTab.ID = this.ClientID + "_" + "imgtopTab";
            imgtopTab.SkinID = "imgTop";
            Controls.Add(new LiteralControl("<div style='padding-left:8px;'>"));
            Controls.Add(imgtopTab);
            //Controls.Add(new LiteralControl(@"<FIELDSET></FIELDSET>"));
            Controls.Add(new LiteralControl("<div class='tabBackground'>"));
            Controls.Add(new LiteralControl("<div style='padding-left:1px;'>"));
            HideTab = false; //by default show the tabs

        }

        protected override void CreateChildControls()
        {
            imgbotTab = new Image();
            imgbotTab.ID = this.ClientID + "_" + "imgbotTab";
            imgbotTab.SkinID = "imgBottom";


            this.ID = "tabs";
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(imgbotTab);
            Controls.Add(new LiteralControl("</div>"));
            base.CreateChildControls();
        }

        protected override void Render(HtmlTextWriter writer)
        {

            string HeaderText;
            //building the tabs ul
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<script type=""text/javascript"">$(function (){$tabs = $(""#tabs"").tabs();");
            //checking for selected tab
            if (SelectedTabIndex != 0)
                sb.Append(@"$tabs.tabs('select', " + SelectedTabIndex.ToString() + ");");

            //checking for enabled tab
            int iTab = 0;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Tab)
                {
                    Tab tab = (Tab)ctrl;
                    if (!tab.Enabled)
                    {
                        sb.Append(@"$tabs.tabs('disable', " + iTab.ToString() + ");");
                    }
                    iTab++;
                }
            }

            sb.Append(@"});</script>");

            sb.Append("<ul>");
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Tab)
                {
                    Tab tab = (Tab)ctrl;
                    HeaderText = tab.HeaderText;
                    if (HeaderText.Trim() == "")
                        HeaderText = @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    if (tab.Visible)
                        sb.Append(String.Format(@"<li><a href=""#{0}"">{1}</a></li>", tab.ID, HeaderText));
                }
            }
            sb.Append("</ul>");
            if(!HideTab)
                ((Literal)base.Controls[0]).Text = sb.ToString();
            base.Render(writer);
        }

        public int SelectedTabIndex { get; set; }

        public bool HideTab { get; set; }

    }
}
