using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Salt_Password_Sample;

namespace AWAD_Assignment.routes
{
    public partial class register : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
        }
        protected void btnRegister_Click(object sender, EventArgs e) {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            conn.Open();

            bool exists = false;

            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [accounts] WHERE email = @email", conn)) {
                //checks if the email that the user has entered exists in the database table
                cmd.Parameters.AddWithValue("Email", TextBox_Email.Text);
                exists = (int)cmd.ExecuteScalar() > 0;
            }

            //if the email exists, send an alert
            if (exists) {
                Label_EmailExists.Text = "Sorry, Email is already taken!";
                //Response.Write("<script>alert('Sorry, Email is already taken!');</script>");
            }

            //else, insert 
            else {
                Label_EmailExists.Text = "";

                string insertQuery = "INSERT INTO accounts (Id, first_name, last_name, email, isAdmin, password, mobile_number) " +
                    "values (@id, @first, @last, @email, @admin, @password, @mobile)";

                SqlCommand com = new SqlCommand(insertQuery, conn);

                com.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                com.Parameters.AddWithValue("@first", TextBox_FirstName.Text);
                com.Parameters.AddWithValue("@last", TextBox_LastName.Text);
                com.Parameters.AddWithValue("@email", TextBox_Email.Text);
                com.Parameters.AddWithValue("@admin", false);
                com.Parameters.AddWithValue("@password", Hash.ComputeHash(TextBox_Password.Text, "SHA512", null));
                com.Parameters.AddWithValue("@mobile", TextBox_MobileNumber.Text);

                com.ExecuteNonQuery();

                Response.Write("<script>alert('Successfully created account! Welcome! ');</script>");
                // TODO - login newly created account 
            }

            conn.Close();

            //txt_FirstName.Text = "";
            //txt_LastName.Text = "";
            //txt_RegEmail.Text = "";
        }
    }
}