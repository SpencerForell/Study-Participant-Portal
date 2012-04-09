using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParticipantStudy : System.Web.UI.Page {

    Study study = null;
    Qualifier qual = null;
    List<Qualifier> qualifiers = new List<Qualifier>();

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

    protected void btnShowQuestions_Click(object sender, EventArgs e) {
        pnlQuals.Visible = true;
        btnSubmit.Visible = true;
    }

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