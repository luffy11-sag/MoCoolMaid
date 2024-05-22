<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="housekeeperlogin.aspx.cs" Inherits="MoCoolMaid.housekeeperlogin" %>

<%@ Register Src="~/uc/loginctrl2.ascx" TagPrefix="housekeeper" TagName="loginform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid login-container">
    <housekeeper:loginform runat="server" ID="hkLogin1" ValidationGroup="HKLogin" />
    <asp:Label ID="lblmsgHK" runat="server" Text=""></asp:Label>
    <asp:Button ID="btnHKLogin" runat="server" Text="Login" OnClick="btnHKLogin_Click" CssClass="btnLogin btn btn-primary" ValidationGroup="HKlogin" />
    <br />
    <br />
    Don't have an account yet?
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Sign up" CssClass="signup-link-button" NavigateUrl="~/SignUp.aspx" />
</div>
</asp:Content>