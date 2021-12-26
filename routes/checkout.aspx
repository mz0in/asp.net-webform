<%@ Page Title="" Language="C#" MasterPageFile="~/BeforeLogin.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="AWAD_Assignment.routes.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Checkout</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="https://polyfill.io/v3/polyfill.min.js?version=3.52.1&features=fetch"></script>
    <script src="https://js.stripe.com/v3/"></script>
    <style>
        #returning_customer_session_management_div {
        }
    </style>
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
            <div runat="server" id="theReturningCustomerDiv">
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
                        <div class="row contact_form" <%--action="#" method="post" novalidate="novalidate"--%>>

                            <div class="col-md-6 form-group p_star">
                                <!-- <input type="text" class="form-control" id="first" name="name" /> -->
                                <!-- <span class="placeholder" data-placeholder="First name"></span> -->
                                <asp:TextBox ID="TextBox_FirstName" CssClass="form-control" TabIndex="1" Placeholder="First Name" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FirstName" runat="server" ErrorMessage="Required Field" ForeColor="Red" Text="Required Field" ControlToValidate="TextBox_FirstName"></asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator_FirstName" ForeColor="Red" ControlToValidate="TextBox_FirstName" ValidationExpression="^([a-zA-Z' ]+)$" ErrorMessage="Real names only have letters"></asp:RegularExpressionValidator>

                                <div class="col-md-6 form-group p_star">
                                    <!-- <input type="text" class="form-control" id="last" name="name" /> -->
                                    <!-- <span class="placeholder" data-placeholder="Last name"></span> -->
                                    <asp:TextBox ID="TextBox_MobileNumber" TabIndex="3" TextMode="Phone" CssClass="form-control" Placeholder="Mobile Number" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_MobileNumber" ForeColor="Red" runat="server" ErrorMessage="Required Field" Text="Required Field" ControlToValidate="TextBox_MobileNumber"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator_MobileNumber" ForeColor="Red" ControlToValidate="TextBox_MobileNumber" ValidationExpression="^\d{8}$" ErrorMessage="Invalid mobile number"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-6 form-group p_star">
                                <!-- <input type="text" class="form-control" id="number" name="number" /> -->
                                <!-- <span class="placeholder" data-placeholder="Phone number"></span> -->
                                <asp:TextBox ID="TextBox_LastName" CssClass="form-control" TabIndex="2" Placeholder="Last Name" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_LastName" runat="server" ErrorMessage="Required Field" ForeColor="Red" Text="Required Field" ControlToValidate="TextBox_LastName"></asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator_LastName" ForeColor="Red" ControlToValidate="TextBox_LastName" ValidationExpression="^([a-zA-Z' ]+)$" ErrorMessage="Real names only have letters"></asp:RegularExpressionValidator>
                            </div>

                            <div class="col-md-6 form-group p_star">
                                <!-- <input type="text" class="form-control" id="email" name="compemailany" /> -->
                                <!-- <span class="placeholder" data-placeholder="Email Address"></span> -->
                                <asp:TextBox ID="TextBox_Email" TextMode="Email" TabIndex="4" CssClass="form-control" Placeholder="Email Address" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ForeColor="Red" ErrorMessage="Required Field" Text="Required Field" ControlToValidate="TextBox_Email"></asp:RequiredFieldValidator>
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
                                <!-- <input type="text" class="form-control" id="add1" name="add1" /> -->
                                <!-- <span class="placeholder" data-placeholder="Address line 01"></span> -->
                                <asp:TextBox ID="TextBox_Address1" TextMode="MultiLine" TabIndex="5" CssClass="form-control" Placeholder="Address line 01" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="Required Field" Text="Required Field" ControlToValidate="TextBox_MobileNumber"></asp:RequiredFieldValidator>
                                <%--<br />--%>
                                <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ForeColor="Red" ControlToValidate="TextBox_Address1" ValidationExpression="(?:<script(?:\s[^<>]+)*>.*<\/script>)|(?:<.+(?:\s[^<>]+)*(?:\s(?:onload|onfocus|onerror)=(?:&quot;|').+(?:&quot;|'))(?:\s[^<>]+)*\/?>(?:<\/.+>)?)|(?:<.+(?:\s[^<>]+)*(?:\s[^<>]+=(?:&quot;|')javascript:.+(?:&quot;|'))(?:\s[^<>]+)*\/?>(?:<\/.+>)?)" ErrorMessage="No XSS please"></asp:RegularExpressionValidator>--%>
                                <%--<br />--%>
                                <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ForeColor="Red" ControlToValidate="TextBox_Address1" ValidationExpression="(((((where|and|set)(\s+[\w\(]+[.]*[\w\)]*\s*)=\s*'?|like\s+'?%?|in\s+\(|top\s*|from|order\s*by|table)\s*)|(values\s*\(.*))(&quot;\s*\+|{\d+}))" ErrorMessage="No SQL injection please"></asp:RegularExpressionValidator>--%>
                            </div>

                            <div class="col-md-12 form-group p_star">
                                <!-- <input type="text" class="form-control" id="add2" name="add2" /> -->
                                <!-- <span class="placeholder" data-placeholder="Address line 02"></span> -->
                                <asp:TextBox ID="TextBox_Address2" TextMode="MultiLine" TabIndex="6" CssClass="form-control" Placeholder="Address line 02" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ErrorMessage="Required Field" Text="Required Field" ControlToValidate="TextBox_MobileNumber"></asp:RequiredFieldValidator>--%>
                                <%--<br />--%>
                                <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" ForeColor="Red" ControlToValidate="TextBox_Address2" ValidationExpression="(?:<script(?:\s[^<>]+)*>.*<\/script>)|(?:<.+(?:\s[^<>]+)*(?:\s(?:onload|onfocus|onerror)=(?:&quot;|').+(?:&quot;|'))(?:\s[^<>]+)*\/?>(?:<\/.+>)?)|(?:<.+(?:\s[^<>]+)*(?:\s[^<>]+=(?:&quot;|')javascript:.+(?:&quot;|'))(?:\s[^<>]+)*\/?>(?:<\/.+>)?)" ErrorMessage="No XSS please"></asp:RegularExpressionValidator>--%>
                                <%--<br />--%>
                                <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ForeColor="Red" ControlToValidate="TextBox_Address2" ValidationExpression="(((((where|and|set)(\s+[\w\(]+[.]*[\w\)]*\s*)=\s*'?|like\s+'?%?|in\s+\(|top\s*|from|order\s*by|table)\s*)|(values\s*\(.*))(&quot;\s*\+|{\d+}))" ErrorMessage="No SQL injection please"></asp:RegularExpressionValidator>--%>
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
                                <!-- <input type="text" class="form-control" id="zip" name="zip" placeholder="Postcode/ZIP" /> -->
                                <asp:TextBox ID="TextBox_Zipcode" TextMode="Phone" TabIndex="7" CssClass="form-control" Placeholder="Postcode/ZIP" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" runat="server" ErrorMessage="Required Field" Text="Required Field" ControlToValidate="TextBox_Zipcode"></asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5" ForeColor="Red" ControlToValidate="TextBox_Zipcode" ValidationExpression="^\d{6}$" ErrorMessage="Invalid Zipcode/Postal Code"></asp:RegularExpressionValidator>
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
                                <textarea class="form-control" name="message" tabindex="8" id="message" rows="1"
                                    placeholder="Order Notes"></textarea>
                            </div>
                        </div>
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
                            <div class="payment_item active">
                                <div class="radion_btn">
                                    <%--<input type="radio" id="f-option5" name="selector" />--%>
                                    <%--<label for="f-option5">Stripe payment</label>--%>
                                    <asp:RadioButton GroupName="selector" Checked="true" runat="server" Text="Stripe" />
                                    <div class="check"></div>
                                </div>
                                <p>
                                    Pay with Credit/Debit card
                                </p>
                            </div>
                            <div class="payment_item">
                                <div class="radion_btn">
                                    <!-- <input type="radio" id="f-option6" name="selector" /> -->
                                    <!-- <label for="f-option6">Paypal </label> -->
                                    <asp:RadioButton GroupName="selector" Enabled="false" runat="server" Text="PayPal" />
                                    <img src="../assets/img/product/single-product/card.jpg" alt="" />
                                    <div class="check"></div>
                                </div>
                                <p>
                                    Pay with your PayPal account
                                </p>
                            </div>
                            <div class="creat_account">
                                <!--
                                <input type="checkbox" id="f-option4" name="selector" />
                                <label for="f-option4">I’ve read and accept the </label>
                                <a href="#">terms & conditions*</a>
                                -->
                                <asp:CheckBox runat="server" ID="CheckBox_ToS" Checked="false" Text="I’ve read and accept the terms & conditions*" />
                            </div>
                            <!-- <a class="btn_3" href="#">Proceed to Payment</a> -->
                            <%--<asp:LinkButton runat="server" CssClass="btn_3" Text="Proceed to Payment" OnClick="LinkButton_Payment_Click" ID="LinkButton_Payment"></asp:LinkButton>--%>
                            <asp:Button runat="server" CssClass="btn_3" Text="Proceed to Payment" OnClick="Button_Payment_Click" ID="Button_Payment"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================End Checkout Area =================-->
</asp:Content>
