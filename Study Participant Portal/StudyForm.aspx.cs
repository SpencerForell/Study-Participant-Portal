using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudyForm : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        int study_id = Convert.ToInt32(Request.QueryString["study_id"]);
        Study study = new Study(study_id);

        Researcher res = new Researcher(study.ResearcherID);

        lblStdName.Text = study.Name;
        lblStdCreator.Text = res.FirstName + " " + res.LastName;
        lblStdDate.Text = study.DateCreated.ToString();
        tbStdDescription.Text = study.Description;
        tbStdDescription.ReadOnly = true;
        
    }
}