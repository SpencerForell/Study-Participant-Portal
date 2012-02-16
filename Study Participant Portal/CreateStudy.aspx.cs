using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateStudy : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack && Request.QueryString["edit"] == "true") {
            Study study = new Study(Convert.ToInt32(Request.QueryString["study_id"]));
            tbTitle.Text = study.StudyName;
            tbDescription.Text = study.StudyDescription;
        }
    }
    protected void BtnStdCancel_Click(object sender, EventArgs e) {
        Response.Redirect("ResearcherForm.aspx");
    }
    protected void BtnStdSubmit_Click(object sender, EventArgs e) {
        if (tbTitle.Text.Equals(string.Empty) || tbDescription.Text.Equals(string.Empty)) {
            lblError.Text = "Please fill out the necassary fields.";
        }

        bool isNewStudy = !Convert.ToBoolean(Request.QueryString["edit"]);
        Researcher res = (Researcher)Session["user"];
        StringBuilder queryString = null;
        DatabaseQuery query = null;
        switch (isNewStudy) {
            case true:
                queryString = new StringBuilder("insert into Study");
                queryString.Append(" (Name, Description, Creation_date, Expired, Res_ID)");
                queryString.Append(" values ");
                queryString.Append(" ('" + tbTitle.Text + "', '" + tbDescription.Text + "', ");
                queryString.Append("NOW()" + ", 0, " + res.User_id + ")");
                query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);
                break;
            case false:
                queryString = new StringBuilder("update Study");
                queryString.Append(" set Name = '" + tbTitle.Text + "'" );
                queryString.Append(", Description = '" + tbDescription.Text + "'");
                queryString.Append(" where Study_ID = " + Request.QueryString["study_id"]);
                query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Update);
                break;
        }
        

        string studyQuery = "select Study_ID from Study where Name = '" + tbTitle.Text+ "'";
        query = new DatabaseQuery(studyQuery, DatabaseQuery.Type.Select);
        int study_id = Convert.ToInt32(query.Results[0][0]);

        Response.Redirect("StudyForm.aspx?study_id=" + study_id);
    }
}