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
    public partial class single_product : BasePage {
        Clothes clothes = null;
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                try {
                    clothes = Clothes.getClothesID(Request.QueryString["id"].ToString());
                } catch (NullReferenceException) {
                    Response.Redirect("404");
                }
                if (clothes == null) Response.Redirect("404");

                Label_Clothes_name.Text = clothes.name;
                Label_Clothes_price.Text = string.Format("${0:00.00}", clothes.price); //string.Format("{0:00.00}", double.Parse(clothes.price.ToString()));
                Label_Clothes_overview.Text = clothes.overview.Replace("\n", "<br/>");

                // Get all Images related to this product        
                DataSet datasetImages = GetImages(clothes.id);
                RepeaterImages.DataSource = datasetImages;
                RepeaterImages.DataBind();

                // Get all reviews related to this product        
                DataSet datasetReviews = GetAllReviews(clothes.id);
                RepeaterReview.DataSource = datasetReviews;
                RepeaterReview.DataBind();

                // If user has already created a review, show the review in the textbox.
                if (UserAlreadyCreatedReview(clothes.id)) {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                    conn.Open();
                    //SqlCommand command = new SqlCommand("SELECT review FROM Ratings WHERE account_id = 'a379c844-fa66-4103-958f-59cfc2424e23' AND product_id = '1db01a98-4f62-49dc-8fe5-0b789a89386a'", conn);
                    SqlCommand command = new SqlCommand("SELECT review, stars FROM Ratings WHERE account_id = @aid AND product_id = @pid", conn);
                    command.Parameters.AddWithValue("@aid", Account.GetAccount(Session["email"].ToString()).id);
                    command.Parameters.AddWithValue("@pid", clothes.id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        TextBox_ReviewDescription.Text = reader[0].ToString();
                        Rating1.CurrentRating = int.Parse(reader[1].ToString());
                    }
                    conn.Close();
                }

                Label_ReviewCount.Text = GetAllReviewsCount(clothes.id); // Display review count

                // Display Average Rating stars & Rating number
                DataTable dataTable = GetAverageRating(clothes.id);
                Label_AverageReviewStar.Text = dataTable.Rows[0][0].ToString();
            }
        }
        private DataSet GetImages(string product_id) {

            clothes = Clothes.getClothesID(product_id);

            List<string> image_paths = new List<string>();

            // https://www.aspsnippets.com/Articles/Display-Directory-Folder-structure-using-ASPNet-TreeView-control-in-C-and-VBNet.aspx
            DirectoryInfo di = new DirectoryInfo(Server.MapPath(@"./assets/img/_clothing/carousel/" + clothes.id + "/"));
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
                Response.Redirect("404");
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

        protected void Button_SubmitReview_Click(object sender, EventArgs e) {
            // Check user is signed in, only signed user are allowed to leave a review
            if (Session["email"] == null) {
                Response.Write("<script>alert('Only logged in users are allowed to leave a review')</script>");
                return;
            }

            // Validate input and ensure fields are not empty
            if (TextBox_ReviewDescription.Text.Trim().Length < 5) {
                Response.Write("<script>alert('Review must contain 5 characters minimum')</script>");
            }
            if (Rating1.CurrentRating == 0) {
                Response.Write("<script>alert('Please select the rating star before submitting')</script>");
            }

            // check if the user creating a review or updating existing review
            if (UserAlreadyCreatedReview(Request.QueryString["id"].ToString())) { // Update rating 
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                SqlCommand cmd = new SqlCommand("UPDATE RATINGS SET review = @review, stars=@stars WHERE account_id = @aid AND product_id = @pid", conn);
                //SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@review", TextBox_ReviewDescription.Text);
                cmd.Parameters.AddWithValue("@stars", Rating1.CurrentRating.ToString());
                cmd.Parameters.AddWithValue("@aid", Account.GetAccount(Session["email"].ToString()).id);
                cmd.Parameters.AddWithValue("@pid", Request.QueryString["id"].ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            } else { // insert rating                
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO [RATINGS] (Id, account_id, product_id, stars, review) VALUES (@id, @ac_id, @pd_id, @stars,@review)", conn);
                //SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("@ac_id", Account.GetAccount(Session["email"].ToString()).id);
                cmd.Parameters.AddWithValue("@pd_id", Request.QueryString["id"].ToString());
                cmd.Parameters.AddWithValue("@stars", Rating1.CurrentRating.ToString());
                cmd.Parameters.AddWithValue("@review", TextBox_ReviewDescription.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            Response.Redirect(Request.Url.AbsoluteUri);

        }

        private DataSet GetAllReviews(string product_id) {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from Ratings WHERE product_id = @pid", conn);
            cmd.Parameters.AddWithValue("@pid", product_id);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            // Creating dataset, and all the columns name for the repeater
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            dt.Columns.Add("name");
            dt.Columns.Add("review");

            while (dr.Read()) {
                DataRow dataRow = dt.Rows.Add();
                string name = GetUserFullnameByAccountID(dr["account_id"].ToString());
                dataRow.SetField("name", name);
                dataRow.SetField("review", dr["review"]);
            }

            conn.Close();
            dr.Close();
            dr.Dispose();
            return ds;


        }

        private bool UserAlreadyCreatedReview(string product_id) {
            if (Session["email"] == null) return false;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Ratings WHERE account_id = @aid AND product_id = @pid", conn);
            command.Parameters.AddWithValue("@aid", Account.GetAccount(Session["email"].ToString()).id);
            command.Parameters.AddWithValue("@pid", product_id);
            return Convert.ToInt32(command.ExecuteScalar().ToString()) == 0 ? false : true ;
        }

        private string GetAllReviewsCount(string product_id) {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Ratings WHERE product_id = @pid", conn);
            command.Parameters.AddWithValue("@pid", product_id);
            return command.ExecuteScalar().ToString();
        }

        private DataTable GetAverageRating(string product_id) {
            // SELECT ISNULL(AVG(Rating), 0) AverageRating, COUNT(Rating) " + "RatingCount FROM [RATINGS] WHERE Title = @booktitle
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            //return command.ExecuteScalar().ToString();
            SqlDataAdapter dataAdapter = new SqlDataAdapter($"SELECT ISNULL(AVG(stars), 0) as averageRating FROM Ratings WHERE product_id = '{product_id}'", conn);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt;
        }

        private string GetUserFullnameByAccountID(string account_id) {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from Accounts WHERE id = @aid", conn);
            cmd.Parameters.AddWithValue("@aid", account_id);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            string name = "";
            if (dr.Read()) {
                name = dr["first_name"].ToString() + " " + dr["last_name"].ToString();
            }
            conn.Close();
            dr.Close();
            dr.Dispose();
            return name;
        }
    }
}