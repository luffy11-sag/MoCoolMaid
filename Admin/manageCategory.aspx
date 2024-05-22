<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="manageCategory.aspx.cs" Inherits="MoCoolMaid.Admin.manageCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
        <div class="container-fluid">
            <!-- Form for Category ID and Category Name -->
            <asp:Panel ID="pnlCategoryForm" runat="server">
                <div class="mb-3 catForm">
                    <asp:Label ID="lblCategoryId" runat="server" AssociatedControlID="txtCategoryId">Category ID</asp:Label>
                    <asp:TextBox ID="txtCategoryId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="mb-3 catForm">
                    <asp:Label ID="lblCategoryName" runat="server" AssociatedControlID="txtCategoryName">Category Name</asp:Label>
                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <!-- Buttons for Add, Update, Delete -->
                <div class="mb-3 btnForm">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary btnCatForm" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-secondary btnCatForm" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger btnCatForm" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger btnCatForm" onclick="btnCancel_Click" />

                </div>
            </asp:Panel>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <!-- GridView for Category Details -->
            <asp:GridView ID="gvs" DataKeyNames="JCategory_ID" runat="server" AutoGenerateColumns="False" CssClass="table" OnSelectedIndexChanged="gvs_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="Select" Text="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category Name">
                        <ItemTemplate>
                            <!-- display the category name -->
                            <asp:Label ID="lblCatName" Text='<%#Eval("JCategory_Name")%>'
                                runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
</asp:Content>
