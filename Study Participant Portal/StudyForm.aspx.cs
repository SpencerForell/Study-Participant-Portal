using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudyForm : System.Web.UI.Page {
    Study study;
    Researcher researcher;

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            int studyID = Convert.ToInt32(Request.QueryString["study_id"]);
            study = new Study(studyID);
            researcher = new Researcher(study.ResearcherID);
            lblStdName.Text = study.Name;
            lblStdDate.Text = study.DateCreated.ToString();
            tbStdDescription.Text = study.Description;

            generateQualifiers(study);
        }
    }

    /// <summary>
    /// Dynamically creates panels that store each qualifier and adds them to the main panel of the page.
    /// </summary>
    /// <param name="study"></param>
    private void generateQualifiers(Study study) {
        foreach (Qualifier qualifier in study.Qualifiers) {
            //create a new panel that will hold all the information for this qualifier
            Panel pnlQualifer = new Panel();
            LiteralControl lineBreak = new LiteralControl("<br>");
            Label lblQualifier = new Label();
            RadioButtonList rblistAnswers = new RadioButtonList();
            foreach (Answer answer in qualifier.Answers) {
                rblistAnswers.Items.Add(answer.AnswerText);
            }
            pnlQualifer.Controls.Add(lineBreak);
            lblQualifier.Text = qualifier.Question;
            pnlQualifer.Controls.Add(lblQualifier);
            rblistAnswers.Enabled = false;
            pnlQualifer.Controls.Add(rblistAnswers);
            //add the panel we just made to the form
            pnlStdQualifiers.Controls.Add(pnlQualifer); 
        }
    }

    /// <summary>
    /// Event triggered to perform the matchmaking algorithm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFindParticipants_Click(object sender, EventArgs e) {
        
    }
}