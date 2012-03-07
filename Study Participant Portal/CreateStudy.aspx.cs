using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class CreateStudy : System.Web.UI.Page {

    Study study;
    List<Qualifier> qualifiers = new List<Qualifier>();
    private int editIndex = 0;

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            Session["study"] = null;
            //Editing an existing study
            if (Request.QueryString["edit"] == "true") {
                study = new Study(Convert.ToInt32(Request.QueryString["study_id"]));
                tbName.Text = study.Name;
                tbDescription.Text = study.Description;
                cbStdExpired.Visible = true;
                lblExpired.Visible = true;
                lblExpired2.Visible = true;
                if (study.Expired == true) {
                    cbStdExpired.Checked = true;
                }
            }
        }
        study = (Study)Session["study"];
        if (study != null) {
            qualifiers = study.Qualifiers;
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
    /// Method to add Answers to the answer list box. This button will also work
    /// in combination with the edit button to resubmit and edited answer.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddAnswer_Click(object sender, EventArgs e) {

        // regular expression to enure only numbers are entered in the rank text field.
        Regex reg = new Regex(@"^[-10123456789]+$");

        if (tbAnswer.Text.Equals(string.Empty) || tbScore.Text.Equals(string.Empty) || (!reg.IsMatch(tbScore.Text))) {
            lblErrorAdd.Text = "Please provide an answer, and a score. The score must be between -1-9";
            lblErrorAdd.Visible = true;
        }
        else {
            if (tbQuestion.Enabled == false) {
                lbAnswerList.Items[editIndex].Text = tbAnswer.Text + " [" + tbScore.Text + "]";
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
                //the -1 should be the answer id if it has one
                ListItem item = new ListItem(tbAnswer.Text + " [" + tbScore.Text + "]", "-1");
                lbAnswerList.Items.Add(item);                
            }
            tbAnswer.Text = string.Empty;
            tbScore.Text = string.Empty;
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
            tbScore.Text = answerRank[1];
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

    /// <summary>
    /// This method takes all the information from the answer and qualifier fields and
    /// populates the database. It will first populate the Qualifier table, then it
    /// will populate the Study_Qualifier table, and finally it will populate the
    /// Answer table. It will then clear all the text fields and the user may provide 
    /// another qualifier.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddQualifier(object sender, EventArgs e) {
        if (tbQualDesc.Text.Equals(string.Empty) || tbQuestion.Text.Equals(string.Empty) || lbAnswerList.Items.Count == 0) {
            lblErrorCont.Visible = true;
            return;
        }
        lblErrorCont.Visible = false;

        //todo replace 0 with the SQL INCREMENT ID
        Qualifier qualifier = new Qualifier(-1, tbQuestion.Text ,tbQualDesc.Text);

        foreach (ListItem item in lbAnswerList.Items) {
        //todo replace 0 with the SQL INCREMENT ID
            Answer answer = new Answer(-1, item.Text, Convert.ToInt32(item.Value));
            qualifier.Answers.Add(answer);
        }

        qualifiers.Add(qualifier);
        study = new Study(-1, tbName.Text, tbDescription.Text, DateTime.Now, cbStdExpired.Checked, ((Researcher)(Session["user"])).UserID, qualifiers);
        Session["study"] = study;

        // clear the contents of the fields for new entries
        tbQualDesc.Text = string.Empty;
        tbQuestion.Text = string.Empty;
        tbAnswer.Text = string.Empty;
        tbScore.Text = string.Empty;
        lbAnswerList.Items.Clear();
        populateQualifiers(study.Qualifiers);
        
    }


    /// <summary>
    /// This method does pretty much the same as above only this time upon completion
    /// redirects the user to the Study form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFinished_Click(object sender, EventArgs e) {
        if (tbName.Text.Equals(string.Empty) || tbDescription.Text.Equals(string.Empty)) {
            lblError.Text = "Please fill out the necessary fields.";
            lblError.Visible = true;
            return;
        }
        lblError.Visible = false;

        bool isNewStudy = !Convert.ToBoolean(Request.QueryString["edit"]);
        int expired = Convert.ToInt32(cbStdExpired.Checked);
        Researcher res = (Researcher)Session["user"];
        int studyID = -1; //-1 means that it hasn't been set yet. A study should never have a value of -1 when inserting/updating into the database
        if (!isNewStudy) {
            studyID = Convert.ToInt32(Request.QueryString["studyID"]);
        }
        study = new Study(studyID, tbName.Text, tbDescription.Text, DateTime.Now, Convert.ToBoolean(expired), res.UserID, qualifiers);

        switch (isNewStudy) {
            case true:
                //insert all the data into the database
                study.StudyID = DAL.InsertStudy(study);
                foreach (Qualifier qualifier in study.Qualifiers) {
                    qualifier.QualID = DAL.InsertQualifier(qualifier, study.StudyID);
                    foreach (Answer answer in qualifier.Answers) {
                        DAL.InsertAnswer(answer, qualifier.QualID);
                    }
                }
                break;
            case false:
                DAL.UpdateStudy(study);
                //todo - finish the update section
                break;
        }

        Response.Redirect("StudyForm.aspx?study_id=" + studyID);
    }

    private void populateQualifiers(List<Qualifier> qualifiers) {
        lbQualifiers.Items.Clear();
        if (qualifiers.Count > 0) {
            pnlExistingQuals.Visible = true;
            foreach (Qualifier qual in qualifiers) {
                ListItem item = new ListItem(qual.Description, qual.QualID.ToString());
                lbQualifiers.Items.Add(item);
            }
        }
    }
}