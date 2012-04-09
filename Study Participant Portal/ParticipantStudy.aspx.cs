using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParticipantStudy : System.Web.UI.Page {

    // Class variables
    Study study = null;
    Qualifier qual = null;
    List<Qualifier> qualifiers = new List<Qualifier>();

    /// <summary>
    /// This is the page load method and is called whenever the ParticipantStudy 
    /// page is loaded. If the page is a post back it sets the main panel 
    /// visibility to true. If it is not a post back, the visibility gets set to
    /// false. The method sets the study name and description appropriately, and then
    /// loads the main qualifier panel with inner panels containing the questions and
    /// answers.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e) {
        study = new Study(Convert.ToInt32(Request.QueryString["study_id"]));
        qualifiers = study.Qualifiers;

        if (!IsPostBack) {
            pnlQuals.Visible = false;
        }
        tbName.Text = study.Name;
        tbDescription.Text = study.Description;

        for (int i = 0; i < qualifiers.Count; i++) {
            qual = qualifiers[i];
            pnlQuals.Controls.Add(CreateQuestionAnswer(qual));
            pnlQuals.Controls.Add(new LiteralControl("<hr />"));
            
        }       
    }

    /// <summary>
    /// This method takes a single qualifier object as a parameter and returns a
    /// panel control. The function of this method is to create individual question
    /// and answers. It uses a RadioButtonList as the user functionality. Hence a
    /// user will only be able to select one answer per question. The panel acts 
    /// as our container for the controls. We first add the question, and then add
    /// each ListItem to the RadioButtonList. Once all the answers are in the 
    /// RadioButtonList, we add that to the wrapper panel. Once our panel is all
    /// setup we return it.
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
    /// A simple method that responds to a button click event. It simply sets the
    /// visibility to our main panel as true so our user may answer the questions.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowQuestions_Click(object sender, EventArgs e) {
        pnlQuals.Visible = true;
        btnSubmit.Visible = true;
    }

    /// <summary>
    /// This method responds the the submit button click event, for when the user
    /// has provided all of the required answers. It iterates through the nested
    /// panel container obtaining all of the selected answer values and storing 
    /// them in the answers List. It does some basic error checking to make sure
    /// the user has provided an answer for each question. It then calls the 
    /// loadAnswers method passing the list of answer strings.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e) { 
        List<string> answers = new List<string>();
        foreach (Control control in pnlQuals.Controls) {
            foreach (Control item in control.Controls) {
                if (item is RadioButtonList) {
                    string answer = ((RadioButtonList)item).SelectedValue.ToString();

                    if (answer.Equals(string.Empty)) {
                        lblError.Visible = true;
                        return;
                    }
                    answers.Add(answer);
                }
            }
        }
        loadAnswers(answers);
    }

    /// <summary>
    /// A helper method for the submit button method. This method takes in the
    /// list of answers that were selected by the user. The method then compares
    /// each answer to the actual answer object and gets the corosponding answer
    /// ID for each answer. These answer IDs along with the Participant ID are 
    /// loaded into the database table using a data layer.
    /// </summary>
    /// <param name="answers"></param>
    private void loadAnswers(List<string> answers) {
        if (lblError.Visible == true) {
            lblError.Visible = false;
        }
        int partID = ((Participant)Session["user"]).UserID;
        List<int> ids = new List<int>();
        int index = 0;

        foreach (Qualifier qual in qualifiers) {
            foreach (Answer ans in qual.Answers) {
                if (answers[index].Equals(ans.AnswerText)) {
                    ids.Add(ans.AnsID);
                    index++;
                    break;
                }
            }
        }

        foreach (int id in ids) {
            DAL.InsertParticipantAnswer(partID, id);
        }
    }
}