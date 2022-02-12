using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stripe;
using Stripe.Checkout;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Salt_Password_Sample;

namespace AWAD_Assignment.routes {
    public partial class checkout : BasePage {
        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                // https://www.c-sharpcorner.com/UploadFile/2f59d0/attached-css-change-css-and-style-in-Asp-Net/             
                if (Session["email"] != null) {
                // If user is logged in, complete the form for them and hide div to login/register account
                    theReturningCustomerDiv.Visible = false;
                    createAccountDiv.Visible = false;
                    var account = Account.GetAccount(Session["email"].ToString());
                    TextBox_FirstName.Text = account.firstname;
                    TextBox_LastName.Text = account.lastname;
                    TextBox_MobileNumber.Text = account.mobilenumber;
                    TextBox_Email.Text = account.email;
                    TextBox_Address1.Text = account.adress1;
                    TextBox_Address2.Text = account.adress2;
                    TextBox_Zipcode.Text = account.zipcode;
                    TextBox_Email.ReadOnly = true; // user is not allowed to change email textbox
                }

                CheckBox_ToS.Checked = true;

                DataSet dataset = GetCartItems();
                if (dataset == null) return;
                Repeater1.DataSource = dataset;
                Repeater1.DataBind();
            }


        }
        private DataSet GetCartItems() {
            Dictionary<string, Cart> carts = (Dictionary<string, Cart>)Session["cart"];

            if (carts == null || carts.Count == 0) {
                // Cart is empty, 
                return null;
            }
            // subtotal
            double subtotal = 0;

            // List all products in cart
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(dt);
            dt.Columns.Add("name");
            dt.Columns.Add("quantity");
            dt.Columns.Add("price");

            foreach (KeyValuePair<string, Cart> kvp in carts) {
                DataRow dr = dt.Rows.Add();
                dr.SetField("name", kvp.Value.item_name);
                dr.SetField("quantity", kvp.Value.item_quantity);
                dr.SetField("price", kvp.Value.item_price * kvp.Value.item_quantity);

                subtotal += kvp.Value.item_price * kvp.Value.item_quantity;

            }
            // Session["subtotal"] = subtotal;
            Label_subtotal.Text = string.Format("${0:00.00}", subtotal);

            //Label_shipping.Text = string.Format("Flat Rate: ${0:00.00}", (subtotal/10));
            Label_shipping.Text = string.Format("Flat Rate: $50.00", subtotal / 10);

            //Label_total.Text = string.Format("${0:00.00}", (subtotal + (subtotal / 10)));
            Label_total.Text = string.Format("${0:00.00}", subtotal + 50);
            return ds;
        }

        protected void LinkButton_Payment_Click(object sender, EventArgs e) {
            if (IsPostBack) {

                // if user didn't checked ToS box alert them to check
                if (CheckBox_ToS.Checked == false) {
                    Response.Write("<script>alert('Please check Terms of Service box before proceeding');</script>");
                    return;
                }
                        

                // Saving address & zipcode if not logged in -- TODO - Fix this below, data from textbox not send over?
                if (Session["email"] == null || Session["email"].ToString().Trim() == "") Session["shipping"] = new string[2] { TextBox_Address1.Text, TextBox_Zipcode.Text };
                else {
                    // update existing shipping address with new address
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("update [accounts] set address1=@addr1, address2=@addr2, zipcode=@zipcode WHERE email = @email", conn)) {
                        //checks if the email that the user has entered exists in the database table
                        command.Parameters.AddWithValue("email", TextBox_Email.Text);
                        command.Parameters.AddWithValue("@addr1", TextBox_Address1.Text);
                        command.Parameters.AddWithValue("@addr2", TextBox_Address2.Text);
                        command.Parameters.AddWithValue("zipcode", TextBox_Zipcode.Text);
                        command.ExecuteNonQuery();
                        // Create session for confirmation page
                        Session["shipping"] = new string[2] { TextBox_Address1.Text, TextBox_Zipcode.Text };
                    }
                    conn.Close();
                }

                // Register Account if CreateAccountbox is checked and email entered is not in DB
                if (Checkbox_AccountCreate.Checked && Account.GetAccount(TextBox_Email.Text) == null) {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                        sqlConnection.Open();
                        string query = "INSERT INTO accounts (Id, first_name, last_name, email, emailConfirmed, isAdmin, password, mobile_number, multi_factor_enabled, secret_key, address1, address2, zipcode) values (@id, @first, @last, @email, @emailConfirmed, @admin, @password, @mobile, @multi_factor_enabled, @secret_key, @address1, @address2, @zipcode)";
                        SqlCommand com = new SqlCommand(query, sqlConnection);
                        com.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                        com.Parameters.AddWithValue("@first", TextBox_FirstName.Text);
                        com.Parameters.AddWithValue("@last", TextBox_LastName.Text);
                        com.Parameters.AddWithValue("@email", TextBox_Email.Text);
                        com.Parameters.AddWithValue("@emailConfirmed", false);
                        com.Parameters.AddWithValue("@admin", false);
                        com.Parameters.AddWithValue("@password", Hash.ComputeHash(passwordCreate.Value, "SHA512", null));
                        com.Parameters.AddWithValue("@mobile", TextBox_MobileNumber.Text);
                        com.Parameters.AddWithValue("@multi_factor_enabled", false);
                        com.Parameters.AddWithValue("@secret_key", DBNull.Value);
                        com.Parameters.AddWithValue("@address1", TextBox_Address1.Text);
                        com.Parameters.AddWithValue("@address2", TextBox_Address2.Text);
                        com.Parameters.AddWithValue("@zipcode", TextBox_Zipcode.Text);
                        com.ExecuteNonQuery();
                    }
                }

                // Stripe payment stuff
                Dictionary<string, Cart> carts = (Dictionary<string, Cart>)Session["cart"];

                SecretKeys api_keys = null; // https://www.delftstack.com/howto/csharp/read-json-file-in-csharp/
                using (StreamReader reader = new StreamReader(Server.MapPath("./apikeys.json"))) {
                    string jsonString = reader.ReadToEnd();
                    api_keys = JsonConvert.DeserializeObject<SecretKeys>(jsonString);                    
                }
                StripeConfiguration.ApiKey = api_keys.stripe_api_key;
                string domain = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority); // https://stackoverflow.com/questions/26189953/how-to-get-current-domain-name-in-asp-net


                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                    { },
                    Mode = "payment",
                    SuccessUrl = domain + "/confirmation",
                    CancelUrl = domain + "/checkout",
                    PaymentMethodTypes = new List<string> { "card", },
                    // https://stripe.com/docs/payments/checkout/shipping
                    //ShippingAddressCollection = new SessionShippingAddressCollectionOptions { AllowedCountries = new List<string> { "SG" } }, // I want to use my own shipping address form
                    ShippingOptions = new List<SessionShippingOptionOptions>
                {
                    new SessionShippingOptionOptions
                    {
                        ShippingRateData = new SessionShippingOptionShippingRateDataOptions
                        {
                            DisplayName = "Universal Shipping",
                            Type = "fixed_amount",
                            FixedAmount = new SessionShippingOptionShippingRateDataFixedAmountOptions {Amount=5000, Currency = "sgd"},
                            DeliveryEstimate = new SessionShippingOptionShippingRateDataDeliveryEstimateOptions
                            {
                                Minimum = new SessionShippingOptionShippingRateDataDeliveryEstimateMinimumOptions { Unit="day", Value=5 },
                                Maximum = new SessionShippingOptionShippingRateDataDeliveryEstimateMaximumOptions { Unit="day", Value=7 }
                            }
                        }
                    }
                },
                };

                foreach (KeyValuePair<string, Cart> kvp in carts) {
                    options.LineItems.Add(new SessionLineItemOptions
                    {
                        //Name = kvp.Value.item_name,
                        //Amount = int.Parse(string.Format("{0:00.00}", kvp.Value.item_price).Replace(".", "")),
                        //Currency = "sgd",
                        //Quantity = kvp.Value.item_quantity,

                        // Above method is deprecated, but I don't know what I am missing below, "Object not set to an instance" Exception 
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "sgd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = kvp.Value.item_name,
                            },
                            //UnitAmountDecimal = decimal.Parse(kvp.Value.item_price.ToString()) * decimal.Parse(kvp.Value.item_quantity.ToString()),
                            UnitAmountDecimal = int.Parse(string.Format("{0:00.00}", kvp.Value.item_price).Replace(".", "")),  // (decimal?)kvp.Value.item_price,
                        },
                        Quantity = kvp.Value.item_quantity,

                    });
                }
                // to catch error when user clicks payment button but has no items in cart
                try {
                    var service = new SessionService();
                    Session session = service.Create(options);

                    // https://stackoverflow.com/questions/9497467/how-to-create-303-response-in-asp-net
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Status = "303 See Other";
                    HttpContext.Current.Response.AddHeader("Location", session.Url);
                    HttpContext.Current.Response.End();
                } catch (StripeException) {
                    Response.Write("<script>alert('Cart is empty. Add some items first');</script>");
                }
            }
        }
    }
}