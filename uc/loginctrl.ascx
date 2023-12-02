<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="loginctrl.ascx.cs" Inherits="MoCoolMaid.uc.loginctrl" %>
<h2>Welcome</h2>
<asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
<asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="txtEmail"></asp:TextBox>
<asp:RequiredFieldValidator CssClass="text-danger" ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ErrorMessage="*Required Field" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" CssClass="txtPass"></asp:TextBox>
<asp:RequiredFieldValidator CssClass="text-danger" ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true" ErrorMessage="*Required Field" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
<asp:label ID="lblRememberMe" runat="server" for="chkRememberMe" CssClass="remember-label">
    <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="remember-checkbox" />
    Remember Me
</asp:label>
