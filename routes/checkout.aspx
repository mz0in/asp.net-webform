<%@ Page Title="" Language="C#" MasterPageFile="~/BeforeLogin.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="AWAD_Assignment.routes.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Checkout</asp:Content>

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
                            <h2>Checkout</h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- slider Area End-->
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="body" runat="server">
    <!--================Checkout Area =================-->
    <section class="checkout_area section_padding">
        <div class="container">
            <div class="returning_customer">
                <div class="check_title">
                    <h2>Returning Customer?
            <a href="#">Click here to login</a>
                    </h2>
                </div>
                <p>
                    If you have shopped with us before, please enter your details in the
          boxes below. If you are a new customer, please proceed to the
          Billing & Shipping section.
                </p>
                <form class="row contact_form" action="#" method="post" novalidate="novalidate">
                    <div class="col-md-6 form-group p_star">
                        <input type="text" class="form-control" id="name" name="name" value=" " />
                        <span class="placeholder" data-placeholder="Username or Email"></span>
                    </div>
                    <div class="col-md-6 form-group p_star">
                        <input type="password" class="form-control" id="password" name="password" value="" />
                        <span class="placeholder" data-placeholder="Password"></span>
                    </div>
                    <div class="col-md-12 form-group">
                        <button type="submit" value="submit" class="btn_3">
                            log in
                        </button>
                        <div class="creat_account">
                            <input type="checkbox" id="f-option" name="selector" />
                            <label for="f-option">Remember me</label>
                        </div>
                        <a class="lost_pass" href="#">Lost your password?</a>
                    </div>
                </form>
            </div>
            <!--
            <div class="cupon_area">
                <div class="check_title">
                    <h2>Have a coupon?
            <a href="#">Click here to enter your code</a>
                    </h2>
                </div>
                <input type="text" placeholder="Enter coupon code" />
                <a class="tp_btn" href="#">Apply Coupon</a>
            </div>
            -->
            <div class="billing_details">
                <div class="row">
                    <div class="col-lg-8">
                        <h3>Shipping Details</h3>
                        <form class="row contact_form" action="#" method="post" novalidate="novalidate">
                            <div class="col-md-6 form-group p_star">
                                <input type="text" class="form-control" id="first" name="name" />
                                <span class="placeholder" data-placeholder="First name"></span>
                            </div>
                            <div class="col-md-6 form-group p_star">
                                <input type="text" class="form-control" id="last" name="name" />
                                <span class="placeholder" data-placeholder="Last name"></span>
                            </div>
                            <div class="col-md-6 form-group p_star">
                                <input type="text" class="form-control" id="number" name="number" />
                                <span class="placeholder" data-placeholder="Phone number"></span>
                            </div>
                            <div class="col-md-6 form-group p_star">
                                <input type="text" class="form-control" id="email" name="compemailany" />
                                <span class="placeholder" data-placeholder="Email Address"></span>
                            </div>
                            <div class="col-md-12 form-group p_star">
                                <select class="country_select">
                                    <option value="1">Singapore</option>
                                    <!--
                                    <option value="2">Country</option>
                                    <option value="4">Country</option>
                                    -->
                                </select>
                            </div>
                            <div class="col-md-12 form-group p_star">
                                <input type="text" class="form-control" id="add1" name="add1" />
                                <span class="placeholder" data-placeholder="Address line 01"></span>
                            </div>
                            <div class="col-md-12 form-group p_star">
                                <input type="text" class="form-control" id="add2" name="add2" />
                                <span class="placeholder" data-placeholder="Address line 02"></span>
                            </div>
                            <!--
                            <div class="col-md-12 form-group p_star">
                                <input type="text" class="form-control" id="city" name="city" />
                                <span class="placeholder" data-placeholder="Town/City"></span>
                            </div>
                            <div class="col-md-12 form-group p_star">
                                <select class="country_select">
                                    <option value="1">District</option>
                                    <option value="2">District</option>
                                    <option value="4">District</option>
                                </select>
                            </div>
                            -->
                            <div class="col-md-12 form-group">
                                <input type="text" class="form-control" id="zip" name="zip" placeholder="Postcode/ZIP" />
                            </div>
                            <div class="col-md-12 form-group">
                                <div class="creat_account">
                                    <input type="checkbox" id="f-option2" name="selector" />
                                    <label for="f-option2">Create an account?</label>
                                </div>
                            </div>
                            <div class="col-md-12 form-group">
                                <!--
                                <div class="creat_account">
                                    <h3>Shipping Details</h3>
                                    <input type="checkbox" id="f-option3" name="selector" />
                                    <label for="f-option3">Ship to a different address?</label>
                                </div>
                                -->
                                <textarea class="form-control" name="message" id="message" rows="1"
                                    placeholder="Order Notes"></textarea>
                            </div>
                        </form>
                    </div>
                    <div class="col-lg-4">
                        <div class="order_box">
                            <h2>Your Order</h2>
                            <ul class="list">
                                <li><a href="#">Product<span>Total</span></a></li>

                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <%--<li><a href="#">Fresh Blackberry<span class="middle">x 02</span><span class="last">$720.00</span></a></li>--%>
                                        <li>
                                            <asp:LinkButton runat="server">
                                                <asp:Label runat="server" ID="Label_name" Text='<%#Eval("name") %>'></asp:Label>
                                                <asp:Label runat="server" ID="Label_quantity" Text='<%# "x " + string.Format("{0:D2}", Eval("quantity").ToString()) %>' CssClass="middle"></asp:Label>
                                                <asp:Label runat="server" ID="Label_price" Text='<%# string.Format("${0:00.00}", double.Parse(Eval("price").ToString())) %>' CssClass="last"></asp:Label>
                                            </asp:LinkButton></li>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <!--
                                <li><a href="#">Fresh Blackberry<span class="middle">x 02</span><span class="last">$720.00</span></a></li>
                                <li><a href="#">Fresh Tomatoes<span class="middle">x 02</span><span class="last">$720.00</span></a></li>
                                <li><a href="#">Fresh Brocoli<span class="middle">x 02</span><span class="last">$720.00</span></a></li>
                                -->
                            
                            </ul>
                            <ul class="list list_2">
                                <li><a href="#">Subtotal<span><asp:Label runat="server" ID="Label_subtotal"></asp:Label></span></a></li>
                                <li><a href="#">Shipping<span><asp:Label runat="server" ID="Label_shipping"></asp:Label></span></a></li>
                                <li><a href="#">Total<span><asp:Label runat="server" ID="Label_total"></asp:Label></span></a></li>
                            </ul>
                            <div class="payment_item">
                                <div class="radion_btn">
                                    <input type="radio" id="f-option5" name="selector" />
                                    <label for="f-option5">Stripe payment</label>
                                    <div class="check"></div>
                                </div>
                                <p>
                                    Pay with Credit/Debit card
                                </p>
                            </div>
                            <div class="payment_item active">
                                <div class="radion_btn">
                                    <input type="radio" id="f-option6" name="selector" />
                                    <label for="f-option6">Paypal </label>
                                    <img src="../assets/img/product/single-product/card.jpg" alt="" />
                                    <div class="check"></div>
                                </div>
                                <p>
                                    Pay with your PayPal account
                                </p>
                            </div>
                            <div class="creat_account">
                                <input type="checkbox" id="f-option4" name="selector" />
                                <label for="f-option4">I’ve read and accept the </label>
                                <a href="#">terms & conditions*</a>
                            </div>
                            <a class="btn_3" href="#">Proceed to Payment</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Checkout Area =================-->
</asp:Content>
