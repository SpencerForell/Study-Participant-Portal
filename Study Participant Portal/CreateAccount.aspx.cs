using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class CreateAccount : System.Web.UI.Page {

    bool invalid = false;

    /// <summary>
    /// Method to try and validate user input for emails (found on http://msdn.microsoft.com/en-us/library/01escwtf.aspx)
    /// </summary>
    /// <param name="strIn"></param>
    /// <returns></returns>
    private bool IsValidEmail(string strIn) {
        invalid = false;
        if (String.IsNullOrEmpty(strIn))
            return false;

        // Use IdnMapping class to convert Unicode domain names.
        strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper);
        if (invalid)
            return false;

        // Return true if strIn is in valid e-mail format.
        bool temp = Regex.IsMatch(strIn,
               @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
               RegexOptions.IgnoreCase);
        return temp;
    }

    /// <summary>
    /// Helper method for IsValidEmail (found on http://msdn.microsoft.com/en-us/library/01escwtf.aspx)
    /// </summary>
    /// <param name="match"></param>
    /// <returns></returns>
    private string DomainMapper(Match match) {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException) {
            invalid = true;
        }
        return match.Groups[1].Value + domainName;
    }

    /// <summary>
    /// Method that checks if the fields are correctly filled out before hitting 'submit' to try and create an account. 
    /// This is to disallow them to try and put in null fields, incorrect characters, etc
    /// </summary>
    /// <param name="user">Whether the user is trying to create a Researcher or Participant account</param>
    /// <returns>True if all fields are valid</returns>
    private bool isFormValid(SuperUser.UserType user) {
        switch (user) {
            case SuperUser.UserType.Participant:
                lblParStatus.Text = "";
                if (tbParPassword.Text == "" || tbParPassword.Text != tbParPasswordConfirm.Text) {
                    lblParStatus.Text = "Invalid Password, they do not match";
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
                if(!IsValidEmail(tbParEmail.Text)){
                    lblParStatus.Text = "Please enter a valid Email";
                }
                if (lblParStatus.Text != "") {
                    return false;
                }
                break;
            case SuperUser.UserType.Researcher:
                lblResStatus.Text = "";
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
                if (!IsValidEmail(tbResEmail.Text)){
                    lblResStatus.Text = "Please enter a valid Email";
                }
                if (lblResStatus.Text != "") {
                    return false;
                }
                break;
        }
        return true;
    }

    /// <summary>
    /// Automatically fills in the appropriate text in the forms from the given logged in user
    /// This should only be used when a user is logged in and their information is stored in the Session
    /// </summary>
    /// <param name="userType">Determines if user is a Researcher or Participant</param>
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
        if (!IsPostBack) {
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
    }

    /// <summary>
    /// Event handler when a researcher is finished creating/editing an account. 
    /// If there are no errors, the session variable will be set to the user and they
    /// will be redirected to the researcher form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnResSubmit_Click(object sender, EventArgs e) {
        if (isFormValid(SuperUser.UserType.Researcher)) {
            string queryString = "";
            DatabaseQuery query;
            if (Request.QueryString["edit"] == "true") {
                int resID = ((Researcher)Session["user"]).UserID;
                DAL.UpdateResearcher(resID, tbResFirstName.Text, tbResLastName.Text, tbResUserName.Text, tbResEmail.Text, tbResPassword.Text);
            }
            else {
                try {
                    DAL.InsertResearcher(tbResUserName.Text, tbResFirstName.Text, tbResLastName.Text, tbResEmail.Text, tbResPassword.Text);
                }
                catch (Exception exception) {
                    lblParStatus.Text = exception.Message;
                    lblParStatus.Visible = true;
                    return;
                }
            }    
            lblResStatus.Text = "";

            queryString = "select Res_ID from Researcher where User_Name = '" + tbResUserName.Text + "'";
            query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
            int user_id = Convert.ToInt32(query.Results[0][0]);

            Researcher res = new Researcher(user_id, tbResUserName.Text, tbResFirstName.Text, tbResLastName.Text, tbResEmail.Text);
            Session["user"] = res;

            Response.Redirect("ResearcherForm.aspx");
        }
    }

    /// <summary>
    /// Event handler when a participant is finished creating/editing an account. 
    /// If there are no errors, the session variable will be set to the user and they
    /// will be redirected to the participant form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnParSubmit_Click(object sender, EventArgs e) {
        if (isFormValid(SuperUser.UserType.Participant)) {
            DatabaseQuery query;
            string queryString = "";
            if (Request.QueryString["edit"] == "true") {
                int parID = ((Participant)Session["user"]).UserID;
                DAL.UpdateParticipant(tbParUserName.Text, tbParFirstName.Text, tbParLastName.Text, tbParEmail.Text, tbParPassword.Text, parID);
            }
            else {
                try {
                    DAL.InsertParticipant(tbParUserName.Text, tbParFirstName.Text, tbParLastName.Text, tbParEmail.Text, tbParPassword.Text);

                }
                catch (Exception exception) {
                    lblParStatus.Text = exception.Message;
                    lblParStatus.Visible = true;
                    return;
                }              
            }
            
            lblParStatus.Text = "";

            queryString = "select Par_ID from Participant where User_Name = '" + tbParUserName.Text + "'";
            query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
            int userID = Convert.ToInt32(query.Results[0][0]);

            Session["user"] = new Participant(userID, tbParUserName.Text, tbParFirstName.Text, tbParLastName.Text, tbParEmail.Text, new List<Answer>());
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