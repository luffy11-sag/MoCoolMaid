<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="contactus.aspx.cs" Inherits="MoCoolMaid.contactus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <section class="contact">
        <div class="main-text">
            <h2>Contact Us</h2>
        </div>
        <br>
        <br>

        <div class="container">
            <div class="row">
                <div class="col-lg-6">
                    <div class="info">
                        <div class="box">
                            <div class="font">
                                <i class="fa-solid fa-location-dot"></i>
                            </div>
                            <div class="contact-text">
                                <h3>Address</h3>
                                <p>
                                    111, Eagleton,<br>
                                    Mauritius
                                </p>
                            </div>
                        </div>

                        <div class="box">
                            <div class="font">
                                <i class="fa-solid fa-envelope"></i>
                            </div>
                            <div class="contact-text">
                                <h3>Email</h3>
                                <p>demo@gmail.com</p>
                            </div>
                        </div>

                        <div class="box">
                            <div class="font">
                                <i class="fa-solid fa-phone"></i>
                            </div>
                            <div class="contact-text">
                                <h3>Telephone</h3>
                                <p>+230 2323232</p>
                            </div>
                        </div>

                        <div class="box">
                            <div class="font">
                                <i class="fa-brands fa-whatsapp"></i>
                            </div>
                            <div class="contact-text">
                                <h3>Whatsapp Us</h3>
                                <p>+230 57777777</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="feedback-form">
                        <h4>Send Us a Message</h4>
                        <div class="input-field">
                            <input type="text" name="full-name" required="required">
                            <span>Full Name</span>
                        </div>

                        <div class="input-field">
                            <input type="text" name="email" required="required">
                            <span>Email</span>
                        </div>

                        <div class="input-field">
                            <textarea name="feedback-field" required="required"></textarea>
                            <span>Your Message</span>
                        </div>

                        <div class="input-field">
                            <input type="submit" name="submit-btn" value="Send">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
