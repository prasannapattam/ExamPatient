using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Exam
{
    public class ExamPanel : Panel
    {

        private string _HeaderText;

        public List<string> ListPanels { get; set; }


        public string HeaderText
        {
            get { return _HeaderText; }
            set { _HeaderText = value; }
        }

        public string tcmspnlCss { get; set; }

        public string bulletImagesrc { get; set; }

        public bool PanelCollapse { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            if (tcmspnlCss == null)
            {
                tcmspnlCss = "exampanel";
            }
            if (bulletImagesrc == null)
            {
                bulletImagesrc = "Images/bullet.gif";
            }
            writer.Write(@"<div id=""" + this.ClientID + @"_Header"" class=" + tcmspnlCss + @" onclick=""$('#" + this.ClientID + @"').slideToggle('fast');"" ><div style='display:table-cell;vertical-align:middle'><img src=" + bulletImagesrc + @" width=""14"" height=""26"" />");
            writer.WriteLine("</div><div class='tcmspnlHeader'>");
            if (!string.IsNullOrEmpty(HeaderText))
            { HeaderText = HeaderText.ToUpper(); }
            writer.Write(HeaderText + "</div>");
            writer.WriteLine(@"</div>");
            base.Render(writer);
        }

        protected override void CreateChildControls()
        {
            List<string> lst;
            lst = (List<string>)base.Context.Items["ExpandCollapse"];
            if (lst == null)
            {
                lst = new List<string>();
                base.Context.Items["ExpandCollapse"] = lst;
            }
            lst.Add(this.ClientID);

            if (PanelCollapse == true)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID + "PanelHide", "$('#" + this.ClientID + @"').slideUp('fast');", true);
            }

            base.CreateChildControls();
        }
    }

}
