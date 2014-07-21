using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Exam
{
    public class Tab : Panel
    {
        private string _HeaderText;

        public string HeaderText
        {
            get { return _HeaderText; }
            set { _HeaderText = value; }
        }
    }
}
