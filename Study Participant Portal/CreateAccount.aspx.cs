using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        string user = Request.QueryString["user"];
        if (user == SuperUser.Type.Researcher.ToString().ToLower()) {
            pnlResearcher.Visible = true;
        }
        else if (user == SuperUser.Type.Participant.ToString().ToLower()) {
            pnlParticipant.Visible = true;
        }

    }
    protected void btnResSubmit_Click(object sender, EventArgs e) {
        if (tbResPassword != tbResPasswordConfirm) {
            lblResStatus.Text = "Passwords did not much!";
        }
        else {
            //insert info into database
            lblResStatus.Text = "";
        }

    }
    protected void btnParSubmit_Click(object sender, EventArgs e) {
        //database connection stuff

    }
    protected void btnResCancel_Click(object sender, EventArgs e) {
        Response.Redirect("Default.aspx");
    }
    protected void btnParCancel_Click(object sender, EventArgs e) {
        Response.Redirect("Default.aspx");
    }
}