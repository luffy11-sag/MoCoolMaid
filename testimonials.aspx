<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="testimonials.aspx.cs" Inherits="MoCoolMaid.testimonials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="rating-box">
            <div class="rating-container">
                <div class="main-text">
                    <h2>Write Your Testimonial</h2>
                </div>
                <br />
                <label for="fuPP">Upload a Profile Picture</label>
                <asp:FileUpload runat="server" ID="fuPP" CssClass="form-control" />
                <br />
                <label for="fuPP">Testimonial</label>
                <asp:TextBox runat="server" ID="txtTestimonial" CssClass="form-control" TextMode="MultiLine" Rows="8"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="submit-button" OnClick="btnSubmit_Click" />
                <asp:Label runat="server" ID="lblTxtMessage" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
