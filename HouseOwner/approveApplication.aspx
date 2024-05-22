<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="approveApplication.aspx.cs" Inherits="MoCoolMaid.HouseOwner.approveApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="main-text">
            <h2>Job Applications</h2>
        </div>
        <asp:GridView ID="gvs" ClientIDMode="Static" OnPreRender="gvs_PreRender1" CssClass="table table-striped table-bordered"
            runat="server" OnRowDataBound="gvs_RowDataBound" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="fname" HeaderText="First Name" />
                <asp:BoundField DataField="lname" HeaderText="Last Name" />
                <asp:BoundField DataField="email" HeaderText="Email" />
                <asp:ImageField DataImageUrlField="image"
                    DataImageUrlFormatString="~/images/{0}" ControlStyle-Width="100" HeaderText="Profile Pic" />
                <asp:BoundField DataField="appdate" HeaderText="Request Date&Time" />
                <asp:BoundField DataField="jname" HeaderText="Job Title" />
                <asp:BoundField DataField="tumstatus" HeaderText="Access Status" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <%--store the movie id in the hiddenfield--%>
                        <asp:HiddenField ID="hidjob" runat="server" Value='<%# Eval("JobId") %>' />
                        <%--store the user id in the LinkButtons--%>
                        <asp:LinkButton ID="lnkdeny" CssClass="btn btn-danger" OnClick="lnkdeny_Click"
                            runat="server" CommandArgument='<%# Eval("HKID") %>'>Reject</asp:LinkButton><br />
                        <br />
                        <asp:LinkButton ID="lnkgrant" CssClass="btn btn-success" OnClick="lnkgrant_Click"
                            runat="server" CommandArgument='<%# Eval("HKID") %>'>Accept</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:TextBox runat="server" ID="txtEmail" Visible="false"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtUsername" Visible="false"></asp:TextBox>
    </div>
</asp:Content>
