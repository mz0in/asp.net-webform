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
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace AWAD_Assignment.routes
{
    public partial class register : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
        }
        protected void btnRegister_Click(object sender, EventArgs e) {
            RegisterAccount();
        }
        private void RegisterAccount() {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            conn.Open();

            bool exists = false;

            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [accounts] WHERE email = @email", conn)) {
                //checks if the email that the user has entered exists in the database table
                cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);
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

                string insertQuery = "INSERT INTO accounts (Id, first_name, last_name, email, emailConfirmed, isAdmin, password, mobile_number, multi_factor_enabled, secret_key, address1, address2, zipcode) " +
                    "values (@id, @first, @last, @email, @emailConfirmed, @admin, @password, @mobile, @multi_factor_enabled, @secret_key, @address1, @address2, @zipcode)";

                SqlCommand com = new SqlCommand(insertQuery, conn);

                com.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                com.Parameters.AddWithValue("@first", TextBox_FirstName.Text);
                com.Parameters.AddWithValue("@last", TextBox_LastName.Text);
                com.Parameters.AddWithValue("@email", TextBox_Email.Text);
                com.Parameters.AddWithValue("@emailConfirmed", false);
                com.Parameters.AddWithValue("@admin", false);
                com.Parameters.AddWithValue("@password", Hash.ComputeHash(TextBox_Password.Text, "SHA512", null));
                com.Parameters.AddWithValue("@mobile", TextBox_MobileNumber.Text);
                com.Parameters.AddWithValue("@multi_factor_enabled", false);
                com.Parameters.AddWithValue("@secret_key", DBNull.Value);
                com.Parameters.AddWithValue("@address1", DBNull.Value);
                com.Parameters.AddWithValue("@address2", DBNull.Value);
                com.Parameters.AddWithValue("@zipcode", DBNull.Value);

                com.ExecuteNonQuery();

                // User should verify email first before login  ~~login newly created account~~
                Label_EmailExists.Text = "An email has been sent to your email address to verify your account";

                SecretKeys api_keys = null; // https://www.delftstack.com/howto/csharp/read-json-file-in-csharp/
                using (StreamReader reader = new StreamReader(Server.MapPath("./apikeys.json"))) {
                    string jsonString = reader.ReadToEnd();
                    api_keys = JsonConvert.DeserializeObject<SecretKeys>(jsonString);
                }

                // Get user geo location
                WebClient webClient = new WebClient();
                string ipaddress = webClient.DownloadString("https://api.ipify.org");
                string url = $"https://geo.ipify.org/api/v1?apiKey={api_keys.ipify}&ipAddress={ipaddress}";
                string resultData = string.Empty;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                using (HttpWebResponse response = (HttpWebResponse)req.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream)) {
                    resultData = reader.ReadToEnd();
                }
                Dictionary<dynamic, dynamic> ipify = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(resultData);
                string countryISO = ipify["location"]["country"];
                // Save Country to database
                com.CommandText = $"SELECT COUNT(*) FROM Accounts WHERE Email = '{countryISO}'";
                int countryexists = Convert.ToInt32(com.ExecuteScalar().ToString());
                // If country exists update count else create new row
                if (countryexists == 1) {
                    com.CommandText = $"update Countries set count=count+1 where country='{countryISO}'";
                    com.ExecuteNonQuery();
                } else {
                    com.CommandText = $"INSERT INTO countries (Id, country, count) values ('{Guid.NewGuid().ToString()}', '{countryISO}', 1)";
                    com.ExecuteNonQuery();
                }
            }

            conn.Close();

            // Send verification Email to user
            SendEmail(TextBox_Email.Text, string.Format("{0} {1}", TextBox_FirstName.Text, TextBox_LastName.Text));

            //txt_FirstName.Text = "";
            //txt_LastName.Text = "";
            //txt_RegEmail.Text = "";
        }
        private void SendEmail(string email, string name = "Customer") {

            SecretKeys api_keys = null; // https://www.delftstack.com/howto/csharp/read-json-file-in-csharp/
            using (StreamReader reader = new StreamReader(Server.MapPath("./apikeys.json"))) {
                string jsonString = reader.ReadToEnd();
                api_keys = JsonConvert.DeserializeObject<SecretKeys>(jsonString);
            }
            // Create an encrypted token for verification email
            var encryptedToken = Function.EncryptEmailToken(Function.CreateEmailToken(Encoding.ASCII.GetBytes(email))); // Use uuid4 for token?

            var client = new SendGridClient(api_keys.sendgrid_api_key); // Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(api_keys.sendgrid_email, "Support"));
            msg.AddTo(new EmailAddress(email, name));
            msg.SetTemplateId("d-73e6f5f3fefa4f5fa51fac3a321d40ae");
            var dynamicTemplateData = new VerificationEmailTemplateData { verifylink = $"http://localhost:62828/routes/verify_email.aspx?token={encryptedToken}", name = name };
            msg.SetTemplateData(dynamicTemplateData);

            var response = client.SendEmailAsync(msg);
            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(response.Headers.ToString());
            //Console.WriteLine("\n\nPress any key to exit.");
        }
    }
}