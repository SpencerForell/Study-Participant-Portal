using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParticipantInfo : System.Web.UI.Page {
    
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            Study study = new Study(Convert.ToInt32(Request.QueryString["study_id"]));
            Participant participant = new Participant(Convert.ToInt32(Request.QueryString["participant_id"]));
            lblUserName2.Text = participant.UserName;
            lblFullName2.Text = participant.FirstName + " " + participant.LastName;
            lblEmail2.Text = participant.Email;
            showStudyInfo(study, participant);

        }
    }

    /// <summary>
    /// Shows questions/answers for all existing studies
    /// </summary>
    private void showAllInfo(Participant participant) {
        List<Study> studies = DAL.GetStudies();
        foreach (Study study in studies) {
            showStudyInfo(study, participant);
        }
    }

    /// <summary>
    /// Shows questions/answers and score for a particular study
    /// </summary>
    /// <param name="study"></param>
    private void showStudyInfo(Study study, Participant participant) {
        foreach (Qualifier qualifier in study.Qualifiers) {
            Label lblQualifier = new Label();
            pnlQualifiers.Controls.Add(lblQualifier);
            RadioButtonList rblistAnswers = new RadioButtonList();
            foreach (Answer answer in qualifier.Answers) {
                rblistAnswers.Items.Add(answer.AnswerText);
                foreach (Answer participantAnswer in participant.Answers) {
                    if (answer.AnsID == participantAnswer.AnsID) {
                        rblistAnswers.SelectedValue = answer.AnswerText;
                    }
                }
            }
            pnlQualifiers.Controls.Add(rblistAnswers);
            lblQualifier.Text = qualifier.Question;
            rblistAnswers.Enabled = false;
        }        
    }

    /// <summary>
    /// Shows all of the questions/answers for the user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowAll_Click(object sender, EventArgs e) {
        Participant participant = new Participant(Convert.ToInt32(Request.QueryString["participant_id"]));
        pnlQualifiers.Controls.Clear();
        showAllInfo(participant);
    }

    /// <summary>
    /// Shows only the quuestions and answers for the current study being viewed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShowThisStudy_Click(object sender, EventArgs e) {
        Study study = new Study(Convert.ToInt32(Request.QueryString["study_id"]));
        Participant participant = new Participant(Convert.ToInt32(Request.QueryString["participant_id"]));
        pnlQualifiers.Controls.Clear();
        showStudyInfo(study, participant);
    }
}