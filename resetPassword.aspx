<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="resetPassword.aspx.cs" Inherits="MoCoolMaid.resetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="postJob-box">
        <div class="form-group">
            <asp:TextBox ID="Password" runat="server" placeholder="New Password" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="*Required" ControlToValidate="Password"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regPassword" ControlToValidate="Password"
    ValidationExpression="^(?=.*\d{2})(?=.*[a-zA-Z]{2}).{6,}$" runat="server" ErrorMessage="Password Not Strong"></asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="ConfirmPassword" runat="server" placeholder="Confirm New Password" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="*Required" ControlToValidate="ConfirmPassword"></asp:RequiredFieldValidator>
            <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" CssClass="text-danger" Display="Dynamic" ErrorMessage="The passwords do not match." />
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save-button" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel-button" OnClick="btnCancel_Click" CausesValidation="false" />
            <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
