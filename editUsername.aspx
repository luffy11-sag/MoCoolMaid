<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="editUsername.aspx.cs" Inherits="MoCoolMaid.editUsername" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="postJob-box">
        <div class="form-group">
            <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*Required" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="*Required" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save-button" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="cancel-button" OnClick="btnCancel_Click" CausesValidation="false" />
            <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
