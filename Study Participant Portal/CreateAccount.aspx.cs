using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        string user = Request.QueryString["user"];
        if (user == SuperUser.UserType.Researcher.ToString().ToLower()) {
            pnlResearcher.Visible = true;
        }
        else if (user == SuperUser.UserType.Participant.ToString().ToLower()) {
            pnlParticipant.Visible = true;
        }

    }
    protected void btnResSubmit_Click(object sender, EventArgs e) {
        if (tbResPassword.Text != tbResPasswordConfirm.Text) {
            lblResStatus.Text = "Passwords did not match!";
        }
        else {
            string queryString = "insert into Researcher" +
                                 " (Res_ID, Name, Email, Password, Num_Ratings)" + 
                                 " values " +
                                 " (1,'" + tbResUser.Text + "', '" + tbResEmail.Text + "', '" + tbResPassword.Text + "',0)";
            //1 is hardcoded as the ID, this needs to be setup as a sequence
            DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
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