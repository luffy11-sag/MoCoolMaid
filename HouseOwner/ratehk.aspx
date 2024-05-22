<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ratehk.aspx.cs" Inherits="MoCoolMaid.HouseOwner.ratehk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
        <asp:GridView ID="gvs" runat="server" AutoGenerateColumns="False" CssClass="table">
            <Columns>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <asp:Label ID="lblUser" Text='<%# Eval("lName") + " " + Eval("fname") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnRate" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="Rate" Text="Rate" CommandArgument='<%# Eval("HKID") %>' OnClick="lbtnRate_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
