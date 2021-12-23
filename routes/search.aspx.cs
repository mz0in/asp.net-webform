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
                /*
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlCommand command = new SqlCommand("select count(*) from Clothes where name like %@search% OR overview like %@search%", conn);
                    command.Parameters.AddWithValue("@search", search_input);

                    // If count is more then 0, then there are some search results
                    count = Convert.ToInt32(command.ExecuteScalar().ToString());
                    if (count > 0) {
                        //SqlCommand command = new SqlCommand(strCommandText, myConnect);
                        command.CommandText = "SELECT * FROM [Clothes] WHERE name LIKE @search OR overview LIKE @search";

                        command.ExecuteNonQuery();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        Repeater1.DataSource = ds;
                        Repeater1.DataBind();

                        Label_NoResultsFound.Text = "a";
                        //Label_NoResultsFound.CssClass = "h3";
                    } else {
                        // No results
                        //Label_NoResultsFound.Text = "No Results Found";
                        //Label_NoResultsFound.CssClass = "h3";
                    }

                }*/
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
                Response.Redirect(ResolveClientUrl("index.aspx"));
            }            
        }
    }
}