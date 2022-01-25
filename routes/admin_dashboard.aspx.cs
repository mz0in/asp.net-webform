﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AWAD_Assignment.routes {
    public partial class admin_dashboard : BasePage {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                GetAllClothes();
            }
        }
        protected void GetAllClothes() {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                connection.Open();
                SqlDataAdapter sql = new SqlDataAdapter("select * from Clothes", connection);
                sql.Fill(dt);
            }
            if (dt.Rows.Count > 0) {
                GridView_ProductTable.DataSource = dt;
                GridView_ProductTable.DataBind();
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

        protected void GridView_ProductTable_RowCommand(object sender, GridViewCommandEventArgs e) {
            try {
                if (e.CommandName == "Add") {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                        connection.Open();
                        string query = "INSERT INTO Clothes (id, name, quantity, price, overview, gender, category_id, link) VALUES (@id, @name, @quantity, @price, @overview, @gender, @category_id, @link)";
                        SqlCommand sql = new SqlCommand(query, connection);
                        sql.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                        sql.Parameters.AddWithValue("@name", (GridView_ProductTable.HeaderRow.FindControl("Add_Name") as TextBox).Text.Trim());
                        sql.Parameters.AddWithValue("@quantity", Convert.ToInt32((GridView_ProductTable.HeaderRow.FindControl("Add_Quantity") as TextBox).Text.Trim()));
                        sql.Parameters.AddWithValue("@price", Convert.ToDouble((GridView_ProductTable.HeaderRow.FindControl("Add_Price") as TextBox).Text.Trim()));
                        sql.Parameters.AddWithValue("@overview", (GridView_ProductTable.HeaderRow.FindControl("Add_Overview") as TextBox).Text.Trim());
                        sql.Parameters.AddWithValue("@gender", (GridView_ProductTable.HeaderRow.FindControl("Add_Gender") as TextBox).Text.Trim());
                        sql.Parameters.AddWithValue("@category_id",  Convert.ToInt32((GridView_ProductTable.HeaderRow.FindControl("Add_CategoryID") as TextBox).Text.Trim()));
                        sql.Parameters.AddWithValue("@link", (GridView_ProductTable.HeaderRow.FindControl("Add_Link") as TextBox).Text.Trim());
                        //sql.Parameters.AddWithValue("@", );
                        sql.ExecuteNonQuery();
                        GetAllClothes();
                    }
                }
            } catch (SqlException err) {

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
                    connection.Open();
                    string query = "UPDATE Clothes SET name=@name, quantity=@quantity, price=@price, overview=@overview, gender=@gender, category_id=@category_id, link=@link WHERE id=@id";
                    SqlCommand sql = new SqlCommand(query, connection);
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
            } catch (SqlException err) {}
        }

        protected void GridView_ProductTable_RowDeleting(object sender, GridViewDeleteEventArgs e) {
            try {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString)) {
                    connection.Open();
                    string query = "DELETE Clothes WHERE id=@id";
                    SqlCommand sql = new SqlCommand(query, connection);
                    sql.Parameters.AddWithValue("@id", GridView_ProductTable.DataKeys[e.RowIndex].Value.ToString());
                    sql.ExecuteNonQuery();
                    GetAllClothes();
                }
            } catch (SqlException err) { }
        }
    }
}