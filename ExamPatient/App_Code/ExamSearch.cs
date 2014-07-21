using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Exam
{
    public class ExamSearch : Control, INamingContainer
    {
        #region Variables

        public event EventHandler dataadd_Click;
        public event EventHandler datasearch_Click;
        public event EventHandler datasave_Click;
        public event EventHandler datadelete_Click;
        public event EventHandler dataok_Click;

        Button btnSearch;
        Button btnAdd;
        Button btnReset;
        Button btnSave;
        Button btnDelete;
        Button btnResultReset;
        Button btnOK;
        ExpandCollapse excolPanel;

        #region Additional Buttons

        public event EventHandler dataadSearch_Click;
        public event EventHandler dataadUpdate1_Click;
        public event EventHandler dataadUpdate2_Click;

        Button btnAdditionalSearch;
        Button btnAdditionalUpdateOne;
        Button btnAdditionalUpdateTwo;

        public bool ShowAdditionalSearch { get; set; }
        public bool ShowAdditionalUpdateOne { get; set; }
        public bool ShowAdditionalUpdateTwo { get; set; }
        public string AddSearchValidationGroup { get; set; }
        public string AddSearchText { get; set; }
        public string AddSearchClientClick { get; set; }
        public string AddUpdate1ValidationGroup { get; set; }
        public string AddUpdate1Text { get; set; }
        public string AddUpdate1ClientClick { get; set; }
        public string AddUpdate2ValidationGroup { get; set; }
        public string AddUpdate2Text { get; set; }
        public string AddUpdate2ClientClick { get; set; }


        #endregion

        #region Push Buttons Visibility

        private bool _showSearchBotton = true;
        public bool ShowSearchButton
        {
            get { return _showSearchBotton; }
            set { _showSearchBotton = value; }
        }

        private bool _showSearchAddButton = true;
        public bool ShowSearchAddButton
        {
            get { return _showSearchAddButton; }
            set { _showSearchAddButton = value; }
        }

        private bool _showSearchResetBotton = true;
        public bool ShowSearchResetButton
        {
            get { return _showSearchResetBotton; }
            set { _showSearchResetBotton = value; }
        }

        private bool _showUpdateSaveBotton = true;
        public bool ShowUpdateSaveButton
        {
            get { return _showUpdateSaveBotton; }
            set { _showUpdateSaveBotton = value; }
        }

        private bool _showUpdateDeleteBotton = true;
        public bool ShowUpdateDeleteButton
        {
            get { return _showUpdateDeleteBotton; }
            set { _showUpdateDeleteBotton = value; }
        }

        private bool _showUpdateOKBotton = true;
        public bool ShowUpdateOKButton
        {
            get { return _showUpdateOKBotton; }
            set { _showUpdateOKBotton = value; }
        }

        private bool _showUpdateResetBotton = true;
        public bool ShowUpdateResetButton
        {
            get { return _showUpdateResetBotton; }
            set { _showUpdateResetBotton = value; }
        }

        #endregion

        ExamPanel searchpanel = new ExamPanel();
        ExamPanel addPanel = new ExamPanel();
        Panel addupdate = new Panel();
        ExamPanel resultspanel = new ExamPanel();
        List<Control> lst = new List<Control>();

        private string _mode;
        string _headerCaption;

        public string labelText { get; set; }
        public string SearchValidationGroup { get; set; }
        public string SaveValidationGroup { get; set; }
        public string AddValidationGroup { get; set; }
        public string DeleteValidationGroup { get; set; }
        public string ResultPanelReset { get; set; }
        public string SearchPanelReset { get; set; }
        public string ResultsResetOnClientClick { get; set; }
        public string SearchResetOnClientClick { get; set; }

        private bool _showExpandCollapse = true;
        public bool ShowExpandCollapse
        {
            get { return _showExpandCollapse; }
            set { _showExpandCollapse = value; }
        }

        #endregion

        #region Methods

        private Panel GetPanel(Control panelControl)
        {
            Panel pnl = new Panel();
            pnl.ID = panelControl.ID + "_pnl";
            while (panelControl.Controls.Count > 0)
            {
                pnl.Controls.Add(panelControl.Controls[0]);
            }
            return pnl;
        }

        protected override void CreateChildControls()
        {
            addupdate.ID = this.ClientID + "AddUpdate";
            resultspanel.ID = this.ClientID + "resultspanel";
            searchpanel.ID = this.ClientID + "searchpanel";
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel)
                    lst.Add(ctrl);
            }
            this.Controls.Add(CollapsePanel());
            this.Controls.Add(SearchPanel());
            Controls.Add(new LiteralControl("<div style='padding-top:15px;'>"));
            Controls.Add(new LiteralControl("</div>"));
            Controls.Add(new LiteralControl("<div id='divResultsPanel'>"));
            this.Controls.Add(ResultsPanel());
            Controls.Add(new LiteralControl("</div>"));
            this.Controls.Add(UpdatePanel());
            Controls.Add(new LiteralControl("<div id='divAddPanel'>"));
            this.Controls.Add(AddPanel());
            Controls.Add(new LiteralControl("</div>"));
            if (!Page.IsPostBack)
                resultspanel.Visible = false;
            addupdate.Visible = true;
            addPanel.Visible = false;
            base.CreateChildControls();
        }

        private ExpandCollapse CollapsePanel()
        {
            excolPanel = new ExpandCollapse();
            excolPanel.ID = this.ClientID + "collapsepanel";
            return excolPanel;
        }

        private ExamPanel SearchPanel()
        {
            searchpanel.HeaderText = "SEARCH";
            searchpanel.Controls.Clear();
            searchpanel.Controls.Add(GetPanel(lst[0]));

            btnSearch = new Button();
            btnSearch.ID = this.ClientID + "btnSearch";
            btnSearch.Text = "SEARCH";
            btnSearch.CssClass = "btnStyle";
            btnSearch.ValidationGroup = SearchValidationGroup;
            btnSearch.Click += new EventHandler(search_Click);
            btnSearch.OnClientClick = SearchResetOnClientClick;
            searchpanel.Controls.Add(btnSearch);

            btnAdd = new Button();
            btnAdd.ID = this.ClientID + "btnAdd";
            btnAdd.Text = "ADD";
            btnAdd.CssClass = "btnStyle";
            btnAdd.ValidationGroup = AddValidationGroup;
            btnAdd.Click += new EventHandler(add_Click);
            btnAdd.OnClientClick = "return addClentClick()";
            searchpanel.Controls.Add(btnAdd);

            btnReset = new Button();
            btnReset.ID = this.ClientID + "btnReset";
            btnReset.Text = "RESET";
            btnReset.CssClass = "btntcmsSearch";
            //btnReset.OnClientClick = "return ResetSearchDiv('" + searchpanel.ID + "','divResultsPanel','divAddPanel') ";
            btnReset.OnClientClick = "return ResetSearch()";
            searchpanel.Controls.Add(btnReset);

            btnAdditionalSearch = new Button();
            btnAdditionalSearch.ID = this.ClientID + "btnAddSearch";
            btnAdditionalSearch.Text = AddSearchText;
            btnAdditionalSearch.CssClass = "btnStyle";
            btnAdditionalSearch.ValidationGroup = AddSearchValidationGroup;
            btnAdditionalSearch.Click += dataadSearch_Click;
            btnAdditionalSearch.OnClientClick = AddSearchClientClick;
            searchpanel.Controls.Add(btnAdditionalSearch);

            return searchpanel;
        }

        private ExamPanel ResultsPanel()
        {
            resultspanel.HeaderText = "RESULTS";
            resultspanel.Controls.Clear();
            resultspanel.Controls.Add(GetPanel(lst[1]));

            resultspanel.Controls.Add(addupdate);
            return resultspanel;
        }

        private ExamPanel AddPanel()
        {
            addPanel.ID = this.ClientID + "addPanel";
            addPanel.HeaderText = "ADD " + labelText;
            addPanel.Controls.Clear();
            addPanel.Controls.Add(addupdate);
            return addPanel;
        }

        private Panel UpdatePanel()
        {
            addupdate.Controls.Clear();
            addupdate.Controls.Add(GetPanel(lst[2]));

            btnOK = new Button();
            btnOK.ID = this.ClientID + "btnOK";
            btnOK.Text = "OK";
            btnOK.CssClass = "btnStyle";
            btnOK.Click += new EventHandler(ok_Click);
            addupdate.Controls.Add(btnOK);

            btnSave = new Button();
            btnSave.ID = this.ClientID + "btnSave";
            btnSave.Text = "SAVE";
            btnSave.CssClass = "btnStyle";
            btnSave.ValidationGroup = SaveValidationGroup;
            btnSave.Click += new EventHandler(save_Click);
            btnSave.OnClientClick = ResultsResetOnClientClick;
            addupdate.Controls.Add(btnSave);

            btnDelete = new Button();
            btnDelete.ID = this.ClientID + "btnDelete";
            btnDelete.Text = "DELETE";
            btnDelete.CssClass = "btnStyle";
            btnDelete.ValidationGroup = DeleteValidationGroup;
            btnDelete.Click += new EventHandler(delete_Click);
            btnDelete.OnClientClick = "return ConfirmDelete()";
            addupdate.Controls.Add(btnDelete);

            btnResultReset = new Button();
            btnResultReset.ID = this.ClientID + "btnResultReset";
            btnResultReset.Text = "RESET";
            btnResultReset.CssClass = "btntcmsSearch";
            if (ResultPanelReset != "")
            {
                btnResultReset.OnClientClick = ResultPanelReset;
            }
            addupdate.Controls.Add(btnResultReset);

            btnAdditionalUpdateOne = new Button();
            btnAdditionalUpdateOne.ID = this.ClientID + "btnAddUpdate1";
            btnAdditionalUpdateOne.Text = AddUpdate1Text;
            btnAdditionalUpdateOne.CssClass = "btnStyle";
            btnAdditionalUpdateOne.ValidationGroup = AddUpdate1ValidationGroup;
            btnAdditionalUpdateOne.Click += dataadUpdate1_Click;
            btnAdditionalUpdateOne.OnClientClick = AddUpdate1ClientClick;
            addupdate.Controls.Add(btnAdditionalUpdateOne);

            btnAdditionalUpdateTwo = new Button();
            btnAdditionalUpdateTwo.ID = this.ClientID + "btnAddUpdate2";
            btnAdditionalUpdateTwo.Text = AddUpdate2Text;
            btnAdditionalUpdateTwo.CssClass = "btnStyle";
            btnAdditionalUpdateTwo.ValidationGroup = AddUpdate2ValidationGroup;
            btnAdditionalUpdateTwo.Click += dataadUpdate2_Click;
            btnAdditionalUpdateTwo.OnClientClick = AddUpdate2ClientClick;
            addupdate.Controls.Add(btnAdditionalUpdateTwo);


            return addupdate;
        }

        public void DisableAddPanel()
        {
            addPanel.Visible = false;
            resultspanel.Visible = true;
            addupdate.Visible = true;
            resultspanel.Controls.Add(addupdate);
        }

        protected override void OnPreRender(EventArgs e)
        {
            excolPanel.Visible = ShowExpandCollapse;
            btnAdditionalSearch.Visible = ShowAdditionalSearch;
            btnAdditionalUpdateOne.Visible = ShowAdditionalUpdateOne;
            btnAdditionalUpdateTwo.Visible = ShowAdditionalUpdateTwo;
            btnSearch.Visible = ShowSearchButton;
            btnAdd.Visible = ShowSearchAddButton;
            btnReset.Visible = ShowSearchResetButton;
            btnSave.Visible = ShowUpdateSaveButton;
            btnDelete.Visible = ShowUpdateDeleteButton;
            btnResultReset.Visible = ShowUpdateResetButton;
            btnOK.Visible = ShowUpdateOKButton;
        }

        #endregion

        #region Properities

        public string Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }
        }

        public string SetHeaderCaption
        {
            get
            {
                return _headerCaption;
            }

            set
            {
                _headerCaption = value;
            }
        }

        #endregion

        #region Events

        void search_Click(object sender, EventArgs e)
        {
            EnsureChildControls();
            addPanel.Visible = false;
            resultspanel.Visible = true;
            addupdate.Visible = true;
            resultspanel.Controls.Add(addupdate);
            btnDelete.Visible = true;
            datasearch_Click.Invoke(sender, e);
        }

        void add_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            resultspanel.Visible = false;
            btnDelete.Visible = false;
            dataadd_Click.Invoke(sender, e);
        }

        void delete_Click(object sender, EventArgs e)
        {
            DisableAddPanel();
            datadelete_Click.Invoke(sender, e);
        }

        void save_Click(object sender, EventArgs e)
        {
            datasave_Click.Invoke(sender, e);

            if (!String.IsNullOrEmpty(Mode))
            {
                if (Mode.ToUpper() == "UPDATE")
                {
                    addPanel.Visible = false;
                    resultspanel.Visible = true;
                    addupdate.Visible = true;
                    resultspanel.Controls.Add(addupdate);
                }
                else if (Mode.ToUpper() == "ADD")
                {
                    addPanel.Visible = true;
                    resultspanel.Visible = false;
                    btnDelete.Visible = false;
                }
                else if (Mode.ToUpper() == "ALREADYUPDATED")
                {
                    addPanel.Visible = false;
                    resultspanel.Visible = true;
                    resultspanel.Controls.Add(addupdate);
                    addupdate.Visible = true;
                    btnDelete.Visible = true;
                }
            }
            else
            {
                addPanel.HeaderText = SetHeaderCaption;
                searchpanel.Visible = false;
                addPanel.Visible = false;
                resultspanel.Visible = false;
                ((ExamPanel)addupdate.Parent).Visible = true;
            }
        }

        void ok_Click(object sender, EventArgs e)
        {
            dataok_Click.Invoke(sender, e);
        }

        #endregion
    }
}