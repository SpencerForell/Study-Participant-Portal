using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResearcherForm : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {
        lboxStudyList.Items.Clear();
        Researcher res = (Researcher)Session["User"];

        string queryString = "Select Study_ID from Study where Res_ID = " + res.User_id;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        int resultNum = 0;
        while(query.Results.Count > resultNum){
            Study study = new Study(Convert.ToInt32(query.Results[resultNum][0]));
            ListItem item = new ListItem(study.StudyName, study.Study_ID.ToString());
            lboxStudyList.Items.Add(item);
            resultNum++;
        }
    }

    protected void logout_Click(object sender, EventArgs e) {
        Session.Clear();
        Response.Redirect("Default.aspx");
    }

    protected void btnResCreate_Click(object sender, EventArgs e) {
        Response.Redirect("CreateStudy.aspx");
    }
    protected void btnResEditStdy_Click(object sender, EventArgs e) {
        int study_id = Convert.ToInt32(lboxStudyList.SelectedValue);
        Response.Redirect("CreateStudy.aspx?edit=true&study_id=" + study_id);
       
    }
}
