using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page {

    /// <summary>
    /// Method that checks if the fields are correctly filled out before hitting 'submit' to try and create an account. 
    /// This is to disallow them to try and put in null fields, incorrect characters, etc
    /// </summary>
    /// <param name="user">Whether the user is trying to create a Researcher or Participant account</param>
    /// <returns>True if all fields are valid</returns>
    private bool isFormValid(SuperUser.UserType user) {
        switch (user) {
            case SuperUser.UserType.Participant:
                if (tbParPassword.Text == "" || tbParPassword.Text != tbParPasswordConfirm.Text) {
                    lblParStatus.Text = "Invalid Password";
                }
                if (tbParLastName.Text == "") {
                    lblParStatus.Text = "Please enter your Last Name";
                }
                if (tbParFirstName.Text == "") {
                    lblParStatus.Text = "Please enter your First Name";
                }
                if (tbParUserName.Text == "") {
                    lblParStatus.Text = "Please enter a User Name";
                }
                if (lblParStatus.Text != "") {
                    return false;
                }
                break;
            case SuperUser.UserType.Researcher:
                if (tbResPassword.Text == "" || tbResPassword.Text != tbResPasswordConfirm.Text) {
                    lblResStatus.Text = "Invalid Password";
                }
                if (tbResLastName.Text == "") {
                    lblResStatus.Text = "Please enter your Last Name";
                }
                if (tbResFirstName.Text == "") {
                    lblResStatus.Text = "Please enter your First Name";
                }
                if (tbResUserName.Text == "") {
                    lblResStatus.Text = "Please enter a User Name";
                }
                if (lblResStatus.Text != "") {
                    return false;
                }
                break;
        }
        return true;
    }

    private void autoFillForms(SuperUser.UserType userType) {
        SuperUser user = (SuperUser)Session["user"];
        int userID = user.UserID;
        switch (userType) {
            case SuperUser.UserType.Researcher:
                Researcher res = new Researcher(userID);
                tbResUserName.Text = res.UserName;
                tbResFirstName.Text = res.FirstName;
                tbResLastName.Text = res.LastName;
                tbResEmail.Text = res.Email;
                break;
            case SuperUser.UserType.Participant:
                Participant par = new Participant(userID);
                tbParUserName.Text = par.UserName;
                tbParFirstName.Text = par.FirstName;
                tbParLastName.Text = par.LastName;
                tbParEmail.Text = par.Email;
                break;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        
        string user = Request.QueryString["user"];
        if (user == SuperUser.UserType.Researcher.ToString()) {
            pnlResearcher.Visible = true;
            if (Request.QueryString["edit"] == "true") {
                autoFillForms(SuperUser.UserType.Researcher);
            }
        }
        else if (user == SuperUser.UserType.Participant.ToString()) {
            pnlParticipant.Visible = true;
            if (Request.QueryString["edit"] == "true") {
                autoFillForms(SuperUser.UserType.Participant);
            }
        }
    }

    protected void btnResSubmit_Click(object sender, EventArgs e) {
        if (isFormValid(SuperUser.UserType.Researcher)) {
            string queryString = "insert into Researcher" +
                                 " (User_Name, First_Name, Last_Name, Email, Password, Num_Ratings)" + 
                                 " values " +
                                 " ('" + tbResUserName.Text + "', '" + tbResFirstName.Text + "','" + tbResLastName.Text + "', '" + tbResEmail.Text + "', '" + tbResPassword.Text + "',0)";
            DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
            lblResStatus.Text = "";

            queryString = "select Res_ID from Researcher where User_Name = '" + tbResUserName.Text + "'";
            query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
            int user_id = Convert.ToInt32(query.Results[0][0]);

            Researcher res = new Researcher(user_id, tbResUserName.Text, tbResFirstName.Text, tbResLastName.Text, tbResEmail.Text);
            Session["user"] = res;

            Response.Redirect("ResearcherForm.aspx");
        }
    }

    protected void btnParSubmit_Click(object sender, EventArgs e) {
        if (isFormValid(SuperUser.UserType.Participant)) {
            string queryString = "insert into Participant" +
                                 " (User_Name, First_Name, Last_Name, Email, Password, Num_Ratings)" +
                                 " values " +
                                 " ('" + tbParUserName.Text + "', '" + tbParFirstName.Text + "','" + tbParLastName.Text + "','" + tbParEmail.Text + "', '" + tbParPassword.Text + "',0)";
            DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Insert);
            lblParStatus.Text = "";
            Session["user"] = new Participant(tbParUserName.Text, tbParFirstName.Text, tbParLastName.Text, tbParEmail.Text);
            Response.Redirect("ParticipantForm.aspx");
        }
    }

    protected void btnResCancel_Click(object sender, EventArgs e) {
        Response.Redirect("Default.aspx");
    }
    protected void btnParCancel_Click(object sender, EventArgs e) {
        Response.Redirect("Default.aspx");
    }
}