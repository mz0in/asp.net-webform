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
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace AWAD_Assignment.routes {
    public partial class forget_password : BasePage {
        protected void Page_Load(object sender, EventArgs e) {}
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
                // Send a password token reset
                //SendEmail().Wait();
                var user = Account.GetAccount(TextBox_Email.Text);
                SendEmail(user.email, string.Format("{0} {1}", user.firstname, user.lastname));


            } else {
                Thread.Sleep(500);
            }
            //Label_Status.Text = RandomNumberGenerator.Create().ToString();
            Label_Status.Text = "A password reset request has been sent if the email exists";
            conn.Close();
        }
        private void SendEmail(string email, string name="Customer") {

            SecretKeys api_keys = null; // https://www.delftstack.com/howto/csharp/read-json-file-in-csharp/
            using (StreamReader reader = new StreamReader(Server.MapPath("./apikeys.json"))) {
                string jsonString = reader.ReadToEnd();
                api_keys = JsonConvert.DeserializeObject<SecretKeys>(jsonString);
            }
            // Create an encrypted token for reset password
            var encryptedToken = Function.EncryptEmailToken(Function.CreateEmailToken(Encoding.ASCII.GetBytes(email))); // Use uuid4 for token?

            var client = new SendGridClient(api_keys.sendgrid_api_key); // Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(api_keys.sendgrid_email, "Support"));
            msg.AddTo(new EmailAddress(email, name));
            msg.SetTemplateId("d-5a8d6e8cea8c4e949c0f8b0df5290fcf");
            var dynamicTemplateData = new ForgetPasswordTemplateData { resetlink = $"http://localhost:62828/routes/change_password.aspx?token={encryptedToken}" };
            msg.SetTemplateData(dynamicTemplateData);

            var response = client.SendEmailAsync(msg);
            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(response.Headers.ToString());
            //Console.WriteLine("\n\nPress any key to exit.");
        }
        private static string EncryptEmailToken(string textToEncrypt) {
            try {
                //string  = "WaterWorld";
                string ToReturn = "";
                string publickey = "12345678";
                string secretkey = "87654321";
                byte[] secretkeyByte = { };
                secretkeyByte = Encoding.UTF8.GetBytes(secretkey);
                byte[] publickeybyte = { };
                publickeybyte = Encoding.UTF8.GetBytes(publickey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                byte[] inputbyteArray = Encoding.UTF8.GetBytes(textToEncrypt);
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider()) {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                    cs.Write(inputbyteArray, 0, inputbyteArray.Length);
                    cs.FlushFinalBlock();
                    ToReturn = Convert.ToBase64String(ms.ToArray());
                }
                return ToReturn;
            } catch (Exception ex) {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        private static string CreateEmailToken(byte[] arg) {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = arg ?? Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());

            return token;
        }
    }
}