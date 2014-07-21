using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class AutoCorrect : System.Web.UI.Page
{
    private string doctorUserName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        btnEditAC.Visible = true;
        btnDeleteAC.Visible = true;
        result.Visible = false;
        resultError.Visible = false;

        if (!IsPostBack)
        {
            string cmdText = "SELECT FirstName + ' ' + LastName as DoctorName, UserName FROM [User] ORDER BY DoctorName";

            SqlDataReader drUser = DBUtil.ExecuteReader(cmdText);

            WebUtil.BindLookupDropDown(ddlFilter, drUser, "DoctorName", "UserName");
            ddlFilter.Items.RemoveAt(0);

            doctorUserName = ddlFilter.SelectedValue;
            BindGrid();
        }

        doctorUserName = ddlFilter.SelectedValue;
    }

    protected void BindGrid()
    {
        AutoCorrectSource.SelectParameters["UserName"].DefaultValue = doctorUserName;
        AutoCorrectResults.DataBind();

        if (AutoCorrectResults.Rows.Count == 0)
        {
            btnEditAC.Visible = false;
            btnDeleteAC.Visible = false;
        }
    }
    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindGrid();
        pnlGrid.Visible = true;
        pnlEdit.Visible = false;
        pnlCopy.Visible = false;
        lbCopyError.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = false;
        pnlEdit.Visible = true;
        tbName.ReadOnly = false;
        tbName.Text = "";
        tbValue.Text = "";
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = false;
        pnlCopy.Visible = true;
        string cmdText = "SELECT FirstName + ' ' + LastName as DoctorName, UserName FROM [User] ORDER BY DoctorName";

        SqlDataReader drUser = DBUtil.ExecuteReader(cmdText);

        WebUtil.BindLookupDropDown(ddlDoctors, drUser, "DoctorName", "UserName");
        ddlDoctors.Items.RemoveAt(0);
    }

    protected void btnCopySave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFilter.SelectedValue == ddlDoctors.SelectedValue)
            {
                throw new ApplicationException(@"Copy source and destination cannot be same");
            }

            string cmdText = "DELETE FROM AutoCorrect WHERE [UserName] = '" + doctorUserName + "'";
            DBUtil.Execute(cmdText);

            cmdText = "INSERT INTO AutoCorrect([Name], [Value], [UserName]) SELECT [Name], [Value], '" + doctorUserName + "' FROM AutoCorrect WHERE  [UserName] = '" + ddlDoctors.SelectedValue + "'";
            DBUtil.Execute(cmdText);
            result.Text = @"Successfully copied Autocorrect values from """ + ddlDoctors.SelectedValue + @"""";
            result.Visible = true;
            pnlCopy.Visible = false;
            pnlGrid.Visible = true;
            BindGrid();
        }
        catch (Exception exp)
        {
            lbCopyError.Text = exp.Message;
            lbCopyError.Visible = true;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        pnlGrid.Visible = false;
        pnlEdit.Visible = true;
        tbName.ReadOnly = true;

        //getting the Value
        string cmdText = "SELECT [Value] FROM AutoCorrect WHERE [Name] = '" + patientID.Value + "' AND [UserName] = '" + doctorUserName + "'";
        object value = DBUtil.ExecuteScalar(cmdText);

        tbName.Text = patientID.Value;
        tbValue.Text = value.ToString();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string cmdText = "DELETE FROM AutoCorrect WHERE [Name] = '" + patientID.Value + "' AND [UserName] = '" + doctorUserName + "'";
        DBUtil.Execute(cmdText);
        result.Text = @"""" + patientID.Value + @""" deleted successfully";
        BindGrid();
        result.Visible = true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string cmdText = "";

        try
        {
            if (tbName.ReadOnly)
                cmdText = "UPDATE AutoCorrect SET [Value] = '{1}' WHERE [Name] = '{0}' AND [UserName] = '{2}'";
            else
            {
                //checking whether the name already exists
                cmdText = "SELECT [Name] FROM AutoCorrect  WHERE [Name] = '" + tbName.Text + "' AND [UserName] = '" + doctorUserName + "'";
                object currentName = DBUtil.ExecuteScalar(cmdText);
                if (currentName != null)
                    throw new ApplicationException(@"Auto Correct is already defined for """ + tbName.Text + @"""");
                cmdText = "INSERT INTO AutoCorrect ([Name], [Value], [UserName]) VALUES ('{0}', '{1}', '{2}')";
            }

            cmdText = String.Format(cmdText, tbName.Text, tbValue.Text.Replace("'", "''"), doctorUserName);

            DBUtil.Execute(cmdText);
            result.Text = @"""" + tbName.Text + @""" saved successfully";
            result.Visible = true;
            pnlEdit.Visible = false;
            pnlGrid.Visible = true;
            BindGrid();
        }
        catch (Exception exp)
        {
            lbError.Text = exp.Message;
            lbError.Visible = true;
        }
    }

    private string firstRowClientID;
    protected void AutoCorrectResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string rowUserID = ((System.Data.DataRowView)e.Row.DataItem)["Name"].ToString();
            if (firstRowClientID == null)
            {
                firstRowClientID = e.Row.ClientID;
                patientID.Value = rowUserID;
            }
            e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "', '" + firstRowClientID + "', '" + rowUserID + "')");
        }

    }
}