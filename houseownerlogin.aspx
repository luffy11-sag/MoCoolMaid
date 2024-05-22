<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="houseownerlogin.aspx.cs" Inherits="MoCoolMaid.houseownerlogin" %>

<%@ Register Src="~/uc/loginctrl2.ascx" TagPrefix="houseowner" TagName="loginform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid login-container">
        <houseowner:loginform runat="server" ID="hoLogin" ValidationGroup="HOlogin" />
        <asp:Label ID="lblmsgHO" runat="server" Text=""></asp:Label>
        <asp:Button ID="btnHOLogin" runat="server" Text="Login" OnClick="btnHOLogin_Click" CssClass="btnLogin btn btn-primary" ValidationGroup="HOlogin" />
        <br />
        <br />
        Don't have an account yet?
            <asp:HyperLink ID="HyperLink1" runat="server" Text="Sign up" CssClass="signup-link-button" NavigateUrl="~/SignUp.aspx" />
    </div>
</asp:Content>
