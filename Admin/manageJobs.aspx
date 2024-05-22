<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="manageJobs.aspx.cs" Inherits="MoCoolMaid.Admin.manageJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
    <div class="main-text1">
        <h2>Browse All Jobs</h2>
    </div>
    <asp:Label runat="server" ID="lblMsg" CssClass="JobsDelMsg" ></asp:Label>
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
                        <p class="card-text"><strong>Posted By:</strong> <%# Eval("User_LName") %> <%# Eval("User_FName") %></p>

                        <asp:LinkButton ID="lnkDeleteJobs" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Job_ID") %>' CssClass="btn btn-primary link-Button" OnClick="lnkDeleteJobs_Click" OnClientClick="return confirm('Are you sure you want to delete job?')" >Delete</asp:LinkButton>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvJobs" PageSize="8" >
        <Fields>
            <asp:NumericPagerField ButtonCount="5" />         
        </Fields>
    </asp:DataPager>
</div>
</asp:Content>
