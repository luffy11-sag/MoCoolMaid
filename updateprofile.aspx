<%@ Page Title="Update Profile" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="updateprofile.aspx.cs" Inherits="MoCoolMaid.updateprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="signup-container">
        <div class="signup-box">
            <h2 class="signup-title" style="font-family: Montserrat;">Update Your Profile</h2>
            <div class="form-group">
                <label for="txtUptFirstName">First Name</label>
                <asp:TextBox ID="txtUptFirstName" runat="server" CssClass="form-control" />

                <label for="txtUptLastName">Last Name</label>
                <asp:TextBox ID="txtUptLastName" runat="server" CssClass="form-control" />

                <label for="txtUptEmail">Email</label>
                <asp:TextBox ID="txtUptEmail" runat="server" CssClass="form-control" />

                <label for="txtUptPhone">Phone</label>
                <asp:TextBox ID="txtUptPhone" runat="server" CssClass="form-control" />

                <label for="txtUptStreet">Street</label>
                <asp:TextBox ID="txtUptStreet" runat="server" CssClass="form-control" />

                <label for="txtUptCity">City</label>
                <asp:TextBox ID="txtUptCity" runat="server" CssClass="form-control" />

                <label for="txtUptZipCode">Zip Code</label>
                <asp:TextBox ID="txtUptZipCode" runat="server" CssClass="form-control" />

                <label for="txtUptBio">Bio or About me</label>
                <asp:TextBox ID="txtUptBio" runat="server" CssClass="form-control" TextMode="MultiLine" />


                <asp:Button ID="btnUpdate" runat="server" Text="Update Profile" CssClass="form-control signup-button" />
            </div>
        </div>
    </div>
</asp:Content>
