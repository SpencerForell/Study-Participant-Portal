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
    private List<Qualifier> existingQualList;
    StringBuilder temp = null;

    protected void Page_Load(object sender, EventArgs e) {
        int count;
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
            //create a temporary study that will be overwritten later
            study = new Study(-1, "", "", "", DateTime.Now, false, 0, new List<Qualifier>());
        }
        // Create all of our qualifiers to be used to select from
        existingQualList = ConstructQualifiers(new Dictionary<int, List<List<string>>>());

        // populate Pre existing qualifier list box
        if (!IsPostBack) {
            foreach (Qualifier qual in existingQualList) {
                lbPreDefinedQuals.Items.Add(qual.Question);
                count = lbPreDefinedQuals.Items.Count - 1;
                lbPreDefinedQuals.Items[count].Attributes.Add("Answer", "Test");
            }
        }
   
        if (!IsPostBack) {
            Session["answerText"] = new List<string>();
            foreach (Qualifier qual in existingQualList) {
                temp = new StringBuilder("Answers: \n");
                foreach (Answer ans in qual.Answers) {
                    temp.AppendLine(ans.AnswerText);
                }
                ((List<string>)Session["answerText"]).Add(temp.ToString());
                temp = null;
            }
        }

        for (int i = 0; i < lbPreDefinedQuals.Items.Count; i++) {
            lbPreDefinedQuals.Items[i].Attributes.Add("Title", ((List<string>)Session["AnswerText"])[i]);
        }
    }

    private List<Qualifier> ConstructQualifiers(Dictionary<int, List<List<string>>> rawData) {
        Dictionary<int, List<List<string>>> qualifiersRaw = DAL.GetAllQualifiers();
        List<Qualifier> qualifiers = new List<Qualifier>();
        List<Answer> answers = null;
        Qualifier qualifier = null;
        Answer answer = null;
        bool exists = false;
        int index = 0;

        // iterate through each answer in the raw dictionary
        foreach (KeyValuePair<int, List<List<string>>> kvp in qualifiersRaw) {
            exists = false;
            answers = new List<Answer>();
            //iterate through each record in the raw qualifier
            foreach (List<string> record in kvp.Value) {
                for (int i = 0; i < qualifiers.Count; i++) {
                    if (qualifiers[i].QualID == Convert.ToInt32(record[0])) {
                        exists = true;
                        index = i;
                        break;
                    }
                }
                if (exists == false) {
                    qualifier = new Qualifier(Convert.ToInt32(record[0]), record[1], record[2], Convert.ToInt32(record[3]), new List<Answer>());
                    answer = new Answer(Convert.ToInt32(record[4]), record[5], Convert.ToInt32(record[6]), qualifier);
                    answers.Add(answer);
                    qualifier.Answers.Add(answer);
                    qualifiers.Add(qualifier);
                }
                else {
                    answer = new Answer(Convert.ToInt32(record[4]), record[5], Convert.ToInt32(record[6]), qualifiers[index]);
                    answers.Add(answer);
                    qualifiers[index].Answers.Add(answer);
                }
            }
        }
        return qualifiers;
    }

    /// <summary>
    /// Method that automatically fills out all of the forms based on the study given as a parameter
    /// </summary>
    /// <param name="study"></param>
    private void populateForm(Study study) {
        tbName.Text = study.Name;
        tbDescription.Text = study.Description;
        tbIncentive.Text = study.Incentive;
        cbStdExpired.Visible = true;
        lblExpired.Visible = true;
        lblExpired2.Visible = true;
        if (study.Expired == true) {
            cbStdExpired.Checked = true;
        }

        //fills in the list box of qualifiers
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
        ansEditIndex = Convert.ToInt32(Session["ansEditIndex"]);
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
    protected void btnRemoveAnswer_Click(object sender, EventArgs e) {
        if (lbAnswerList.SelectedIndex == -1) {
            lblErrorAdd.Visible = true;
            lblErrorAdd.Text = "Please Select an Answer to remove";
        }
        else {
            lbAnswerList.Items.Remove(lbAnswerList.Items[lbAnswerList.SelectedIndex]);
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

        //make sure the user has selected an Answer to edit
        if (lbAnswerList.SelectedIndex == -1) {
            lblErrorAdd.Text = "Please select an answer to edit.";
            lblErrorAdd.Visible = true;
        }
        else {
            ansEditIndex = lbAnswerList.SelectedIndex;
            Session["ansEditIndex"] = ansEditIndex; //set session variable so the value persists through postback
            //get the Possible Answer and the Score from the listbox selected item
            List<string> answerRank = sepearateAnswerAndRank(lbAnswerList.SelectedItem.Text);
            tbAnswer.Text = answerRank[0];
            tbScore.Text = answerRank[1];

            //set other fields to disabled so user MUST finish editing the answer before continuing
            tbQualDesc.Enabled = false;
            tbQuestion.Enabled = false;
            lbAnswerList.Enabled = false;
            btnRemove.Enabled = false;
            btnClear.Enabled = false;
            btnEdit.Enabled = false;
            btnContinue.Enabled = false;
            btnFinished.Enabled = false;
        }
    }
  

    /// <summary>
    /// Take the answer and rank combo from the Answer list box and parse it
    /// into two seperate strings containing the answer and the rank. The two
    /// results are stored in a list of strings.
    /// </summary>
    /// <param name="combo"></param>
    /// <returns>Returns the answer as the first index in the list [0] and the score as the second [1]</returns>
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
        //error checking to make sure they filled out all of the forms correctly
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
        qualifier = new Qualifier(qualID, tbQuestion.Text, tbQualDesc.Text, Convert.ToInt32(((Researcher)Session["user"]).UserID));
        //clear all of the answers and then add them in one at a time to the qualifier
        qualifier.Answers.Clear();
        foreach (ListItem item in lbAnswerList.Items) {
            List<String> ansRank = sepearateAnswerAndRank(item.Text);
            string ans = ansRank[0];
            int rank = Convert.ToInt32(ansRank[1]);
            int ansID = -1;
            if (Convert.ToInt32(rank) > 0) {
                ansID = Convert.ToInt32(rank);
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
        study = new Study(studyID, tbName.Text, tbDescription.Text, tbIncentive.Text, DateTime.Now, cbStdExpired.Checked, ((Researcher)(Session["user"])).UserID, study.Qualifiers);
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
    /// Takes all of the data filled out for the study and either inserts or updates it into the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFinished_Click(object sender, EventArgs e) {
        //Check that the name and description are filled in.
        if (tbName.Text.Equals(string.Empty) || tbDescription.Text.Equals(string.Empty)) {
            lblError.Text = "Please fill out the necessary fields.";
            lblError.Visible = true;
            return;
        }
        lblError.Visible = false;

        int expired = Convert.ToInt32(cbStdExpired.Checked);
        Researcher res = (Researcher)Session["user"];
        int studyID = -1; //-1 means that it hasn't been set yet. Only a new study should have the value of -1, otherwise it should be the ID of the study in the database
       
        //check to see if we are editing or creating a study
        if (isEdit) {
            studyID = Convert.ToInt32(Request.QueryString["study_id"]);
        }
        //Create a study object with all of its attributes filled out from the forms that the user entered
        study = new Study(studyID, tbName.Text, tbDescription.Text, tbIncentive.Text, DateTime.Now, Convert.ToBoolean(expired), res.UserID, study.Qualifiers);

        finishStudy(study);

        //Send an email A notification email out to all the participants that a new study is ready
        if (!isEdit) {
            EmailSender mailSender = new EmailSender();
            List<string> recipients = DAL.GetRecipientEmails();
            string subject = "New research studies are available!";
            StringBuilder body = new StringBuilder("Please log on to the Study Participant Portal to check out new studies!");
            mailSender.sendEmail("NoReply@gmail.com", recipients, subject, body);

            mailSender = null;
            body = null;
        }
    }


    /// <summary>
    /// Helper method that decides whether each part of the study should be inserted or updated into the database
    /// </summary>
    /// <param name="study"></param>
    private void finishStudy(Study study) {
        //if studyID is -1 it is a new study, insert it into the database
        if (study.StudyID == -1) {
            study.StudyID = DAL.InsertStudy(study);
        }
        else {
            DAL.UpdateStudy(study);
        }
        foreach (Qualifier qualifier in study.Qualifiers) {
            //if qualifierID is -1 it is a new qualifier, insert it into the database
            if (qualifier.QualID == -1) {
                qualifier.QualID = DAL.InsertQualifier(qualifier, study.StudyID);
            }
            else {
                //if the study already has this qualifier in it than just update it
                if (DAL.QualifierExistsInStudy(study.StudyID, qualifier.QualID)) {
                    DAL.UpdateQualifier(qualifier);
                }
                //if the study doesn't have this qualifier than they must be adding an existing
                //study and we only need to add it to the Study_Qualifier table but not the Qualifier table
                else {
                    DAL.InsertExisingQualifier(qualifier, study.StudyID);
                }
            }
            //if answerID is -1, it is a new answer, insert it into the database
            foreach (Answer answer in qualifier.Answers) {
                if (answer.AnsID == -1) {
                    int ansID = DAL.InsertAnswer(answer, qualifier.QualID);
                    answer.AnsID = ansID;
                }
                else {
                    DAL.UpdateAnswer(answer);
                }
            }
        }

        removeDeletedAnwers(study);
        removeDeletedQualifiers(study);

        Response.Redirect("StudyForm.aspx?study_id=" + study.StudyID);
    }

    /// <summary>
    /// Queries the database to compare what Answers currently exist in the 
    /// database with the study passed in and removes them from the database if they no longer exist.
    /// </summary>
    /// <param name="study"></param>
    private void removeDeletedAnwers(Study study) {
        string queryString = "select Ans_ID from Study_Qualifiers sq, Answers a where sq.Qual_ID = a.Qual_ID and sq.Study_ID = " + study.StudyID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        List<Answer> oldAnswers = new List<Answer>();
        for (int i = 0; i < query.Results.Count; i++) {
            oldAnswers.Add(new Answer(Convert.ToInt32(query.Results[i][0])));
        }

        foreach (Answer oldAnswer in oldAnswers) {
            bool found = false;
            foreach (Qualifier qual in study.Qualifiers) {
                foreach(Answer ans in qual.Answers){
                    if (oldAnswer.AnsID == ans.AnsID) {
                        found = true;
                        break;
                    }
                }
            }
            if (!found) {
                DAL.DeleteAnswer(oldAnswer);
            }
        }
    }

    /// <summary>
    /// Queries the database to compare what Qualifiers currently exist in the 
    /// database with the study passed in and removes them from the database if they no longer exist.
    /// </summary>
    /// <param name="study"></param>
    private void removeDeletedQualifiers(Study study) {
        string queryString = "select Qual_ID from Study_Qualifiers where Study_ID = " + study.StudyID;
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        List<Qualifier> oldQualifiers = new List<Qualifier>();
        for (int i = 0; i < query.Results.Count; i++) {
            oldQualifiers.Add(new Qualifier(Convert.ToInt32(query.Results[i][0])));
        }

        foreach (Qualifier oldQualifier in oldQualifiers) {
            bool found = false;
            foreach (Qualifier qual in study.Qualifiers) {
                if (oldQualifier.QualID== qual.QualID) {
                    found = true;
                    break;
                }   
            }
            if (!found) {
                DAL.DeleteQualifier(oldQualifier, study);
            }
        }
    }

    /// <summary>
    /// Auto-populates the listbox to fill them with the qualifiers passed in as the parameter.
    /// </summary>
    /// <param name="qualifiers"></param>
    private void populateQualifiers(List<Qualifier> qualifiers) {
        //Make sure the listbox is empty before populating
        lbQualifiers.Items.Clear();
        //Go through the qualifiers and at them to the qualifiers listbox
        foreach (Qualifier qual in qualifiers) {
            ListItem item = new ListItem(qual.Question, qual.QualID.ToString());
            lbQualifiers.Items.Add(item);
        }   
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditQual_Click(object sender, EventArgs e) {
        //check for valid input for editing a Qualifier
        if (lbQualifiers.SelectedIndex < 0) {
            lblEditQualError.Visible = true;
        }
        else {
            lblEditQualError.Visible = false;
            pnlQuals.Visible = true;
            clearQualForms();
            pnlExistingQuals.Visible = false;
            pnlPreExistingQuals.Visible = false;

            //get the index of the Qualifier we are editing
            qualEditIndex = lbQualifiers.SelectedIndex;
            //store the index in a session variable so we can keep it through a postback
            Session["qualEditIndex"] = qualEditIndex; 
            //populate the answers from the selected qual
            ///int qualID = Convert.ToInt32(lbQualifiers.SelectedValue);
            fillInQualForms(study.Qualifiers[qualEditIndex]);
        }
    }

    /// <summary>
    /// Autofills in the forms requried for editing a qualifier
    /// </summary>
    /// <param name="qualifier">The qualifier that is going to be auto-populated</param>
    private void fillInQualForms(Qualifier qualifier) {
        tbQualDesc.Text = qualifier.Description;
        tbQuestion.Text = qualifier.Question;

        //make sure the listbox is empty before populating it
        lbAnswerList.Items.Clear();
        //go through each answer in the qualifier and add it to the listbox
        foreach (Answer answer in qualifier.Answers) {
            ListItem item = new ListItem(answer.AnswerText + " [" + answer.Score.ToString() + "]", answer.AnsID.ToString());
            lbAnswerList.Items.Add(item);
        }
    }

    /// <summary>
    /// Action performed when creating a new Qualifier.
    /// Sets the qualEditIndex to -1 and the Qualifiers listbox selected value to -1.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNewQual_Click(object sender, EventArgs e) {
        pnlQuals.Visible = true;
        clearQualForms();
        lbQualifiers.SelectedIndex = -1;
        Session["qualEditIndex"] = -1;
        pnlExistingQuals.Visible = false;
        pnlPreExistingQuals.Visible = true;
    }

    /// <summary>
    /// If the user cancels creating a qualifier just set the panels visibility 
    /// to hidden that contains all the fields required for creating/editing Qualifiers.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQualCancel_Click(object sender, EventArgs e) {
        pnlQuals.Visible = false;
        pnlExistingQuals.Visible = true;
    }


    /// <summary>
    /// Clears the fields requried for creating/editing Qualifiers.
    /// </summary>
    private void clearQualForms() {
        tbQualDesc.Text = "";
        tbQuestion.Text = "";
        tbScore.Text = "";
        tbAnswer.Text = "";
        lbAnswerList.Items.Clear();
    }

    protected void btnDeleteQual_Click(object sender, EventArgs e) {
        if (lbQualifiers.SelectedIndex < 0) {
            lblEditQualError.Text = "Select a qualifier to remove";
            return;
        }
        lblErrorAdd.Text = "";
        study.Qualifiers.RemoveAt(lbQualifiers.SelectedIndex);
        Session["study"] = study;
        lbQualifiers.Items.RemoveAt(lbQualifiers.SelectedIndex);
    }

    protected void btnAddQual_Click(object sender, EventArgs e) {
        Qualifier currQual = null;
        Researcher res = (Researcher)Session["User"];
        
        // get the current Qual object
        for (int i = 0; i < existingQualList.Count; i++) {
            if (lbPreDefinedQuals.SelectedValue.Equals(existingQualList[i].Question)) {
                currQual = existingQualList[i];
                //add the qualifier to our study
                study.Qualifiers.Add(currQual);
                //select the qualifier we just added
                Session["qualEditIndex"] = study.Qualifiers.Count-1;
                break;
            }
        }
        // if the user is the creator of the qualifier allow editing. Else disable editing.
        if (res.UserID == currQual.ResID) {
            pnlNewQuals.Enabled = true;
        }
        else {
            pnlNewQuals.Enabled = false;
        }

        // populate the fields
        tbQuestion.Text = currQual.Question;
        tbQualDesc.Text = currQual.Description;
        lbAnswerList.Items.Clear();
        foreach (Answer ans in currQual.Answers) {
            lbAnswerList.Items.Add(ans.AnswerText + " [" + ans.Score.ToString() + "]");
        }
    }

    protected void btnRemoveQual_Click(object sender, EventArgs e) {
        tbQuestion.Text = string.Empty;
        tbQualDesc.Text = string.Empty;
        lbAnswerList.Items.Clear();
        pnlNewQuals.Enabled = true;
    }
}