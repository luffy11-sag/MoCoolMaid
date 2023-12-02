<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewPostedJobs.aspx.cs" Inherits="MoCoolMaid.HouseOwner.viewPostedJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
    <div class="main-text1">
        <h2>Jobs You Posted</h2>
    </div>
    <asp:ListView ID="lvPostedJobs" runat="server" DataKeyNames="Job_ID" >
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

                        <asp:LinkButton ID="lnkUpdateJob" runat="server" CommandName="Edit" CssClass="btn btn-primary link-Button" CommandArgument='<%# Eval("Job_ID") %>' OnClick="lnkUpdateJob_Click" >Edit</asp:LinkButton>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
</div>
</asp:Content>
