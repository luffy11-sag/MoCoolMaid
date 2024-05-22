<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="manageAccount.aspx.cs" Inherits="MoCoolMaid.Admin.manageAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

    <div class="container-fluid">
        <div class="main-text1">
            <h2>Registered Housekeepers</h2>
        </div>
        <div class="d-flex justify-content-center">
            <div class="dropdownManageAcc">
                <asp:DropDownList runat="server" ID="ddlAccount" CssClass="ddlAcc form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged" OnPreRender="ddlAccount_PreRender">
                    <asp:ListItem Value="-1" Text="All Users"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Housekeeper"></asp:ListItem>
                    <asp:ListItem Value="2" Text="House Owner"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <asp:GridView ID="gvs" DataKeyNames="User_ID" runat="server" AutoGenerateColumns="False" ClientIDMode="Static" CssClass="table" OnRowDataBound="gvs_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDeactivate" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="Deactivate" Text="Deactivate" CommandArgument='<%# Eval("User_ID") %>' OnClick="lbtnDeactivate_Click" />
                        <asp:LinkButton ID="lbtnActivate" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="Activate" Text="Activate" CommandArgument='<%# Eval("User_ID") %>' Visible="false" OnClick="lbtnActivate1_Click" />
                        <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="View" Text="View" CommandArgument='<%# Eval("User_ID") %>' OnClick="lbtnView_Click" />

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>                      
                        <asp:Label ID="lblUser" Text='<%# Eval("User_LName") + " " + Eval("User_FName") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:GridView ID="gvsUsers" DataKeyNames="User_ID" runat="server" ClientIDMode="Static" AutoGenerateColumns="False" CssClass="table" OnRowDataBound="gvsUsers_RowDataBound" OnPreRender="gvsUsers_PreRender" >
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDeactivate1" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="Deactivate" Text="Deactivate" CommandArgument='<%# Eval("User_ID") %>' OnClick="lbtnDeactivate_Click" />
                        <asp:LinkButton ID="lbtnActivate1" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="Activate" Text="Activate" CommandArgument='<%# Eval("User_ID") %>' Visible="false" OnClick="lbtnActivate1_Click" />
                        <asp:LinkButton ID="lbtnView1" runat="server" CssClass="btn btn-outline-info linkButton" CommandName="View" Text="View" CommandArgument='<%# Eval("User_ID") %>' OnClick="lbtnView_Click" />

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>                       
                        <asp:Label ID="lblUser" Text='<%# Eval("User_LName") + " " + Eval("User_FName") %>'
                            runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField></asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="modal fade" id="userModal" tabindex="-1" role="dialog" aria-labelledby="housekeeperModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="housekeeperModalLabel">User Details</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <br />
                        <br />
                        <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblFirstName" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblUserType" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <script>
            function viewUserModal() {
                $(document).ready(function () {
                    $('#userModal').modal('show');
                });
            }
        </script>

        <div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="MessageModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="MessageModalLabel">User Details</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <br />
                        <br />
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <script>
            function viewMessageModal() {
                $(document).ready(function () {
                    $('#messageModal').modal('show');
                });
            }
        </script>
    </div>
</asp:Content>
