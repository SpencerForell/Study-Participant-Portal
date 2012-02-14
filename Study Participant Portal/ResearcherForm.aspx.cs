using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResearcherForm : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {
        Researcher res = (Researcher)Session["User"];
    }
    protected void logout_Click(object sender, EventArgs e) {
        Response.Redirect("Default.aspx");
    }

    protected void btnResCreate_Click(object sender, EventArgs e) {
        Response.Redirect("CreateStudy.aspx");
    }
}
