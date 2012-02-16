using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {

    /* EVENT HANDLERS*/

    protected void Page_Load(object sender, EventArgs e) {
        DatabaseQuery db = new DatabaseQuery("Select * from Researcher", DatabaseQuery.Type.Select);
        List<List<string>> sArray = db.Results;
    }

    protected void btnResearcher_Click(object sender, EventArgs e) {
        btnResearcher.Visible = false;
        btnParticipant.Visible = false;
        pnlResearcher.Visible = true; 
    }

    protected void btnParticipant_Click(object sender, EventArgs e) {
        btnResearcher.Visible = false;
        btnParticipant.Visible = false;
        pnlParticipant.Visible = true;
    }

    protected void btnResCancel_Click(object sender, EventArgs e) {
        btnResearcher.Visible = true;
        btnParticipant.Visible = true;
        pnlResearcher.Visible = false;
    }

    protected void btnResCreateAcc_Click(object sender, EventArgs e) {
        
        Response.Redirect("CreateAccount.aspx?user=Researcher"); 
    }


    protected void btnParCancel_Click(object sender, EventArgs e) {

        btnResearcher.Visible = true;
        btnParticipant.Visible = true;
        pnlParticipant.Visible = false;
    }

    protected void btnParCreateAcc_Click(object sender, EventArgs e) {
        Response.Redirect("CreateAccount.aspx?user=Participant");
    }

    protected void btnParSubmit_Click(object sender, EventArgs e) {
        string queryString = "select * from Participant where user_name = '" + tbParUser.Text + "' and password = '" + tbParPassword.Text + "'";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        if (query.Results.Count == 0) {
            lblResSatus.Text = "Invalid login. Please try again.";
        }
        else if (query.Results.Count == 1) {
            string user_name = query.Results[0][0];
            string first_name = query.Results[0][1];
            string last_name = query.Results[0][2];
            string email = query.Results[0][3];
            Participant par = new Participant(user_name, first_name, last_name, email);
            Session["user"] = par;
            Response.Redirect("ParticipantForm.aspx");
        }
        else {
            throw new Exception("submitting user_name: " + tbParUser.Text + " returned incorrect number of rows: " + query.Results.Count);
        }
    }

    protected void btnResSubmit_Click(object sender, EventArgs e) {
        string queryString = "select Res_ID, user_name, first_name, last_name, email from Researcher where user_name = '" + tbResUser.Text + "' and password = '" + tbResPassword.Text + "'";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        if (query.Results.Count == 0) {
            lblResSatus.Text = "Invalid login. Please try again.";
        }
        else if (query.Results.Count == 1) {
            int user_id = Convert.ToInt32(query.Results[0][0]);
            string user_name = query.Results[0][1];
            string first_name = query.Results[0][2];
            string last_name = query.Results[0][3];
            string email = query.Results[0][4];
            Researcher res = new Researcher(user_id, user_name, first_name, last_name, email);
            Session["user"] = res;
            Response.Redirect("ResearcherForm.aspx");
        }
        else {
            throw new Exception("submitting user_name: " + tbResUser.Text + " returned incorrect number of rows: " + query.Results.Count);
        }
    }
}
