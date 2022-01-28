using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace AWAD_Assignment.routes
{
    public partial class search : BasePage {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != "" && Request.QueryString["q"] != null) {
                string search_input = Server.HtmlDecode(Request.QueryString["q"]);

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                conn.Open();
                string checksearch = "SELECT COUNT(*) FROM [Clothes] WHERE name LIKE @search OR overview LIKE @search";
                SqlCommand command = new SqlCommand(checksearch, conn);

                //declare @search
                command.Parameters.AddWithValue("@search", "%" + search_input + "%");
                //command.Parameters["@search"].Value = "%" + search_input + "%";

                //use temp to create a fucntion
                int temp = Convert.ToInt32(command.ExecuteScalar().ToString());
                    
                //close the connection
                //conn.Close();

                if (temp > 0) {
                    //SqlCommand command = new SqlCommand(strCommandText, myConnect);
                    command.CommandText = "SELECT * FROM [Clothes] WHERE name LIKE @search OR overview LIKE @search";

                    //open the connection
                    //conn.Open();

                    SqlDataReader dataTable = command.ExecuteReader();
                    Repeater1.DataSource = dataTable;
                    Repeater1.DataBind();


                    //close the connection
                    conn.Close();

                    // Remove No results found
                    Label_NoResultsFound.Visible = false;
                    Label_NoResultsFound.Text = "";
                    Debug.WriteLine("--> " + Label_NoResultsFound.Text);
                }
                //*/

            } else {
                //Return absolute path with params "q"
                Response.Redirect("home");
            }            
        }
    }
}