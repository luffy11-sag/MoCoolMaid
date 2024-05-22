<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="adminlogin.aspx.cs" Inherits="MoCoolMaid.adminlogin" %>

<%@ Register Src="~/uc/loginctrl.ascx" TagPrefix="admin" TagName="loginform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid login-container">
        <admin:loginform runat="server" ID="adminLogin" ValidationGroup="adminLogin" EmailPlaceholder="Username" ShowRememberMe="false" />
        <asp:Label ID="lblmsgAD" runat="server"></asp:Label>
        <asp:Button ID="btnAdminLogin" runat="server" Text="Login" OnClick="btnAdminLogin_Click" CssClass="btnLogin btn btn-primary" ValidationGroup="adminLogin" />        
    </div>
</asp:Content>
