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
    public partial class login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnSignIn_Click(object sender, EventArgs e) {

            // Creating session cookies
            Session["Email"] = TextBox_Email.Text;
            //Session["cart"] = new Dictionary<string, Cart>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            conn.Open();

            string checkuser = "SELECT COUNT(*) FROM Accounts WHERE Email = @email";
            SqlCommand com = new SqlCommand(checkuser, conn);
            com.Parameters.AddWithValue("@email", TextBox_Email.Text);

            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());

            conn.Close();

            if (temp == 1) // checks if email exists inside DB
            {
                conn.Open();

                string checkPasswordQuery = "SELECT password FROM Accounts WHERE email = @email2";

                SqlCommand pwcomm = new SqlCommand(checkPasswordQuery, conn);
                pwcomm.Parameters.AddWithValue("@email2", TextBox_Email.Text);
                string password = pwcomm.ExecuteScalar().ToString();
                bool flag = Hash.VerifyHash(TextBox_Password.Text, "SHA512", password); // verifies password through hash function

                if (flag == true) {
                    Response.Write("<script language=javascript>alert('Login Successful')</script>");
                    return;

                    // TODO - Login implementation
                    Session["CHANGE_MASTERPAGE"] = "~/AfterLogin.Master";
                    Session["CHANGE_MASTERPAGE2"] = null;
                    Response.Redirect(Request.Url.AbsoluteUri); // Remove <-- 
                } else {
                    Label_LoginFailure.Text = "Email Adress or Password is incorrect";
                }
            } else {
                Label_LoginFailure.Text = "Email Adress or Password is incorrect";
            }

            TextBox_Email.Text = ""; //clears textbox after login
        }
    }
}