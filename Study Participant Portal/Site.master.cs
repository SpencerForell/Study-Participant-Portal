using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage {

    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Session["user"] == null) {
                linkbtnLogout.Visible = false;
            }
            else {
                linkbtnLogout.Visible = true;
            }
        }
    }

    /// <summary>
    /// Button to logout. All session variables will be cleared
    /// and user will be forwarded to the default screen.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogout_Click(object sender, EventArgs e) {
        Session.Clear();
        Response.Redirect("Default.aspx");
    }
}
