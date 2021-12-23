using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Security.Cryptography;

namespace AWAD_Assignment.routes {
    public partial class forget_password : BasePage {
        protected void Page_Load(object sender, EventArgs e) {

        }
        protected void btnForgetPassword_Click(object sender, EventArgs e) {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            conn.Open();

            string checkuser = "SELECT COUNT(*) FROM Accounts WHERE Email = @email";
            SqlCommand com = new SqlCommand(checkuser, conn);
            com.Parameters.AddWithValue("@email", TextBox_Email.Text);

            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());

            conn.Close();

            if (temp == 1) // checks if email exists inside DB
            {
                // TODO - send a password token reset

                
                
            } else {
                //Thread.Sleep(2000);
            }
            Label_Status.Text = RandomNumberGenerator.Create().ToString();
            //Label_Status.Text = "A password reset request has been sent if the email exists";
        }
    }
}