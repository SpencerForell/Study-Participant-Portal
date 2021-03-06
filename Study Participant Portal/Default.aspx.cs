﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {

    /* EVENT HANDLERS*/

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Session["user"] == null) {
                
            }
            else {
                SuperUser user = (SuperUser) Session["user"];
                switch(user.Type) {
                    case SuperUser.UserType.Researcher:
                        Response.Redirect("ResearcherForm.aspx");
                        break;
                    case SuperUser.UserType.Participant:
                        Response.Redirect("ParticipantForm.aspx");
                        break;
                }
            }
            updateLatestStudy();
             
        }
    }

    protected void btnResearcher_Click(object sender, EventArgs e) {
        pnlMain.Visible = false;
        pnlResearcher.Visible = true;
        pnlWeeklyStudy.Visible = false;
    }

    protected void btnParticipant_Click(object sender, EventArgs e) {
        pnlMain.Visible = false;
        pnlParticipant.Visible = true;
        pnlWeeklyStudy.Visible = false;
    }

    protected void btnResCancel_Click(object sender, EventArgs e) {
        pnlMain.Visible = true;
        pnlWeeklyStudy.Visible = true;
        pnlResearcher.Visible = false;
    }

    protected void btnResCreateAcc_Click(object sender, EventArgs e) {      
        Response.Redirect("CreateAccount.aspx?user=Researcher"); 
    }


    protected void btnParCancel_Click(object sender, EventArgs e) {
        pnlMain.Visible = true;
        pnlWeeklyStudy.Visible = true;
        pnlParticipant.Visible = false;
    }

    protected void btnParCreateAcc_Click(object sender, EventArgs e) {
        Response.Redirect("CreateAccount.aspx?user=Participant");
    }

    /// <summary>
    /// Event handler for when a Participant tries to login. 
    /// On Success they will be directed to the Participant Form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnParSubmit_Click(object sender, EventArgs e) {
        string queryString = "select Par_ID, user_name, first_name, last_name, email from Participant where user_name = '" + tbParUser.Text + "' and password = '" + tbParPassword.Text + "'";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        if (query.Results.Count == 0) {
            lblParStatus.Text = "Invalid login. Please try again.";
        }
        else if (query.Results.Count == 1) {
            int user_id = Convert.ToInt32(query.Results[0][0]);
            string user_name = query.Results[0][1];
            string first_name = query.Results[0][2];
            string last_name = query.Results[0][3];
            string email = query.Results[0][4];
            Participant par = new Participant(user_id, user_name, first_name, last_name, email, new List<Answer>());
            Session["user"] = par;
            Response.Redirect("ParticipantForm.aspx");
        }
        else {
            throw new Exception("submitting user_name: " + tbParUser.Text + " returned incorrect number of rows: " + query.Results.Count);
        }
    }

    /// <summary>
    /// Event handler for when a Researcher tries to login. 
    /// On Success they will be directed to the Researcher Form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Sets all of the information in the latest study table to the most recently created study.
    /// </summary>
    private void updateLatestStudy() {
        Study study = DAL.GetLatestStudy();
        lblWeeklyStudyName.Text = "Name: " + study.Name;
        lblWeeklyIncentive.Text = "Incentive: " + study.Incentive;
        lblWeeklyStudyDesc.Text = "Description: " + study.Description;
    }
}
