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
        // local variables
        Study study = null;
        ListItem item = null;
        string queryString = "Select Study_ID, Name from Study where Res_ID = " + user_id + " order by Name";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        int resultNum = 0;

        // Clear any unwanted data out of the list box
        lboxStudyList.Items.Clear();

        // loop to populate researchers own studies
        while (query.Results.Count > resultNum) {
            study = new Study(Convert.ToInt32(query.Results[resultNum][0]));
            item = new ListItem(study.Name, study.StudyID.ToString());
            lboxStudyList.Items.Add(item);
            resultNum++;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        Researcher res = (Researcher)Session["User"];
        if (!IsPostBack) {
            populateListbox(res.UserID);
        }
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
    protected void btnResEdit_Click(object sender, EventArgs e) {
        Response.Redirect("CreateAccount.aspx?user=Researcher&edit=true");
    }
    protected void btnResView_Click(object sender, EventArgs e) {
        if (lboxStudyList.SelectedIndex < 0) {
            lblStatus.Text = "Please select a Study to view";
        }
        else {
            int study_id = Convert.ToInt32(lboxStudyList.SelectedValue);
            Response.Redirect("StudyForm.aspx?study_id=" + study_id);
        }
    }
}
