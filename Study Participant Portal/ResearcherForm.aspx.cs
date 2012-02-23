using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResearcherForm : System.Web.UI.Page {

    /// <summary>
    /// Populate the listbox with all the studies, order by Name
    /// </summary>
    /// <param name="user_id"></param>
    private void populateListbox(int user_id) {
        lboxStudyList.Items.Clear();
        string queryString = "Select Study_ID from Study where Res_ID = " + user_id + " order by Name";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        int resultNum = 0;
        while (query.Results.Count > resultNum) {
            Study study = new Study(Convert.ToInt32(query.Results[resultNum][0]));
            ListItem item = new ListItem(study.StudyName, study.Study_ID.ToString());
            lboxStudyList.Items.Add(item);
            resultNum++;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        Researcher res = (Researcher)Session["User"];
        if (!IsPostBack) {
            populateListbox(res.User_id);
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
        if (lboxStudyList.SelectedIndex < 0) {
            lblStatus.Text = "Please select a Study to edit";
        }
        else {
            int study_id = Convert.ToInt32(lboxStudyList.SelectedValue);
            Response.Redirect("CreateStudy.aspx?edit=true&study_id=" + study_id);
        }
    }
}
