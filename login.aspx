<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MoCoolMaid.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="login-container">
        <div class="login-box">
            <h2>Welcome</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="txtEmail"></asp:TextBox>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="txtPass"></asp:TextBox>
            <label for="chkRememberMe" class="remember-label">
                <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="remember-checkbox" />
                Remember Me
            </label>
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btnLogin btn btn-primary" />
            <div class="signup-link">
                Don't have an account yet?
                <asp:HyperLink ID="hlSignUp" runat="server" Text="Sign up" CssClass="signup-link-button" NavigateUrl="SignUp.aspx" />
            </div>
        </div>
    </div>
</asp:Content>
