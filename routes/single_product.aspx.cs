using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace AWAD_Assignment.routes
{
    public partial class single_product : BasePage
    {
        Clothes clothes = null;
        protected void Page_Load(object sender, EventArgs e) {
            try { 
                clothes = Clothes.getClothesID(Request.QueryString["id"].ToString());
            } catch(NullReferenceException) {
                Response.Redirect("/routes/error/404.html");
            }
            if (clothes == null) Response.Redirect("/routes/error/404.html");

            Label_Clothes_name.Text = clothes.name;
            Label_Clothes_price.Text = string.Format("${0:00.00}", clothes.price); //string.Format("{0:00.00}", double.Parse(clothes.price.ToString()));
            Label_Clothes_overview.Text = clothes.overview.Replace("\n","<br/>");

            DataSet dataset = GetImages();
            Repeater1.DataSource = dataset;
            Repeater1.DataBind();
        }
        private DataSet GetImages() {
            try {
                clothes = Clothes.getClothesID(Request.QueryString["id"].ToString());
            } catch (NullReferenceException) {
                Response.Redirect("/routes/error/404.html");
            } // Change to -> DRY CODE 

            List<string> image_paths = new List<string>();

            // https://www.aspsnippets.com/Articles/Display-Directory-Folder-structure-using-ASPNet-TreeView-control-in-C-and-VBNet.aspx
            DirectoryInfo di = new DirectoryInfo(Server.MapPath(@"../assets/img/_clothing/carousel/" + clothes.id + "/"));
            foreach (FileInfo f in di.GetFiles()) {
                image_paths.Add("../assets/img/_clothing/carousel/" + clothes.id + "/" + f.Name);
            }
            
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(dt);
            dt.Columns.Add("paths");
            //dt.Columns.Add("id", typeof(string));
            // other columns...
            foreach (string i in image_paths) {
                DataRow dr = dt.Rows.Add();
                dr.SetField("paths", i);
                // other properties
            }
            return ds;
        } // https://stackoverflow.com/questions/45283037/fill-dataset-from-list-of-class-items

        protected void Button_AddToCart_Click(object sender, EventArgs e) {
            try {
                clothes = Clothes.getClothesID(Request.QueryString["id"].ToString());
            } catch (NullReferenceException) {
                Response.Redirect("/routes/error/404.html");
            } // Change to -> DRY CODE 


            //Debug.WriteLine(Session["cart"]);
            Dictionary<string, Cart> carts = (Dictionary<string, Cart>)Session["cart"];

            if (carts.ContainsKey(clothes.id)) { // if clothes already in cart, modify item
                carts[clothes.id].item_quantity = int.Parse(ListBox_Quantity.SelectedValue);
                carts[clothes.id].item_size = ListBox_Size.SelectedValue;
                carts[clothes.id].item_color = ListBox_Colour.SelectedValue;
            } else { // Add clothes to cart
                carts[clothes.id] = new Cart(clothes.name, int.Parse(ListBox_Quantity.SelectedValue), double.Parse(clothes.price.ToString()), ListBox_Colour.SelectedValue, ListBox_Size.SelectedValue, clothes.id);
            }
            // update cart with new/updated clothes
            Session["cart"] = carts;
        }
    }
}