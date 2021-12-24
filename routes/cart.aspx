<%@ Page Title="" Language="C#" MasterPageFile="~/BeforeLogin.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="AWAD_Assignment.routes.cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Shopping Cart</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Preloader" runat="server">
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
                            <h2>Shopping Cart</h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- slider Area End-->
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="body" runat="server">
    <!--================Cart Area =================-->
    <section class="cart_area section_padding">
        <div class="container">
            <div class="cart_inner">
                <div class="table-responsive">
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col">Product</th>
                                <th scope="col">Price</th>
                                <th scope="col">Quantity</th>
                                <th scope="col">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <%--<asp:Repeater ID="Repeater1" runat="server" OnItemCommand="R1_ItemCommand">--%> 
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <div class="media">
                                                <div class="d-flex">
                                                    <asp:Image runat="server" ImageUrl='<%# Eval("image")%>' ID="Image_Checkout" />
                                                </div>
                                                <div class="media-body">
                                                    <p>
                                                        <asp:Label runat="server" Text='<%# Eval("name")%>' ID="Label_Name"></asp:Label>
                                                        <asp:Label runat="server" Visible="false" Enabled="false" Font-Size="XX-Small" Text='<%# Eval("id")%>' ID="Label_ID"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <h5>
                                                <asp:Label runat="server" ID="Label_Price" Text='<%# string.Format("${0:00.00}", double.Parse(Eval("price").ToString()))%>'></asp:Label></h5>
                                        </td>
                                        <td>
                                            <div class="product_count">
                                                <asp:TextBox runat="server" TextMode="Number" Text='<%# Eval("quantity")%>' ID="TextBox_CartQuantity"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <h5>
                                                <asp:Label runat="server" ID="Label_Total" Text='<%# string.Format("${0:00.00}", double.Parse(Eval("total").ToString()))%>'></asp:Label></h5>
                                        </td>
                                        <td>
                                            <%--//==== Here we have used CommandName property to distinguish which button is  clicked and we have passed our primary key to CommandArgument property. ====//--%>
                                            <%--<asp:Button runat="server" ID="Button_Edit" Text="Update" CommandName="Edit" ToolTip="Update a record." CommandArgument='<%#Eval("id")+","+ Eval("quantity")%>'/>--%>
                                            <%--<asp:Button runat="server" ID="Button_Delete" Text="Delete" CommandName="Delete" ToolTip="Delete a record." CommandArgument='<%#Eval("id")%>'/>--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                            <!--
                            <tr>
                                <td>
                                    <div class="media">
                                        <div class="d-flex">
                                            <img src="../assets/img/arrivel/arrivel_1.png" alt="" />
                                        </div>
                                        <div class="media-body">
                                            <p>Minimalistic shop for multipurpose use</p>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <h5>$360.00</h5>
                                </td>
                                <td>
                                    <div class="product_count">
                                        <span class="input-number-decrement"><i class="ti-minus"></i></span>
                                        <input class="input-number" type="text" value="1" min="0" max="10">
                                        <span class="input-number-increment"><i class="ti-plus"></i></span>
                                    </div>
                                </td>
                                <td>
                                    <h5>$720.00</h5>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="media">
                                        <div class="d-flex">
                                            <img src="../assets/img/arrivel/arrivel_2.png" alt="" />
                                        </div>
                                        <div class="media-body">
                                            <p>Minimalistic shop for multipurpose use</p>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <h5>$360.00</h5>
                                </td>
                                <td>
                                    <div class="product_count">
                                        <span class="input-number-decrement"><i class="ti-minus"></i></span>
                                        <input class="input-number" type="text" value="1" min="0" max="10">
                                        <span class="input-number-increment"><i class="ti-plus"></i></span>
                                    </div>
                                </td>
                                <td>
                                    <h5>$720.00</h5>
                                </td>
                            </tr>
                            -->


                            <tr class="bottom_button">
                                <td>
                                    <!-- <a class="btn_1" href="#">t</a> -->
                                    <%--<asp:LinkButton runat="server" CssClass="btn_1" Text="Update Cart" OnClick="LinkButton_UpdateCart_Click" ID="LinkButton_UpdateCart"></asp:LinkButton>--%>
                                    <%--<asp:LinkButton runat="server" CssClass="btn_1" Text="Update Cart" OnClick="LinkButton_UpdateCart_Click" ID="LinkButton_UpdateCart"></asp:LinkButton>--%>
                                    <asp:Button runat="server" CssClass="btn_1" Text="Update Cart" OnClick="Button_UpdateCart_Click" ID="Button_UpdateCart" />
                                </td>
                                <td></td>
                                <td></td>
                                <td>
                                    <!--
                                    <div class="cupon_text float-right">
                                        <a class="btn_1" href="#">Close Coupon</a>
                                    </div>
                                    -->
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <h5>Subtotal</h5>
                                </td>
                                <td>
                                    <h5>
                                        <asp:Label ID="Label_SubTotal" runat="server"></asp:Label></h5>
                                </td>
                            </tr>
                            <!--
                            <tr class="shipping_area">
                                <td></td>
                                <td></td>
                                <td>
                                    <h5>Shipping</h5>
                                </td>
                                <td>
                                    <div class="shipping_box">
                                        <ul class="list">
                                            <li>Flat Rate: $5.00
                        <input type="radio" aria-label="Radio button for following text input">
                                            </li>
                                            <li>Free Shipping
                        <input type="radio" aria-label="Radio button for following text input">
                                            </li>
                                            <li>Flat Rate: $10.00
                        <input type="radio" aria-label="Radio button for following text input">
                                            </li>
                                            <li class="active">Local Delivery: $2.00
                        <input type="radio" aria-label="Radio button for following text input">
                                            </li>
                                        </ul>
                                        <h6>Calculate Shipping
                      <i class="fa fa-caret-down" aria-hidden="true"></i>
                                        </h6>
                                        <select class="shipping_select">
                                            <option value="1">Bangladesh</option>
                                            <option value="2">India</option>
                                            <option value="4">Pakistan</option>
                                        </select>
                                        <select class="shipping_select section_bg">
                                            <option value="1">Select a State</option>
                                            <option value="2">Select a State</option>
                                            <option value="4">Select a State</option>
                                        </select>
                                        <input class="post_code" type="text" placeholder="Postcode/Zipcode" />
                                        <a class="btn_1" href="#">Update Details</a>
                                    </div>
                                </td>
                            </tr>
                            -->
                        </tbody>
                    </table>
                    <div class="checkout_btn_inner float-right">
                        <!-- <a class="btn_1" href="#">Continue Shopping</a> -->
                        <a class="btn_1 checkout_btn_1" href="checkout.aspx">Proceed to checkout</a>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Cart Area =================-->
</asp:Content>
