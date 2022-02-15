using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AWAD_Assignment.routes {
    public partial class admin_dashboard : BasePage {
        protected void Page_Load(object sender, EventArgs e) {

            // If admin is not logged in, give 404
            if (Session["email"] == null) Response.Redirect(ResolveClientUrl("../404"));
            if (!Account.GetAccount(Session["email"].ToString()).isAdmin) Response.Redirect(ResolveClientUrl("../404"));

            if (!IsPostBack) {
                GetAllClothes();
            }
        }
        protected void GetAllClothes() {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                using (SqlCommand command = new SqlCommand("Product_CRUD", connection)) {
                    connection.Open();
                    command.Parameters.AddWithValue("@ACTION", "SELECT");
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter sql = new SqlDataAdapter()) {
                        sql.SelectCommand = command;

                        //SqlDataAdapter sql = new SqlDataAdapter(, connection);
                        sql.Fill(dt);
                    }
                    if (dt.Rows.Count > 0) {
                        GridView_ProductTable.DataSource = dt;
                        GridView_ProductTable.DataBind();
                        GridView_ProductTable.HeaderRow.TableSection = TableRowSection.TableHeader;
                    } else {
                        dt.Rows.Add(dt.NewRow());
                        GridView_ProductTable.DataSource = dt;
                        GridView_ProductTable.DataBind();
                        GridView_ProductTable.Rows[0].Cells.Clear();
                        GridView_ProductTable.Rows[0].Cells.Add(new TableCell());
                        GridView_ProductTable.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                        GridView_ProductTable.Rows[0].Cells[0].Text = "No Clothes in Database";
                        GridView_ProductTable.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                        GridView_ProductTable.Rows[0].Cells[0].VerticalAlign = VerticalAlign.Middle;
                    }
                }
            }
        }

        protected void GridView_ProductTable_RowCommand(object sender, GridViewCommandEventArgs e) {
            try {
                if (e.CommandName == "Add") {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                        using (SqlCommand sql = new SqlCommand("Product_CRUD", connection)) {
                            connection.Open();
                            sql.Parameters.AddWithValue("@ACTION", "INSERT");
                            sql.CommandType = CommandType.StoredProcedure;
                            string id = Guid.NewGuid().ToString();
                            sql.Parameters.AddWithValue("@id", id);
                            sql.Parameters.AddWithValue("@name", (GridView_ProductTable.HeaderRow.FindControl("Add_Name") as TextBox).Text.Trim());
                            sql.Parameters.AddWithValue("@quantity", Convert.ToInt32((GridView_ProductTable.HeaderRow.FindControl("Add_Quantity") as TextBox).Text.Trim()));
                            sql.Parameters.AddWithValue("@price", Convert.ToDouble((GridView_ProductTable.HeaderRow.FindControl("Add_Price") as TextBox).Text.Trim()));
                            sql.Parameters.AddWithValue("@overview", (GridView_ProductTable.HeaderRow.FindControl("Add_Overview") as TextBox).Text.Trim());
                            sql.Parameters.AddWithValue("@gender", (GridView_ProductTable.HeaderRow.FindControl("Add_Gender") as TextBox).Text.Trim().ToUpper());
                            sql.Parameters.AddWithValue("@category_id", Convert.ToInt32((GridView_ProductTable.HeaderRow.FindControl("Add_CategoryID") as TextBox).Text.Trim()));
                            sql.Parameters.AddWithValue("@link", (GridView_ProductTable.HeaderRow.FindControl("Add_Link") as TextBox).Text.Trim());
                            sql.Parameters.AddWithValue("@today", DateTime.Now);
                            sql.ExecuteNonQuery();

                            // Create directory for images
                            string path = Server.MapPath(@"/assets/img/_clothing/carousel/" + id);
                            if (!Directory.Exists(path)) {
                                Directory.CreateDirectory(path);
                            }

                            // Save Images
                            StringBuilder files = new StringBuilder();
                            FileUpload images = (GridView_ProductTable.HeaderRow.FindControl("FileUpload_image") as FileUpload);
                            try {
                                if (images.HasFiles) {

                                    foreach (var image in images.PostedFiles) {

                                        // Check if "1.jpg" exists, if not rename the image to it - Saving images
                                        if (!File.Exists(Path.Combine(path, "1.jpg"))) image.SaveAs(Path.Combine(path, "1.jpg"));
                                        else image.SaveAs(Path.Combine(path, image.FileName));
                                    }
                                }
                            } catch (Exception err) {
                                Debug.WriteLine(err);
                            }
                            GetAllClothes();
                        }
                    }
                }
            } catch (SqlException err) {
                Debug.WriteLine(err);
            } catch (FormatException err) {
                Debug.WriteLine(err);
            }
        }

        protected void GridView_ProductTable_RowEditing(object sender, GridViewEditEventArgs e) {
            GridView_ProductTable.EditIndex = e.NewEditIndex;
            GetAllClothes();
        }

        protected void GridView_ProductTable_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) {
            GridView_ProductTable.EditIndex = -1;
            GetAllClothes();
        }

        protected void GridView_ProductTable_RowUpdating(object sender, GridViewUpdateEventArgs e) {
            try {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    using (SqlCommand sql = new SqlCommand("Product_CRUD", connection)) {
                        connection.Open();
                        sql.Parameters.AddWithValue("@ACTION", "UPDATE");
                        sql.CommandType = CommandType.StoredProcedure;
                        sql.Parameters.AddWithValue("@id", GridView_ProductTable.DataKeys[e.RowIndex].Value.ToString());
                        sql.Parameters.AddWithValue("@name", (GridView_ProductTable.Rows[e.RowIndex].FindControl("TextBox_name") as TextBox).Text.Trim());
                        sql.Parameters.AddWithValue("@quantity", Convert.ToInt32((GridView_ProductTable.Rows[e.RowIndex].FindControl("TextBox_quantity") as TextBox).Text.Trim()));
                        sql.Parameters.AddWithValue("@price", Convert.ToDouble((GridView_ProductTable.Rows[e.RowIndex].FindControl("TextBox_price") as TextBox).Text.Trim()));
                        sql.Parameters.AddWithValue("@overview", (GridView_ProductTable.Rows[e.RowIndex].FindControl("TextBox_Overview") as TextBox).Text.Trim());
                        sql.Parameters.AddWithValue("@gender", (GridView_ProductTable.Rows[e.RowIndex].FindControl("TextBox_gender") as TextBox).Text.Trim());
                        sql.Parameters.AddWithValue("@category_id", Convert.ToInt32((GridView_ProductTable.Rows[e.RowIndex].FindControl("TextBox_categoryID") as TextBox).Text.Trim()));
                        sql.Parameters.AddWithValue("@link", (GridView_ProductTable.Rows[e.RowIndex].FindControl("TextBox_link") as TextBox).Text.Trim());
                        //sql.Parameters.AddWithValue("@", );
                        sql.ExecuteNonQuery();
                        GridView_ProductTable.EditIndex = -1;
                        GetAllClothes();
                    }                
                }
            } catch (SqlException) {
            } catch (FormatException) { 
            }
        }

        protected void GridView_ProductTable_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            try {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    using (SqlCommand sql = new SqlCommand("Product_CRUD", connection)) {
                        connection.Open();
                        string id = GridView_ProductTable.DataKeys[e.RowIndex].Value.ToString();
                        sql.Parameters.AddWithValue("@ACTION", "DELETE");
                        sql.CommandType = CommandType.StoredProcedure;
                        sql.Parameters.AddWithValue("@id", id);
                        sql.ExecuteNonQuery();

                        // Remove Directory
                        DirectoryInfo di = new DirectoryInfo(Server.MapPath(@"/assets/img/_clothing/carousel/" + id + "/"));
                        foreach (FileInfo file in di.GetFiles()) {
                            file.Delete();
                        }
                        di.Delete();

                        GetAllClothes();
                    }
                }
            } catch (SqlException) { }
        }
    }
}