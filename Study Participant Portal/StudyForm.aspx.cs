using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudyForm : System.Web.UI.Page {

    private Study Study {
        get { return (Study)Session["study"]; }
        set { Session["study"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            int studyID = Convert.ToInt32(Request.QueryString["study_id"]);
            Study = new Study(studyID);
            lblStdName.Text = Study.Name;
            lblStdDate.Text = Study.DateCreated.ToString();
            lblStdDescription.Text = Study.Description;
            lblEmailStatus.Visible = false;
        }
        pnlStdQualifiers.Controls.Add(generateQualifiers(Study));
    }

    /// <summary>
    /// Dynamically creates panels that store each qualifier and answer
    /// </summary>
    /// <param name="study"></param>
    public Panel generateQualifiers(Study study) {
        Panel pnlQualifer = new Panel();
        foreach (Qualifier qualifier in study.Qualifiers) {
            //create a new panel that will hold all the information for this qualifier
            
            Label lblQualifier = new Label();
            RadioButtonList rblistAnswers = new RadioButtonList();
            foreach (Answer answer in qualifier.Answers) {
                rblistAnswers.Items.Add(answer.AnswerText);
            }
            lblQualifier.Text = qualifier.Question;
            pnlQualifer.Controls.Add(lblQualifier);
            rblistAnswers.Enabled = false;
            pnlQualifer.Controls.Add(rblistAnswers);
            //add the panel we just made to the form
        }
        return pnlQualifer; 
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
        TableHeaderRow header = new TableHeaderRow();
        TableHeaderCell headerName = new TableHeaderCell();
        TableHeaderCell headerEmail = new TableHeaderCell();
        TableHeaderCell headerScore = new TableHeaderCell();
        headerName.Text = "Name";
        headerEmail.Text = "Email";
        headerScore.Text = "Score";
        header.Cells.Add(headerName);
        header.Cells.Add(headerEmail);
        header.Cells.Add(headerScore);
        tblResults.Rows.Add(header);
        tblResults.CellSpacing = 3;
        tblResults.CellPadding = 5;


        foreach (KeyValuePair<Participant, int> result in matchmaker.Results) {
            TableRow row = new TableRow();
            TableCell cellID = new TableCell();
            TableCell cellName = new TableCell();
            TableCell cellEmail = new TableCell();
            TableCell cellScore = new TableCell();

            cellID.Text = result.Key.UserID.ToString();
            cellID.Visible = false;
            HyperLink link = new HyperLink();
            link.ToolTip = "Click the link to view more information about this user";
            link.Text = result.Key.FirstName + " " + result.Key.LastName;
            link.NavigateUrl="ParticipantInfo.aspx?participant_id=" + cellID.Text + "&study_id=" + studyID;
            cellName.Controls.Add(link);
            cellEmail.Text = result.Key.Email;
            cellScore.Text = result.Value.ToString();
            
            row.Cells.Add(cellID);
            row.Cells.Add(cellName);
            row.Cells.Add(cellEmail);
            row.Cells.Add(cellScore);
            tblResults.Rows.AddAt(getIndexToAdd(tblResults, row), row);
            pnlmatchmakingResults.Controls.Add(tblResults);
        }

        pnlmatchmakingResults.Visible = true;
        btnEmailParticipant.Visible = true;

    }

    private int getIndexToAdd(Table tblResults, TableRow row) {
        int i = 1; //start at 1 because of the header
        while (i < tblResults.Rows.Count) {
            //compare the two scores to see which one is greater.

            if (Convert.ToInt32(row.Cells[3].Text) < Convert.ToInt32(tblResults.Rows[i].Cells[3].Text)) {
                //if the row to insert is less than the current row, keep going down the rows
                i++;
            }
            else {
                //if the row is >=, than insert it here;
                break;
            }
        }
        return i;
    }

    protected void btnEmailParticipant_Click(object sender, EventArgs e) {
        List<String> emails = getEmails(Study);
        btnEmailParticipant.Visible = false;
        foreach (String email in emails) {
            tbEmailList.Text = email + ",";
        }
        //only remove the last comma if there have been some emails inserted in 
        if (tbEmailList.Text.Length != 0) {
            tbEmailList.Text = tbEmailList.Text.Remove(tbEmailList.Text.Length - 1);
        }
        btnFindParticipants_Click(sender, e);
    }

    private List<string> getEmails(Study study) {
        List<String> emails = new List<string>();
        Matchmaker matchmaker = new Matchmaker(new Study(study.StudyID));
        foreach (KeyValuePair<Participant, int> result in matchmaker.Results) {
            emails.Add(result.Key.Email);
        }
        tbEmailList.Visible = true;
        lblEmailStatus.Visible = true;
        return emails;
    }
}