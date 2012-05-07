using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParticipantForm: System.Web.UI.Page {

    // Class Variables
    int qualCount;
    Qualifier qual = null;
    List<Qualifier> qualifiers = new List<Qualifier>();
    
    /// <summary>
    /// This method does quite a lot. First it populates the Study List box with all of the studies.
    /// Then it populates a qualifier panel with all of the qualifiers that the participant has
    /// not answered yet. It dynamically constructs each qualifier answer.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e) {
        qualCount = 0;
        bool skipFlag = false;
        bool option = false;
        Participant part = (Participant)Session["User"];
        List<int> ansIDs = DAL.GetParticipantAnswers(part.UserID);

        if (!IsPostBack) {
            // Populate the list box
            populateListbox(part.UserID);
        }

        // Populate the qualifier list
        qualifiers = DAL.GetQualifiers(part.UserID);

        // Populate the Qualifier Panel
        for (int i = 0; i < qualifiers.Count; i++) {
            skipFlag = false;
            qual = qualifiers[i];

            // Logic to filter out previously answered questions
            foreach (int id in ansIDs) {
                foreach (Answer ans in qual.Answers) {
                    if (id == ans.AnsID) {
                        skipFlag = true;
                        qualCount++;
                        break;
                    }
                }
                if (skipFlag == true) {
                    break;
                }
            }

            if (skipFlag == true) {
                continue;
            }

            // create each individual question and answer
            pnlQualList.Controls.Add(CreateQuestionAnswer(qual));
            pnlQualList.Controls.Add(new LiteralControl("<br />"));
            option = true;
            
        }

        // Don't show the option panel if there aren't new qualifiers
        if (option == false) {
            pnlCrossroad.Visible = false;
            pnlQualList.Visible = false;
            pnlStudyList.Visible = true;
        }


    }

    /// <summary>
    /// Dynamically create the question and answer.
    /// </summary>
    /// <param name="question"></param>
    /// <returns></returns>
    private Panel CreateQuestionAnswer(Qualifier question) {
        Panel panel = new Panel();
        Label lblQuest = new Label();
        RadioButtonList rbList = new RadioButtonList();
        ListItem li = null;

        
        rbList.Font.Size = 12;
        lblQuest.Font.Size = 14;
        lblQuest.Text = question.Question;
        panel.Controls.Add(lblQuest);
        panel.Controls.Add(new LiteralControl("<br />"));

        for (int i = 0; i < question.Answers.Count; i++) {
            li = new ListItem();
            li.Text = question.Answers[i].AnswerText;
            rbList.Items.Add(li);
        }
        
        panel.Controls.Add(rbList);

        return panel;
    }

    /// <summary>
    /// Populate the study list box
    /// </summary>
    /// <param name="user_id"></param>
    private void populateListbox(int user_id) {
        lboxStudyList.Items.Clear();
        string queryString = "Select Study_ID, Name from Study order by Name";
        DatabaseQuery query = new DatabaseQuery(queryString, DatabaseQuery.Type.Select);
        int resultNum = 0;
        while (query.Results.Count > resultNum) {
            Study study = new Study(Convert.ToInt32(query.Results[resultNum][0]));
            ListItem item = new ListItem(study.Name, study.StudyID.ToString());
            lboxStudyList.Items.Add(item);
            resultNum++;
        }
    }

    /// <summary>
    /// Button to view the Study information from the Study List box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnView_Click(object sender, EventArgs e) {
        if (lboxStudyList.SelectedIndex < 0) {
            lblError.Text = "Please select a study to view.";
        }
        else {
            int studyID = Convert.ToInt32(lboxStudyList.SelectedValue);
            Response.Redirect("ParticipantStudy.aspx?study_id=" + studyID);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnParEdit_Click(object sender, EventArgs e) {
        Response.Redirect("CreateAccount.aspx?user=Participant&edit=true");
    }

    /// <summary>
    /// Click this button to Answer all questions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGoQuestions_Click(object sender, EventArgs e) {
        pnlCrossroad.Visible = false;
        pnlStudyList.Visible = false;
        pnlQualList.Visible = true;
        btnSubmitQuestions.Visible = true;
    }

    /// <summary>
    /// click this button to go the the study list.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGoStudies_Click(object sender, EventArgs e) {
        pnlCrossroad.Visible = false;
        pnlQualList.Visible = false;
        pnlStudyList.Visible = true;
        btnSubmitQuestions.Visible = false;
    }

    /// <summary>
    /// This is a helper method to load the Submitted answers into the database.
    /// </summary>
    /// <param name="answers"></param>
    private void loadAnswers(List<string> answers) {
        bool skipFlag = true;
        if (lblAnswerError.Visible == true) {
            lblAnswerError.Visible = false;
        }

        int partID = ((Participant)Session["user"]).UserID;
        List<int> ids = new List<int>();
        int index = 0;

        foreach (Qualifier qual in qualifiers) {
            skipFlag = true;
            foreach (Answer ans in qual.Answers) {
                if (answers[index].Equals(ans.AnswerText)) {
                    ids.Add(ans.AnsID);
                    skipFlag = false;
                    index++;
                    break;
                }
            }
            if (skipFlag == true) {
                index++;
            }
        }

        foreach (int id in ids) {
            DAL.InsertParticipantAnswer(partID, id);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmitQuestions_Click(object sender, EventArgs e) {
        bool noSelectionFlag = true;
        List<string> answers = new List<string>();
        foreach (Control control in pnlQualList.Controls) {
            foreach (Control item in control.Controls) {
                if (item is RadioButtonList) {
                    string answer = ((RadioButtonList)item).SelectedValue.ToString();

                    if (!answer.Equals(string.Empty)) {
                        answers.Add(answer);
                    }
                    else {
                        answers.Add(string.Empty);
                    }
                    
                }
            }
        }
        foreach (string answer in answers) {
            if (!answer.Equals(string.Empty)) {
                noSelectionFlag = false;
            }
        }
        if (noSelectionFlag == true) {
            lblNoSelection.Visible = true;
            return;
        }
        loadAnswers(answers);
        btnSubmitQuestions.Visible = false;
        pnlQualList.Visible = false;
        pnlCrossroad.Visible = false;
        pnlStudyList.Visible = false;
        pnlConfirmation.Visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConfirm_Click(object sender, EventArgs e) {
        pnlConfirmation.Visible = false;
        pnlCrossroad.Visible = false;
        pnlQualList.Visible = false;
        pnlStudyList.Visible = true;
    }
}