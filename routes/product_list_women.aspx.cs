using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AWAD_Assignment.routes
{
    public partial class product_list_women : BasePage {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != "" && Request.QueryString["q"] != null) {
                if (Request.QueryString["q"] == "tops") {
                    // Dataset
                    DataSet dataset = GetWomenTops();
                    Repeater1.DataSource = dataset;
                    Repeater1.DataBind();

                    //Set Categories - Top
                    LinkButtonCategory1.Text = "Shirts & Blouses";
                    LinkButtonCategory2.Text = "Sweaters";
                    LinkButtonCategory3.Text = "Sweats";
                    LinkButtonCategory4.Text = "T-Shirts";
                    LinkButtonCategory5.Text = "UT(Graphic T-Shirts)";

                } else if (Request.QueryString["q"] == "bottoms") {
                    // Dataset
                    DataSet dataset = GetWomenBottoms();
                    Repeater1.DataSource = dataset;
                    Repeater1.DataBind();

                    //Set Categories - Bottom
                    LinkButtonCategory1.Text = "Jeans";
                    LinkButtonCategory2.Text = "Legging Pants";
                    LinkButtonCategory3.Text = "Pants";
                    LinkButtonCategory4.Text = "Shorts";
                    LinkButtonCategory5.Text = "Skirts";
                } else {
                    //redirect if not valid parameter
                    Response.Redirect(ResolveClientUrl("product_list_women.aspx?q=tops"));
                }
            } else {
                //Return absolute path with params "q"
                Response.Redirect(ResolveClientUrl("product_list_women.aspx?q=tops"));
            }
        }
        private DataSet GetWomenClothes() {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F'", conn);
                DataSet temp = new DataSet();
                sql.Fill(temp);
                return temp;
            }
        }
        private DataSet GetWomenTops() {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (11,12,13,7,8)", conn);
                DataSet temp = new DataSet();
                sql.Fill(temp);
                return temp;
            }
        }
        private DataSet GetWomenBottoms() {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (2,9,3,4,10)", conn);
                DataSet temp = new DataSet();
                sql.Fill(temp);
                return temp;
            }
        }
        protected void LinkButtonCategory1_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "Shirts & Blouses"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (11)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Jeans"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (3)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
        protected void LinkButtonCategory2_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "Sweaters"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (12)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Legging Pants"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (9)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
        protected void LinkButtonCategory3_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "Sweats"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (13)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Pants"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (3)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
        protected void LinkButtonCategory4_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "T-Shirts"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (7)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Shorts"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (4)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
        protected void LinkButtonCategory5_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "UT(Graphic T-Shirts)"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (8)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Skirts"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'F' and category_id in (10)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
    }
}