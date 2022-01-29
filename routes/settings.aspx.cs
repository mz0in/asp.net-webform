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
using System.Drawing;
using QRCoder;
using System.IO;
using OtpNet;

namespace AWAD_Assignment.routes {
    public partial class settings : BasePage {
        protected void Page_Load(object sender, EventArgs e) {

            // If user is not logged in, redirect to login.aspx
            if (Session["email"] == null) Response.Redirect("login");

            if (!IsPostBack) {
                var account = Account.GetAccount(Session["email"].ToString());

                TextBox_Firstname.Text = account.firstname;
                TextBox_Lastname.Text = account.lastname;
                TextBox_MobileNumber.Text = account.mobilenumber;

                TextBox_CurrentPassword.Text = "";
                TextBox_NewPassword.Text = "";

                TextBox_Address1.Text = account.adress1;
                TextBox_Address2.Text = account.adress2;
                TextBox_Zipcode.Text = account.zipcode;

                // Set OTP button text
                if (account.mfaEnabled) Label_mfa.Text = "Current 2FA status: Enabled";
                else Label_mfa.Text = "Current 2FA status: Disabled";
            }
        }

        protected void Button_UpdateAccount_Click(object sender, EventArgs e) {
            // Update user Information
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("update Accounts set first_name = @fn, last_name = @ln, mobile_number = @mn, address1 = @ad1, address2 = @ad2, zipcode = @zc where email = @email", conn);
            command.Parameters.AddWithValue("@fn", TextBox_Firstname.Text);
            command.Parameters.AddWithValue("@ln", TextBox_Lastname.Text);
            command.Parameters.AddWithValue("@mn", TextBox_MobileNumber.Text);
            command.Parameters.AddWithValue("@ad1", TextBox_Address1.Text);
            command.Parameters.AddWithValue("@ad2", TextBox_Address2.Text);
            command.Parameters.AddWithValue("@zc", TextBox_Zipcode.Text);
            command.Parameters.AddWithValue("@email", Session["email"].ToString());
            command.ExecuteNonQuery();

            // Update password if password field is not empty
            if (TextBox_CurrentPassword.Text.Trim() != "" && TextBox_NewPassword.Text.Trim() != "") {
                var account = Account.GetAccount(Session["email"].ToString()); // Getting current user password
                if (Hash.VerifyHash(TextBox_CurrentPassword.Text, "SHA512", account.password)) {
                    // If password match, replace old password with new password
                    command.CommandText = "update Accounts set password = @pw where email = @email";
                    command.Parameters.AddWithValue("@pw", Hash.ComputeHash(TextBox_NewPassword.Text, "SHA512", null));
                    command.ExecuteNonQuery();
                }
            }

            conn.Close();
        }

        protected void LinkButton_mfa_Click(object sender, EventArgs e) {

            if (Account.GetAccount(Session["email"].ToString()).mfaEnabled) {
                // Disable MFA
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    connection.Open();
                    SqlCommand sql = new SqlCommand("update accounts set multi_factor_enabled=0, secret_key=null where id=@id", connection);
                    sql.Parameters.AddWithValue("@id", Account.GetAccount(Session["email"].ToString()).id);
                    sql.ExecuteNonQuery();
                }
                Label_mfa.Text = "Current 2FA status: Disabled";
            } else {
                // Enable MFA
                string secret = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"otpauth://totp/Estore?secret={secret}", QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image
                {
                    Height = 150,
                    Width = 150
                };
                using (Bitmap bitMap = qrCode.GetGraphic(20)) {
                    using (MemoryStream ms = new MemoryStream()) {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                    PlaceHolder_QRCode.Controls.Add(imgBarCode);
                }
                // Saving Secret to database
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    connection.Open();
                    SqlCommand sql = new SqlCommand("update accounts set multi_factor_enabled=1, secret_key=@secret where id=@id", connection);
                    sql.Parameters.AddWithValue("@secret", secret);
                    sql.Parameters.AddWithValue("@id", Account.GetAccount(Session["email"].ToString()).id);
                    sql.ExecuteNonQuery();
                }
                Label_mfa.Text = "Current 2FA status: Enabled";
            }
        }
    }
}