using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AWAD_Assignment.routes {
    public partial class admin_charts : BasePage {
        protected void Page_Load(object sender, EventArgs e) {

            // If admin is not logged in, give 404
            if (Session["email"] == null) Response.Redirect(ResolveClientUrl("../404"));
            if (!Account.GetAccount(Session["email"].ToString()).isAdmin) Response.Redirect(ResolveClientUrl("../404"));

            // Display charts

        }
    }
}