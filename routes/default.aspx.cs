using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AWAD_Assignment.routes
{
    public partial class index1 : BasePage {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                DisplayNewClothing();
                DisplayRandomClothing();
            }
        }

        private void DisplayNewClothing() {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                using (SqlCommand sql = new SqlCommand("select top 6 * from Clothes order by DateAdded desc", conn)) {
                    conn.Open();
                    SqlDataReader dataTable = sql.ExecuteReader();
                    RepeaterNew.DataSource = dataTable;
                    RepeaterNew.DataBind();
                }
            }
        }
        private void DisplayRandomClothing() {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                using (SqlCommand sql = new SqlCommand("select * from Clothes where Id in (select top 6 Id from Clothes order by newid())", conn)) {
                    conn.Open();
                    SqlDataReader dataTable = sql.ExecuteReader();
                    RepeaterRandom.DataSource = dataTable;
                    RepeaterRandom.DataBind();
                }
            }
        }
    }
}