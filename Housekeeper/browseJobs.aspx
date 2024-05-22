<%@ Page Title="Browse Jobs" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="browseJobs.aspx.cs" Inherits="MoCoolMaid.Housekeeper.browseJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="main-text1">
            <h2>Browse All Jobs</h2>
        </div>

        <div class="card filterHK">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <label for="txtSearch">Search:</label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control filterSearch" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" Placeholder="Search by House owner name...." />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="ddlDistrict">District:</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control filterddl" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label for="ddlGender">Salary:</label>
                        <asp:DropDownList ID="ddlSalary" runat="server" CssClass="form-control filterddl" AutoPostBack="true">
                            <asp:ListItem Text="Select Salary" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Above Rs.10000" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Above Rs.20000" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Above Rs.25000" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4">
                        <label for="ddlCategory">Category:</label>
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control filterddl" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvJobs" runat="server" DataKeyNames="Job_ID" AllowPaging="true" OnPagePropertiesChanging="lvJobs_PagePropertiesChanging">
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
                                    <div class="card-line"></div>
                                    <p class="card-text"><strong>Description:</strong> <%# Eval("Job_Desc") %></p>
                                    <p class="card-text"><strong>Salary: </strong>Rs. <%# Eval("Job_Salary") %></p>
                                    <p class="card-text"><strong>Location:</strong> <%# Eval("District") %></p>
                                    <p class="card-text"><strong>Deadline:</strong> <%# Eval("Job_Deadline") %></p>
                                    <p class="card-text"><strong>Posted By:</strong> <%# Eval("User_LName") %> <%# Eval("User_FName") %></p>

                                    <asp:LinkButton ID="lnkApply" runat="server" CommandName="Apply" CommandArgument='<%# Eval("Job_ID") %>' Text="Apply" CssClass="btnApply btn btn-primary" OnClick="lnkApply_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlSalary" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvJobs" PageSize="12">
            <Fields>
                <asp:NumericPagerField ButtonCount="5" />
            </Fields>
        </asp:DataPager>
        <div class="modal fade" id="messageModal" tabindex="-1" role="dialog" aria-labelledby="messageModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="messageModalLabel">Application</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="lblmsg" Text="Already Applied for this Job"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="messageDeadlineModal" tabindex="-1" role="dialog" aria-labelledby="messageDeadlineModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="messageDealineModalLabel">Application</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="lblMsgDeadline" Text="Deadline date for this job has passed!"></asp:Label>
                    </div>
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

    <script>
        function viewMessageDeadlineModal() {
            $(document).ready(function () {
                $('#messageDeadlineModal').modal('show');
            });
        }
    </script>
</asp:Content>
