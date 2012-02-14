using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page {
    
    protected void Page_Load(object sender, EventArgs e) {
        
        string user = Request.QueryString["user"];
        if (user == SuperUser.UserType.Researcher.ToString()) {
            pnlResearcher.Visible = true;
        }
        else if (user == SuperUser.UserType.Participant.ToString()) {
            pnlParticipant.Visible = true;
        }

    }
    protected void btnResSubmit_Click(object sender, EventArgs e) {
        if (tbResPassword.Text != tbResPasswordConfirm.Text) {
            lblResStatus.Text = "Passwords did not match!";
        }
        else {
            string queryString = "insert into Researcher" +
                                 " (Name, Email, Password, Num_Ratings)" + 
                                 " values " +
                                 " ('" + tbResUser.Text + "', '" + tbResEmail.Text + "', '" + tbResPassword.Text + "',0)";
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