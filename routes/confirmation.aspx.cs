using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

namespace AWAD_Assignment.routes
{
    public partial class confirmation : BasePage {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            DateTime dt = DateTime.Now;
            Label_OrderNumber.Text = rand.Next(1000, 9999).ToString();
            Label_Date.Text = dt.ToString("MMM dd, yyyy");

            string[] shippAddr = (string[])Session["shipping"];
            try {
                Label_street.Text = shippAddr[0];
                Label_Zipcode.Text = shippAddr[1];
            } catch (NullReferenceException) {
                Response.Redirect("home");
            }

            DataSet dataset = GetCartItems();
            if (dataset == null) return;

            Repeater1.DataSource = dataset;
            Repeater1.DataBind();

            // Clear shopping cart & shipping session 
            Session.Remove("shipping");
            Session.Remove("cart");

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
            Label_Subtotal.Text = string.Format("${0:00.00}", subtotal);

            //Label_total.Text = string.Format("${0:00.00}", (subtotal + (subtotal / 10)));
            Label_total.Text = string.Format("SGD ${0:00.00}", subtotal + 50);

            // Add to Sales Table in database
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                conn.Open();
                string checkuser = "SELECT COUNT(*) FROM Sales WHERE paymentdate = @today";
                SqlCommand command = new SqlCommand(checkuser, conn);
                command.Parameters.AddWithValue("@today", DateTime.Now.Date);

                int temp = Convert.ToInt32(command.ExecuteScalar().ToString());
                if (temp == 1) {
                    // update data
                    command.CommandText = $"update sales set amount=amount+{subtotal+50} where paymentdate=@today";
                    command.ExecuteNonQuery();
                } else {
                    // create new record
                    command.CommandText = $"insert into sales (id, paymentdate, amount) values ('{Guid.NewGuid().ToString()}', @today, {subtotal + 50})";
                    command.ExecuteNonQuery();
                }
            }

            // Add one for categories Table in database
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                conn.Open();
                foreach (KeyValuePair<string, Cart> kvp in carts) {

                    string query = "update Category set count=1 where id = " +
                        "(Select Category.Id from Clothes inner join Category on Clothes.category_id = Category.Id " +
                        $"where Clothes.Id = '{kvp.Value.clothes_id}')";

                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                }
                return ds;
            }
        }
    }
}