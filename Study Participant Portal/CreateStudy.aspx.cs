using System;
using System.Collections.Generic;
using System.Text;
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

        string Res_ID = Session["ResID"].ToString();
        StringBuilder queryString = new StringBuilder("insert into Study");
        queryString.Append(" (Name, Description, Creation_date, Expired, Res_ID)");
        queryString.Append(" values ");
        queryString.Append(" ('" + tbTitle.Text + "', '" + tbDescription.Text + "', ");
        queryString.Append("NOW()" + ", 0, " + Res_ID + ")");

        DatabaseQuery query = new DatabaseQuery(queryString.ToString(), DatabaseQuery.Type.Insert);
    }
}