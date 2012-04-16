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

        }
        generateQualifiers(study);
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
        int studyID = Convert.ToInt32(Request.QueryString["study_id"]);
        Matchmaker matchmaker = new Matchmaker(new Study(studyID));
        Table tblResults = new Table();

        foreach (KeyValuePair<Participant, int> result in matchmaker.Results) {
            TableRow row = new TableRow();
            TableCell cellID = new TableCell();
            TableCell cellName = new TableCell();
            TableCell cellEmail = new TableCell();
            TableCell cellScore = new TableCell();

            cellID.Text = result.Key.UserID.ToString();
            cellID.Visible = false;
            cellName.Text = result.Key.FirstName + " " + result.Key.LastName;
            cellEmail.Text = result.Key.Email + " " + result.Key.LastName;
            cellScore.Text = result.Value.ToString();

            row.Cells.Add(cellID);
            row.Cells.Add(cellName);
            row.Cells.Add(cellEmail);
            row.Cells.Add(cellScore);
            tblResults.Rows.Add(row);
            pnlmatchmakingResults.Controls.Add(tblResults);
        }
    }
}