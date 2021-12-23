using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AWAD_Assignment.routes
{
    public partial class checkout : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            DataSet dataset = GetCartItems();
            if (dataset == null) return;

            Repeater1.DataSource = dataset;
            Repeater1.DataBind();
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
                dr.SetField("price", kvp.Value.item_price);

                subtotal += kvp.Value.item_price * kvp.Value.item_quantity;

            }
            // Session["subtotal"] = subtotal;
            Label_subtotal.Text = string.Format("${0:00.00}", subtotal.ToString());

            //Label_shipping.Text = string.Format("Flat Rate: ${0:00.00}", (subtotal/10).ToString());
            Label_shipping.Text = string.Format("Flat Rate: $5.00", (subtotal/10).ToString());

            //Label_total.Text = string.Format("${0:00.00}", (subtotal + (subtotal / 10)).ToString());
            Label_total.Text = string.Format("${0:00.00}", (subtotal + 5).ToString());
            return ds;
        }
    }
}