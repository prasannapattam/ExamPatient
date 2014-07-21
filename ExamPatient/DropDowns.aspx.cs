using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class DropDowns : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ButtonsPanel.Visible = true;
        pnlError.Visible = false;
        pnlResult.Visible = false;
        if (!IsPostBack)
        {
            //getting the unique Exam Look Up
            string cmdText = "SELECT DISTINCT FieldName, FieldDescription FROM ExamLookUp";
            SqlDataReader dr = DBUtil.ExecuteReader(cmdText);

            WebUtil.BindLookupDropDown(ControlList, dr, "FieldDescription", "FieldName");
            dr.Close();
            dr.Dispose();
            ButtonsPanel.Visible = false;
        }
    }

    protected void ControlList_SelectedIndexChanged(object sender, EventArgs e)
    { 
        if (ControlList.SelectedIndex == 0)
        {
            DDResults.Visible = false;
            ButtonsPanel.Visible = false;
        }
        else
        {
            //getting the list from Look Up table
            string cmdText = "SELECT LookUpID, FieldValue, FieldDescription, SortOrder FROM LookUp WHERE FieldName = '" + ControlList.SelectedValue + "' ORDER BY SortOrder";
            SqlDataReader dr = DBUtil.ExecuteReader(cmdText);
            DDResults.Visible = true;
            DDResults.DataSource = dr;
            DDResults.DataBind();

            ButtonsPanel.Visible = true;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //adding a blank row
        DataTable dt = GetGridData();
        DataRow dr = dt.NewRow();
        dr["LookUpID"] = -1;
        dt.Rows.Add(dr);

        DDResults.DataSource = dt;
        DDResults.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //adding a blank row
        try
        {
            DataTable dt = GetGridData();

            //updating database
            foreach (DataRow dr in dt.Rows)
            {
                string cmdText = "";
                string sortOrder = dr["SortOrder"] == DBNull.Value || dr["SortOrder"].ToString() == "" ? "NULL" : dr["SortOrder"].ToString();
                if (Convert.ToInt32(dr["LookUpID"]) == -1 && dr["FieldValue"].ToString() == "")
                {
                    continue;
                }
                else if (Convert.ToInt32(dr["LookUpID"]) == -1 && dr["FieldValue"].ToString() != "")
                {
                    cmdText = String.Format("INSERT INTO LookUp(FieldName, FieldValue, FieldDescription, SortOrder) VALUES('{0}', '{1}', '{2}', {3})", ControlList.SelectedValue, DBUtil.EscapeSingleQuote(dr["FieldValue"]), DBUtil.EscapeSingleQuote(dr["FieldDescription"]), sortOrder);
                }
                else if (dr["FieldValue"].ToString() != "")
                {
                    cmdText = String.Format("UPDATE LookUp Set FieldValue = '{0}', FieldDescription = '{1}', SortOrder = {2} WHERE LookUpID = {3}", DBUtil.EscapeSingleQuote(dr["FieldValue"]), DBUtil.EscapeSingleQuote(dr["FieldDescription"]), sortOrder, dr["LookUpID"]);
                }
                else
                {
                    cmdText = String.Format("DELETE FROM LookUp WHERE LookUpID = {0}", dr["LookUpID"]);
                }

                //Response.Write(cmdText + "<br>");

                DBUtil.Execute(cmdText);
            }

            ButtonsPanel.Visible = false;
            pnlResult.Visible = true;
        }
        catch (Exception exp)
        {
            pnlError.Visible = true;
            resultError.Text = exp.Message;
        }
    }

    private DataTable GetGridData()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        string sFieldValue, sFieldDescription;

        dt.Columns.Add("LookUpID", typeof(int));
        dt.Columns.Add("FieldValue");
        dt.Columns.Add("FieldDescription");
        dt.Columns.Add("SortOrder");

        foreach (GridViewRow gvr in DDResults.Rows)
        {
            if (gvr.RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();

                sFieldValue = ((TextBox)gvr.FindControl("FieldValue")).Text;
                sFieldDescription = ((TextBox)gvr.FindControl("FieldDescription")).Text;

                dr["LookUpID"] = ((HiddenField)gvr.FindControl("LookUpID")).Value;
                dr["FieldValue"] = sFieldValue;
                dr["FieldDescription"] = sFieldDescription;
                dr["SortOrder"] = ((TextBox)gvr.FindControl("SortOrder")).Text;

                if ((sFieldValue != "" && sFieldDescription == "") || (sFieldValue == "" && sFieldDescription != ""))
                    throw new ApplicationException("Both Field Value & Field Description are needed");

                dt.Rows.Add(dr);
            }
        }

        return dt;
    }
}