<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="appliedjobs.aspx.cs" Inherits="MoCoolMaid.Housekeeper.appliedjobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <asp:Repeater ID="rptApplications" runat="server" >
            <HeaderTemplate>
                <table class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>House Owner</th>
                            <th>Email</th>
                            <th>Date Applied</th>
                            <th>Job Title</th>
                            <th>Application Status</th>                           
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("fname") + " " + Eval("lname") %></td>
                    <td><%# Eval("email") %></td>
                    <td><%# Eval("appdate") %></td>
                    <td><%# Eval("jname") %></td>
                    <td><%# GetApplicationStatusText(Eval("tumstatus")) %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Panel runat="server" ID="pnlNoApp" Visible="false" CssClass="panelMessage">
            <div class="default-message">
                <h1>You Have Not Applied for Any Jobs Yet!</h1>
                <asp:Button runat="server" ID="btnBrowseJobs" CssClass="btnApply btn btn-primary" Text="Browse Jobs" OnClick="btnBrowseJobs_Click" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
