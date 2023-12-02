<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="MoCoolMaid.signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="signup-box">
        <div class="form-group">
            <h2>Welcome to MoCoolMaid</h2>

            <div class="name-container">
                <div class="form-control">
                    <label for="txtFirstName">First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Required="true" />
                    <asp:RequiredFieldValidator ID="reqFname" ControlToValidate="txtFirstName" runat="server" CssClass="text-danger" ErrorMessage="*Required"></asp:RequiredFieldValidator>

                </div>
                <div class="form-control">
                    <label for="txtLastName">Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Required="true" />
                    <asp:RequiredFieldValidator ID="reqLname" ControlToValidate="txtLastName" runat="server" CssClass="text-danger" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                </div>
            </div>
            <label for="txtDateOfBirth">Date of Birth</label>
            <asp:TextBox ID="txtDateOfBirth" runat="server" placeholder="Date of Birth" TextMode="Date" CssClass="form-control" Required="true" />
            <asp:RequiredFieldValidator ID="reqDOB" ControlToValidate="txtDateOfBirth" runat="server" CssClass="text-danger" ErrorMessage="*Required"></asp:RequiredFieldValidator>
            <!-- Gender Radio Buttons -->
            <div class="radio-group">
                <label>Gender:</label>
                <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" CssClass="form-check-inline" Required="true">
                    <asp:ListItem Text="Mr" Value="Mr" />
                    <asp:ListItem Text="Mrs" Value="Mrs" />
                    <asp:ListItem Text="Other" Value="Other" />
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="reqGender" ControlToValidate="rblGender" runat="server" CssClass="text-danger" ErrorMessage="Please Select a Gender"></asp:RequiredFieldValidator>
            </div>

            <!-- Role Dropdown -->
            <label for="ddlRole">Role</label>
            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control ddlRole" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true" Required="true">
                <asp:ListItem Text="Choose role" Value="-1" />
                <asp:ListItem Text="House Owner" Value="1" />
                <asp:ListItem Text="Housekeeper" Value="2" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="reqRole" ControlToValidate="ddlRole" runat="server" CssClass="text-danger" ErrorMessage="*Choose a Role"></asp:RequiredFieldValidator>

            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Required="true" />
            <asp:RequiredFieldValidator ID="reqEmail" ControlToValidate="txtEmail" runat="server" CssClass="text-danger" ErrorMessage="*Required"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegEmail" runat="server" ControlToValidate="txtEmail" CssClass="text-danger"
                ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                ErrorMessage="Not Valid"></asp:RegularExpressionValidator>

            <div class="password-container">
                <div class="form-control">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" />
                    <asp:RequiredFieldValidator ID="reqPass" ControlToValidate="txtPassword" runat="server" CssClass="text-danger" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regPassword" ControlToValidate="txtPassword"
                        ValidationExpression="^(?=.*\d{2})(?=.*[a-zA-Z]{2}).{6,}$" runat="server" CssClass="text-danger" ErrorMessage="Password Not Strong"></asp:RegularExpressionValidator>
                </div>
                <div class="form-control">
                    <label for="txtConfirmPassword">Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" Required="true" />
                    <asp:RequiredFieldValidator ID="reqConPass" ControlToValidate="txtConfirmPassword" runat="server" CssClass="text-danger" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="conPass" runat="server" CssClass="text-danger" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match!"></asp:CompareValidator>
                </div>
            </div>

            <label for="txtCity">City</label>
            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Required="true" />
            <label for="txtPhone">Phone Number</label>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Required="true" />

            <div id="fuSection" runat="server">
                <label for="fuCV">Upload CV or any relevant documents:</label>
                <asp:FileUpload ID="fuCV" runat="server" CssClass="form-control" />
                <label for="fuPP">Upload a Profile Picture:</label>
                <asp:FileUpload ID="fuPP" runat="server" CssClass="form-control" />
            </div>

            <asp:Label ID="lblTxtMessage" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnSignUp" runat="server" OnClick="btnSignUp_Click" Text="Sign Up" CssClass="form-control signup-button" />
            <p class="login-link">Already have an account? <a href="login.aspx">Sign in</a></p>
        </div>
    </div>
</asp:Content>
