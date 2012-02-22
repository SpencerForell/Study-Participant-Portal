using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParticipantForm: System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        Participant part = (Participant)Session["User"];
        if (!IsPostBack) {
            populateListbox(part.User_id);
        }
    }

    private void populateListbox(int user_id) {
        lboxStudyList.Items.Clear();
        string queryString = "Select Study_ID, Name from Study order by Name";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        int resultNum = 0;
        while (query.Results.Count > resultNum) {
            Study study = new Study(Convert.ToInt32(query.Results[resultNum][0]));
            ListItem item = new ListItem(study.StudyName, study.Study_ID.ToString());
            lboxStudyList.Items.Add(item);
            resultNum++;
        }
    }
}