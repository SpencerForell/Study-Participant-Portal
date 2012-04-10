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
    private int ansEditIndex = -1; //the answer being selected to edit
    private int qualEditIndex = -1; //the qualifier being selected to edit
    private bool isEdit = false; //boolean that is set depending on if the study is new or being updated

    protected void Page_Load(object sender, EventArgs e) {
        isEdit = Convert.ToBoolean(Request.QueryString["edit"] == "true");
        qualEditIndex = Convert.ToInt32(Session["qualEditIndex"]);
        if (!IsPostBack) {
            Session["study"] = null;
            //Editing an existing study
            if (isEdit) {
                study = new Study(Convert.ToInt32(Request.QueryString["study_id"]));
                populateForm(study);
                Session["study"] = study;
            }
        }
        study = (Study)Session["study"];
        //if the study is null, create a temp study that will be altered later.
        if (study == null) {
            study = new Study(-1, "", "", DateTime.Now, false, 0, new List<Qualifier>());
        }
    }

    /// <summary>
    /// Method that automatically fills out all of the forms based on the study given as a parameter
    /// </summary>
    /// <param name="study"></param>
    private void populateForm(Study study) {
        tbName.Text = study.Name;
        tbDescription.Text = study.Description;
        cbStdExpired.Visible = true;
        lblExpired.Visible = true;
        lblExpired2.Visible = true;
        if (study.Expired == true) {
            cbStdExpired.Checked = true;
        }
        lbQualifiers.Items.Clear();
        foreach (Qualifier qualifier in study.Qualifiers) {
            pnlExistingQuals.Visible = true;
            ListItem item = new ListItem(qualifier.Description, qualifier.QualID.ToString());
            lbQualifiers.Items.Add(item);
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
            lblErrorAdd.Visible = true;
        }
        else {
            lblErrorAdd.Visible = false;
            if (tbQuestion.Enabled == false) {
                lbAnswerList.Items[ansEditIndex].Text = tbAnswer.Text + " [" + tbScore.Text + "]";
                tbQualDesc.Enabled = true;
                tbQuestion.Enabled = true;
                lbAnswerList.Enabled = true;
                btnRemove.Enabled = true;
                btnClear.Enabled = true;
                btnEdit.Enabled = true;
                btnContinue.Enabled = true;
                btnFinished.Enabled = true;
                ansEditIndex = 0;

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
            ansEditIndex = lbAnswerList.SelectedIndex;
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
    protected void btnSaveQualifier(object sender, EventArgs e) {
        if (tbQualDesc.Text.Equals(string.Empty) || tbQuestion.Text.Equals(string.Empty) || lbAnswerList.Items.Count == 0) {
            lblErrorCont.Visible = true;
            return;
        }
        lblErrorCont.Visible = false;

        Qualifier qualifier = null;
        int qualID = -1;
        if (qualEditIndex > -1) {
            qualID = study.Qualifiers[qualEditIndex].QualID;
        }
        qualifier = new Qualifier(qualID, tbQuestion.Text, tbQualDesc.Text);
        foreach (ListItem item in lbAnswerList.Items) {
            List<String> ansRank = sepearateAnswerAndRank(item.Text);
            string ans = ansRank[0];
            int rank = Convert.ToInt32(ansRank[1]);
            int ansID = -1;
            if (Convert.ToInt32(item.Value) > 0) {
                ansID = Convert.ToInt32(item.Value);
            }
            Answer answer = new Answer(ansID, ans, rank, qualifier);
            qualifier.Answers.Add(answer);
        }

        //qualifier doesn't exist, add it to the list
        if (qualifier.QualID < 0) {
            study.Qualifiers.Add(qualifier);
        }
        //qualifier already exists, we are just updating it, remove the old one and replace it with the new one
        else {
            study.Qualifiers.Remove(study.Qualifiers[qualEditIndex]);
            study.Qualifiers.Add(qualifier);
        }
        int studyID = -1;
        if (isEdit) {
            studyID = Convert.ToInt32(Request.QueryString["study_id"]);
        }
        study = new Study(studyID, tbName.Text, tbDescription.Text, DateTime.Now, cbStdExpired.Checked, ((Researcher)(Session["user"])).UserID, study.Qualifiers);
        Session["study"] = study;

        // clear the contents of the fields for new entries
        tbQualDesc.Text = string.Empty;
        tbQuestion.Text = string.Empty;
        tbAnswer.Text = string.Empty;
        tbScore.Text = string.Empty;
        lbAnswerList.Items.Clear();
        populateQualifiers(study.Qualifiers);
        Session["qualEditIndex"] = -1;
        pnlQuals.Visible = false;
        pnlExistingQuals.Visible = true;
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

        int expired = Convert.ToInt32(cbStdExpired.Checked);
        Researcher res = (Researcher)Session["user"];
        int studyID = -1; //-1 means that it hasn't been set yet. A study should never have a value of -1 when updating into the database
        if (isEdit) {
            studyID = Convert.ToInt32(Request.QueryString["study_id"]);
        }
        study = new Study(studyID, tbName.Text, tbDescription.Text, DateTime.Now, Convert.ToBoolean(expired), res.UserID, study.Qualifiers);

        switch (!isEdit) {
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
                study.StudyID = studyID;
                DAL.UpdateStudy(study);
                foreach (Qualifier qualifier in study.Qualifiers) {
                    if (qualifier.QualID == -1) {
                        qualifier.QualID = DAL.InsertQualifier(qualifier, study.StudyID);
                    }
                    else {
                        DAL.UpdateQualifier(qualifier);
                    }
                    foreach (Answer answer in qualifier.Answers) {
                        if (answer.AnsID == -1) {
                            DAL.InsertAnswer(answer, qualifier.QualID);
                        }
                        else {
                            DAL.UpdateAnswer(answer);
                        }
                    }
                }
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

    protected void btnEditQual_Click(object sender, EventArgs e) {
        if (lbQualifiers.SelectedIndex < 0) {
            lblEditQualError.Visible = true;
        }
        else {
            lblEditQualError.Visible = false;
            pnlQuals.Visible = true;
            clearQualForms();
            pnlExistingQuals.Visible = false;

            qualEditIndex = lbQualifiers.SelectedIndex;
            Session["qualEditIndex"] = qualEditIndex; 
            //populate the answers from the selected qual
            int qualID = Convert.ToInt32(lbQualifiers.SelectedValue);
            Qualifier qual = study.Qualifiers[qualEditIndex];
            tbQualDesc.Text = qual.Description;
            tbQuestion.Text = qual.Question;

            lbAnswerList.Items.Clear();
            foreach (Answer answer in qual.Answers) {
                ListItem item = new ListItem(answer.AnswerText + " [" + answer.Score.ToString() + "]", answer.AnsID.ToString());
                lbAnswerList.Items.Add(item);
            }
        }
    }
    protected void btnNewQual_Click(object sender, EventArgs e) {
        pnlQuals.Visible = true;
        clearQualForms();
        lbQualifiers.SelectedIndex = -1;
        Session["qualEditIndex"] = -1;
        pnlExistingQuals.Visible = false;
    }
    protected void btnQualCancel_Click(object sender, EventArgs e) {
        pnlQuals.Visible = false;
        pnlExistingQuals.Visible = true;
    }

    private void clearQualForms() {
        tbDescription.Text = "";
        tbQualDesc.Text = "";
        tbQuestion.Text = "";
        tbScore.Text = "";
        tbAnswer.Text = "";
    }
}