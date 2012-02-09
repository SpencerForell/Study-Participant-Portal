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
        String[] sArray = db.getResults();
        int x = 5;
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
        
        Response.Redirect("CreateAccount.aspx?user=researcher"); 
    }


    protected void btnParCancel_Click(object sender, EventArgs e) {

        btnResearcher.Visible = true;
        btnParticipant.Visible = true;
        pnlParticipant.Visible = false;
    }

    protected void btnParCreateAcc_Click(object sender, EventArgs e) {
        Response.Redirect("CreateAccount.aspx?user=participant");
    }

    protected void btnParSubmit_Click(object sender, EventArgs e) {
        //validate login
        //go into participant page
        Response.Redirect("Participant.aspx");
    }

    protected void btnResSubmit_Click(object sender, EventArgs e) {

        //validate login
        //go into participant page
        Response.Redirect("Researcher.aspx");
    }
}
