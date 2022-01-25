<%@ Page Title="" Language="C#" MasterPageFile="~/AfterLoginAdmin.Master" AutoEventWireup="true" CodeBehind="admin_dashboard.aspx.cs" Inherits="AWAD_Assignment.routes.admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Dashboard</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Preloader" runat="server">
    <!-- Preloader Start -->
    <div id="preloader-active">
        <div class="preloader d-flex align-items-center justify-content-center">
            <div class="preloader-inner position-relative">
                <div class="preloader-circle"></div>
                <div class="preloader-img pere-text">
                    <img src="../assets/img/logo/logo.png" alt="">
                </div>
            </div>
        </div>
    </div>
    <!-- Preloader End -->
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="sliderAreaContent" runat="server">
    <!-- slider Area Start-->
    <div class="slider-area ">
        <!-- Mobile Menu -->
        <div class="single-slider slider-height2 d-flex align-items-center" data-background="../assets/img/hero/category.jpg">
            <div class="container">
                <div class="row">
                    <div class="col-xl-12">
                        <div class="hero-cap text-center">
                            <h2>Dashboard</h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- slider Area End-->
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="body" runat="server">
    <%--<asp:GridView runat="server" ID="ClothesTable"
        AutoGenerateColumns="false" ShowFooter="true" GridLines="Vertical" CellPadding="4" ItemType="Clothes" SelectMethod="GetAllClothes" CssClass="table table-borderless">
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="ID" SortExpression="ProductID" />
            <asp:BoundField DataField="Product.ProductName" HeaderText="Name" />
            <asp:BoundField DataField="Product.UnitPrice" HeaderText="Price (each)" DataFormatString="{0:c}" />
            <asp:TemplateField HeaderText="Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="PurchaseQuantity" Width="40" runat="server" Text="<%#: Item.Quantity %>"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Total">
                <ItemTemplate>
                    <%#: String.Format("{0:c}", ((Convert.ToDouble(Item.Quantity)) *  Convert.ToDouble(Item.Product.UnitPrice)))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remove Item">
                <ItemTemplate>
                    <asp:CheckBox ID="Remove" runat="server"></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>--%>



    <asp:GridView runat="server" ID="GridView_ProductTable" AutoGenerateColumns="false" DataKeyNames="Id" ShowHeader="true" ShowHeaderWhenEmpty="true" 
        OnRowCommand="GridView_ProductTable_RowCommand" OnRowEditing="GridView_ProductTable_RowEditing" OnRowCancelingEdit="GridView_ProductTable_RowCancelingEdit"
        OnRowUpdating="GridView_ProductTable_RowUpdating" OnRowDeleting="GridView_ProductTable_RowDeleting">
        <Columns>
            <%--<asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="TextBox_name" Text='<%# Eval("Id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Name">
                <HeaderTemplate>
                    <asp:label runat="server" Text="Name"></asp:label>
                    <asp:TextBox runat="server" ID="Add_Name"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("name") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="TextBox_name" Text='<%# Eval("name") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity">
                <HeaderTemplate>
                     <asp:label runat="server" Text="Quantity"></asp:label>
                    <asp:TextBox runat="server" ID="Add_Quantity"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="TextBox_quantity" Text='<%# Eval("quantity") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price">
                <HeaderTemplate>
                     <asp:label runat="server" Text="Price"></asp:label>
                    <asp:TextBox runat="server" ID="Add_Price"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("price") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="TextBox_price" Text='<%# Eval("price") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Overview">
                <HeaderTemplate>
                     <asp:label runat="server" Text="Overview"></asp:label>
                    <br />
                    <asp:TextBox runat="server" TextMode="MultiLine" Width="" ID="Add_Overview"></asp:TextBox>
                    <%--<textarea runat="server" style="width:100%" id="Add_Overview"></textarea>--%>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("overview") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="TextBox_Overview" Text='<%# Eval("overview") %>'></asp:TextBox>
                    <%--<textarea runat="server" style="width:100%" id="TextBox_Overview" Text='<%# Eval("overview") %>'></textarea>--%>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Gender">
                <HeaderTemplate>
                     <asp:label runat="server" Text="Gender"></asp:label>
                    <asp:TextBox runat="server" ID="Add_Gender" MaxLength="1"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("gender") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="TextBox_gender" Text='<%# Eval("gender") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category ID">
                <HeaderTemplate>
                     <asp:label runat="server" Text="Category ID"></asp:label>
                    <asp:TextBox runat="server" ID="Add_CategoryID"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("category_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="TextBox_categoryID" Text='<%# Eval("category_id") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Link">
                <HeaderTemplate>
                     <asp:label runat="server" Text="Link"></asp:label>
                    <asp:TextBox runat="server" ID="Add_Link"></asp:TextBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("link") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" ID="TextBox_link" Text='<%# Eval("link") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton runat="server" CssClass="fa fa-edit" ForeColor="deepskyblue" CommandName="Edit" ToolTip="Edit"></asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="fa fa-trash" ForeColor="Purple" CommandName="Delete" ToolTip="Delete"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton runat="server" CssClass="fa fa-save" ForeColor="SaddleBrown" CommandName="Update" ToolTip="Update"></asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="fa fa-window-close" ForeColor="OrangeRed" CommandName="Cancel" ToolTip="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <HeaderTemplate>
                    <asp:LinkButton runat="server" CssClass="fa fa-plus" ForeColor="GreenYellow" CommandName="Add" ToolTip="Add"></asp:LinkButton>
                </HeaderTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
