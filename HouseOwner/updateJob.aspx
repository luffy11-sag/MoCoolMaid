<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="updateJob.aspx.cs" Inherits="MoCoolMaid.HouseOwner.updateJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="postJob-box">
        <div class="postJob-container">
            <h2 style="font-family: Montserrat; text-align: center;">Edit Job Details</h2>
            <div class="postJob-field">
                <asp:Label runat="server" AssociatedControlID="txtJobID"></asp:Label>
                <asp:TextBox runat="server" ID="txtJobId" CssClass="form-control" Visible="false" />
            </div>
            <div class="postJob-field">
                <asp:Label runat="server" AssociatedControlID="ddlJobType">Job Type</asp:Label>
                <asp:DropDownList runat="server" ID="ddlJobType" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqJobType" runat="server" CssClass="text-danger" ErrorMessage="*Required" InitialValue="-1" ControlToValidate="ddlJobType"></asp:RequiredFieldValidator>
            </div>
            <div class="postJob-field">
                <asp:Label runat="server" AssociatedControlID="txtJobDescription">Job Description</asp:Label>
                <asp:TextBox runat="server" ID="txtJobDescription" TextMode="MultiLine" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="reqJobDesc" runat="server" CssClass="text-danger" ErrorMessage="*Required" ControlToValidate="txtJobDescription"></asp:RequiredFieldValidator>
            </div>
            <div class="postJob-field">
                <asp:Label runat="server" AssociatedControlID="txtJobDeadline">Job Deadline</asp:Label>
                <asp:TextBox runat="server" ID="txtJobDeadline" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="reqJobDeadline" runat="server" CssClass="text-danger" ErrorMessage="*Required" ControlToValidate="txtJobDeadline"></asp:RequiredFieldValidator>
            </div>
            <div class="postJob-field">
                <asp:Label runat="server" AssociatedControlID="txtJobSalary">Job Salary</asp:Label>
                <asp:TextBox runat="server" ID="txtJobSalary" CssClass="form-control" TextMode="Number" placeholder="Rs." />
                <asp:RequiredFieldValidator ID="reqJobSalary" runat="server" CssClass="text-danger" ErrorMessage="*Required" ControlToValidate="txtJobSalary"></asp:RequiredFieldValidator>
            </div>

            <div class="postJob-field">
                <asp:Label runat="server" AssociatedControlID="ddlJobStatus">Status</asp:Label>
                <asp:DropDownList runat="server" ID="ddlJobStatus" CssClass="form-control">
                    <asp:ListItem Text="Pending" Value="1" />
                    <asp:ListItem Text="Occupied" Value="2" />
                    <asp:ListItem Text="Vacant" Value="3" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger" ErrorMessage="*Required" InitialValue="-1" ControlToValidate="ddlJobDistrict"></asp:RequiredFieldValidator>
            </div>

            <h4 style="font-family: Montserrat; text-align: center;">Location</h4>
            <div class="postJob-field">
                <asp:Label runat="server" AssociatedControlID="ddlJobDistrict">District</asp:Label>
                <asp:DropDownList runat="server" ID="ddlJobDistrict" CssClass="form-control">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqJobDistrict" runat="server" CssClass="text-danger" ErrorMessage="*Required" InitialValue="-1" ControlToValidate="ddlJobDistrict"></asp:RequiredFieldValidator>
            </div>
            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="post-button" OnClick="btnSave_Click" />
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
