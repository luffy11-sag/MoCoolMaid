<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewPostedJobs.aspx.cs" Inherits="MoCoolMaid.HouseOwner.viewPostedJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="main-text1">
            <h2>Jobs You Posted</h2>
        </div>
        <asp:ListView ID="lvPostedJobs" runat="server" DataKeyNames="Job_ID">
            <LayoutTemplate>
                <div class="row">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="job-card">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("JCategory_Name") %></h5>
                            <p class="card-text"><strong>Description:</strong> <%# Eval("Job_Desc") %></p>
                            <p class="card-text"><strong>Salary:</strong> <%# Eval("Job_Salary") %></p>
                            <p class="card-text"><strong>Location:</strong> <%# Eval("District") %></p>
                            <p class="card-text"><strong>Deadline:</strong> <%# Eval("Job_Deadline") %></p>
                            <p class="card-text"><strong>Status:</strong> <%# GetStatusDescription(Convert.ToInt32(Eval("Job_Status"))) %></p>

                            <asp:LinkButton ID="lnkUpdateJob" runat="server" CommandName="Edit" CssClass="btn btn-primary link-Button" CommandArgument='<%# Eval("Job_ID") %>' OnClick="lnkUpdateJob_Click">Edit</asp:LinkButton>
                            <asp:LinkButton ID="lnkChangeStatus" runat="server" CommandName="ChangeStatus" CssClass="btn btn-primary link-Button" CommandArgument='<%# Eval("Job_ID") %>' OnClick="lnkChangeStatus_Click">Change Status</asp:LinkButton>

                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div class="modal fade" id="changeStatusModal" tabindex="-1" role="dialog" aria-labelledby="changeStatusModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="changeStatusModalLabel">Change Job Status</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Text="Pending" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Occupied" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Vacant" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="txtJobID" Visible="false"></asp:TextBox>
                    <asp:Label runat="server" ID="lblMsg" ></asp:Label>
                    <asp:Button ID="btnUpdateStatus" runat="server" Text="Update" CssClass="btn btn-primary link-Button" OnClick="btnUpdateStatus_Click" />
                </div>
            </div>
        </div>
    </div>
    <script>
        function statusModal() {
            $(document).ready(function () {
                $('#changeStatusModal').modal('show');
            });
        }
    </script>
</asp:Content>
