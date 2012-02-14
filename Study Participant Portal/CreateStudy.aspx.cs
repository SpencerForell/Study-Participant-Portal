using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateStudy : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void BtnStdCancel_Click(object sender, EventArgs e) {
        Response.Redirect("Researcher.aspx");
    }
    protected void BtnStdSubmit_Click(object sender, EventArgs e) {
        if (tbTitle.Text.Equals(string.Empty) || tbDescription.Text.Equals(string.Empty)) {
            lblError.Text = "Please fill out the necassary fields.";
            
        }

        //SQL insert statements will be here.
    }
}