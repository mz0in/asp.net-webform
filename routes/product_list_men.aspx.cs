using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AWAD_Assignment.routes {
    public partial class product_list_men : BasePage { 
        protected void Page_Load(object sender, EventArgs e) {

            if (Request.QueryString["q"] != "" && Request.QueryString["q"] != null) {
                if (Request.QueryString["q"] == "tops") {
                    // Dataset
                    DataSet dataset = GetMenTops();
                    Repeater1.DataSource = dataset;
                    Repeater1.DataBind();

                    //Set Categories - Top
                    //somelat.InnerText = "bananas"; // Using Anchor tag
                    LinkButtonCategory1.Text = "Casual Shirts";
                    LinkButtonCategory2.Text = "Polo Shirts";
                    LinkButtonCategory3.Text = "T-Shirts";
                    LinkButtonCategory4.Text = "UT(Graphic T-Shirts)";

                } else if (Request.QueryString["q"] == "bottoms") {
                    // Dataset
                    DataSet dataset = GetMenBottoms();
                    Repeater1.DataSource = dataset;
                    Repeater1.DataBind();

                    //Set Categories - Bottom
                    LinkButtonCategory1.Text = "Chino";
                    LinkButtonCategory2.Text = "Jeans";
                    LinkButtonCategory3.Text = "Pants";
                    LinkButtonCategory4.Text = "Shorts";
                } else {
                    //redirect if not valid parameter
                    Response.Redirect(ResolveClientUrl("product_list_men.aspx?q=tops"));
                }
            } else {
                //Return absolute path with params "q"
                Response.Redirect(ResolveClientUrl("product_list_men.aspx?q=tops"));
            }
        }

        private DataSet GetMenClothes() {
            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M'", conn);
                DataSet temp = new DataSet();
                sql.Fill(temp);
                return temp;
            }
        }
        private DataSet GetMenTops() {
            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (5,6,7,8)", conn);
                DataSet temp = new DataSet();
                sql.Fill(temp);
                return temp;
            }
        }        
        private DataSet GetMenBottoms() {
            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (1,2,3,4)", conn);
                DataSet temp = new DataSet();
                sql.Fill(temp);
                return temp;
            }
        }

        protected void GetMenTops_CasualShirts_ServerClick(object sender, EventArgs e) {

        }

        protected void LinkButtonCategory1_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "Casual Shirts"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (5)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Chino"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (1)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
        protected void LinkButtonCategory2_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "Polo Shirts"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (6)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Jeans"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (2)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
        protected void LinkButtonCategory3_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "T-Shirts"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (7)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Pants"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (3)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
        protected void LinkButtonCategory4_Click(object sender, EventArgs e) {
            if (Request.QueryString["q"] == "tops") {
                // Edit filter = "UT(Graphic T-Shirts)"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (8)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            } else {
                // Edit filter = "Shorts"
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes where gender = 'M' and category_id in (4)", conn);
                    DataSet temp = new DataSet();
                    sql.Fill(temp);
                    Repeater1.DataSource = temp;
                    Repeater1.DataBind();
                }
            }
        }
    }
}