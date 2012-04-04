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

        tbName.Text = study.Name;
        tbDescription.Text = study.Description;

        Panel panel = new Panel();

        for (int i = 0; i < qualifiers.Count; i++) {
            qual = qualifiers[i];
            panel.Controls.Add(CreateQuestionAnswer(qual));
        }
    }

    private Panel CreateQuestionAnswer(Qualifier question) {
        Panel panel = new Panel();
        Label lblQuest = new Label();
        Label lblAns = null;
        CheckBox cbAns = null;
        lblQuest.Text = question.Question;
        panel.Controls.Add(lblQuest);

        for (int i = 0; i < question.Answers.Count; i++) {
            lblAns = new Label();
            cbAns = new CheckBox();
            lblAns.Text = question.Answers[i].AnswerText;
            panel.Controls.Add(lblAns);
            panel.Controls.Add(cbAns);
        }

        return panel;
    }
}