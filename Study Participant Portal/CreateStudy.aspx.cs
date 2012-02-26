using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class CreateStudy : System.Web.UI.Page {

    Study study = null;
    private int editIndex = 0;

    protected void Page_Load(object sender, EventArgs e) {
        lblError.Visible = false;
        lblErrorAdd.Visible = false;
        lblErrorCont.Visible = false;
        lblErrirFinish.Visible = false;
        if (!IsPostBack && Request.QueryString["edit"] == "true") {
            study = new Study(Convert.ToInt32(Request.QueryString["study_id"]));
            tbTitle.Text = study.StudyName;
            tbDescription.Text = study.StudyDescription;
            pnlQuals.Enabled = true;
            if (study.Expired == true) {
                cbStdExpired.Checked = true;
            }
            else {
                cbStdExpired.Checked = false;
            }
        }
        else {

            int stdid = 0;
            if (Session["studyID"] != null) {
                stdid = (int)Session["studyID"];
            }
            DatabaseQuery query = null;
            string queryString = "select Name from Study where Study_ID = " + stdid.ToString();
            query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);

            // disable the qualifier panel if this is a new study.
            if (query.Results.Count() == 0) {
                pnlQuals.Enabled = false;
            }

            // make sure the panel is enabled if it is a postback.
            if (IsPostBack) {
                pnlQuals.Enabled = true;
            }
        }
    }

    /// <summary>
    /// Cancel out of the form and redirect back to the researcher page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStdCancel_Click(object sender, EventArgs e) {
        Response.Redirect("ResearcherForm.aspx");
    }

    /// <summary>
    /// submit study information into the data base. Upon completion set a 
    /// session variable of study ID.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnStdSubmit_Click(object sender, EventArgs e) {
        pnlQuals.Enabled = true;
        if (tbTitle.Text.Equals(string.Empty) || tbDescription.Text.Equals(string.Empty)) {
            lblError.Text = "Please fill out the necassary fields.";
            lblError.Visible = true;
        }

        bool isNewStudy = !Convert.ToBoolean(Request.QueryString["edit"]);
        Researcher res = (Researcher)Session["user"];
        StringBuilder queryString = null;
        DatabaseQuery query = null;

        switch (isNewStudy) {
            case true:
                if (cbStdExpired.Checked == true) {
                    lblError.Text = "Cannot set a new study to expired.";
                    lblError.Visible = true;
                    return;
                }
                queryString = new StringBuilder("insert into Study");
                queryString.Append(" (Name, Description, Creation_date, Expired, Res_ID)");
                queryString.Append(" values ");
                queryString.Append(" ('" + tbTitle.Text + "', '" + tbDescription.Text + "', ");
                queryString.Append("NOW()" + ", 0, " + res.User_id + ")");
                query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);
                break;
            case false:
                queryString = new StringBuilder("update Study");
                queryString.Append(" set Name = '" + tbTitle.Text + "'" );
                queryString.Append(", Description = '" + tbDescription.Text + "'");
                if (cbStdExpired.Checked == true) queryString.Append(", Expired = " + "1");
                else queryString.Append(", Expired = " + "0");
                queryString.Append(" where Study_ID = " + Request.QueryString["study_id"]);
                query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Update);
                break;
        }
        
        // query to get the value of our study ID for the session variable
        string studyQuery = "select Study_ID from Study where Name = '" + tbTitle.Text+ "'";
        query = new DatabaseQuery(studyQuery, DatabaseQuery.Type.Select);
        int study_id = Convert.ToInt32(query.Results[0][0]);

        // set a studyid session variable
        Session["studyID"] = study_id;

        
    }

    /// <summary>
    /// Method to add Answers to the answer list box. This button will also work
    /// in combination with the edit button to resubmit and edited answer.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddAnswer_Click(object sender, EventArgs e) {

        // regular expression to enure only numbers are entered in the rank text field.
        Regex reg = new Regex(@"^[0-9]+$");

        if (tbAnswer.Text.Equals(string.Empty) || tbRank.Text.Equals(string.Empty) || (!reg.IsMatch(tbRank.Text))) {
            lblErrorAdd.Text = "Please provide an answer, and a rank. Make sure the rank is in the form of a diget";
            lblErrorAdd.Visible = true;
        }
        else {
            if (tbQuestion.Enabled == false) {
                lbAnswerList.Items[editIndex].Text = tbAnswer.Text + " [" + tbRank.Text + "]";
                tbQualDesc.Enabled = true;
                tbQuestion.Enabled = true;
                lbAnswerList.Enabled = true;
                btnRemove.Enabled = true;
                btnClear.Enabled = true;
                btnEdit.Enabled = true;
                btnContinue.Enabled = true;
                btnFinished.Enabled = true;
                editIndex = 0;

            }
            else {
                lbAnswerList.Items.Add(tbAnswer.Text + " [" + tbRank.Text + "]");                
            }
            tbAnswer.Text = string.Empty;
            tbRank.Text = string.Empty;
        }
    }

    /// <summary>
    /// Simply remove the selected answer from the list box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemove_Click(object sender, EventArgs e) {
        if (lbAnswerList.SelectedIndex == -1) {
            //error label logic here
        }
        else {
            lbAnswerList.Items.Remove(lbAnswerList.SelectedItem.Text);
        }
    }

    /// <summary>
    /// Clear the Answer list box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e) {
        lbAnswerList.Items.Clear();
    }

    /// <summary>
    /// This button is to edit an answer already submitted to the list box.
    /// It will repopulate the answer and rank text fields for editing. Upon 
    /// completion, user will simply push the add button and the field will 
    /// be corrected in the list box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e) {
        List<string> answerRank;

        if (lbAnswerList.SelectedIndex == -1) {
            lblErrorAdd.Text = "Please select an answer to edit.";
            lblErrorAdd.Visible = true;
        }
        else {
            answerRank = sepearateAnswerAndRank(lbAnswerList.SelectedItem.Text);
            tbAnswer.Text = answerRank[0];
            tbRank.Text = answerRank[1];
            tbQualDesc.Enabled = false;
            tbQuestion.Enabled = false;
            lbAnswerList.Enabled = false;
            btnRemove.Enabled = false;
            btnClear.Enabled = false;
            btnEdit.Enabled = false;
            btnContinue.Enabled = false;
            btnFinished.Enabled = false;
            editIndex = lbAnswerList.SelectedIndex;
        }
    }

    /// <summary>
    /// This method takes all the information from the answer and qualifier fields and
    /// populates the database. It will first populate the Qualifier table, then it
    /// will populate the Study_Qualifier table, and finally it will populate the
    /// Answer table. It will then clear all the text fields and the user may provide 
    /// another qualifier.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinue_Click(object sender, EventArgs e) {
        if (tbQualDesc.Text.Equals(string.Empty) || tbQuestion.Text.Equals(string.Empty) || lbAnswerList.Items.Count == 0) {
            lblErrorCont.Visible = true;
            return;
        }
        int qualID;
        int studyID;
        List<string> answerRank = null;
        StringBuilder queryString = null;
        DatabaseQuery query = null;

        // get the study ID
        queryString = new StringBuilder("select Study_ID");
        queryString.Append(" from Study");
        queryString.Append(" where Name = '" + tbTitle.Text + "'");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Select);
        studyID = Convert.ToInt32(query.Results[0][0]);

        // insert current qualifier record into Qualifiers
        queryString = new StringBuilder("insert into Qualifiers");
        queryString.Append(" (Question, Description)");
        queryString.Append(" values ");
        queryString.Append(" ('" + tbQuestion.Text + "', '" + tbQualDesc.Text + "')");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);

        // get the qualifier ID
        queryString = new StringBuilder("select Qual_ID");
        queryString.Append(" from Qualifiers");
        queryString.Append(" where Question = '" + tbQuestion.Text + "'");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Select);
        qualID = Convert.ToInt32(query.Results[0][0]);

        // insert current study and qualifier id into Study_Qualifiers
        queryString = new StringBuilder("insert into Study_Qualifiers");
        queryString.Append(" (Study_ID, Qual_ID)");
        queryString.Append(" values ");
        queryString.Append(" (" + studyID.ToString() + ", " + qualID.ToString() + ")");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);

        // insert answers into Answer table
        for (int i = 0; i < lbAnswerList.Items.Count; i++) {
            answerRank = sepearateAnswerAndRank(lbAnswerList.Items[i].Text);
            queryString = new StringBuilder("insert into Answers");
            queryString.Append(" (Qual_ID, Answer, Rank)");
            queryString.Append(" values ");
            queryString.Append(" (" + qualID.ToString() + ", '" + answerRank[0] + "', " + answerRank[1] + ")");
            query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);
        }

        // clear the contents of the fields for new entries
        tbQualDesc.Text = string.Empty;
        tbQuestion.Text = string.Empty;
        tbAnswer.Text = string.Empty;
        tbRank.Text = string.Empty;
        lbAnswerList.Items.Clear();
    }

    /// <summary>
    /// This method does pretty much the same as above only this time upon completion
    /// redirects the user to the Study form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFinished_Click(object sender, EventArgs e) {
        if (tbQualDesc.Text.Equals(string.Empty) || tbQuestion.Text.Equals(string.Empty) || lbAnswerList.Items.Count == 0) {
            lblErrirFinish.Visible = true;
            return;
        }

        int qualID;
        int studyID;
        List<string> answerRank = null;
        StringBuilder queryString = null;
        DatabaseQuery query = null;

        // get the study ID
        queryString = new StringBuilder("select Study_ID");
        queryString.Append(" from Study");
        queryString.Append(" where Name = '" + tbTitle.Text + "'");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Select);
        studyID = Convert.ToInt32(query.Results[0][0]);

        // insert current qualifier record into Qualifiers
        queryString = new StringBuilder("insert into Qualifiers");
        queryString.Append(" (Question, Description)");
        queryString.Append(" values ");
        queryString.Append(" ('" + tbQuestion.Text + "', '" + tbQualDesc.Text + "')");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);

        // get the qualifier ID
        queryString = new StringBuilder("select Qual_ID");
        queryString.Append(" from Qualifiers");
        queryString.Append(" where Question = '" + tbQuestion.Text + "'");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Select);
        qualID = Convert.ToInt32(query.Results[0][0]);

        // insert current study and qualifier id into Study_Qualifiers
        queryString = new StringBuilder("insert into Study_Qualifiers");
        queryString.Append(" (Study_ID, Qual_ID)");
        queryString.Append(" values ");
        queryString.Append(" (" + studyID.ToString() + ", " + qualID.ToString() + ")");
        query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);

        // insert answers into Answer table
        for (int i = 0; i < lbAnswerList.Items.Count; i++) {
            answerRank = sepearateAnswerAndRank(lbAnswerList.Items[i].Text);
            queryString = new StringBuilder("insert into Answers");
            queryString.Append(" (Qual_ID, Answer, Rank)");
            queryString.Append(" values ");
            queryString.Append(" (" + qualID.ToString() + ", '" + answerRank[0] + "', " + answerRank[1] + ")");
            query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);
        }

        Response.Redirect("StudyForm.aspx?study_id=" + studyID);
    }

    /// <summary>
    /// Take the answer and rank combo from the Answer list box and parse it
    /// into two seperate strings containing the answer and the rank. The two
    /// results are stored in a list of strings.
    /// </summary>
    /// <param name="combo"></param>
    /// <returns></returns>
    private List<string> sepearateAnswerAndRank(string combo) {
        List<string> seperated = new List<string>();

        for (int i = 0; i < combo.Length; i++) {
            if (combo.Substring(i, 1).Equals("[")) {
                for (int j = i + 1; j < combo.Length; j++) {
                    if (combo.Substring(j, 1).Equals("]")) {
                        seperated.Add(combo.Substring(0, i - 1));
                        seperated.Add(combo.Substring(i + 1, j - i - 1));
                        break;
                    }
                }
                break;
            }
        }
        return seperated;
    }
}