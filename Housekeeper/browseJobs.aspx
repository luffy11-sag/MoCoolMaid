<%@ Page Title="Browse Jobs" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="browseJobs.aspx.cs" Inherits="MoCoolMaid.Housekeeper.browseJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="main-text1">
            <h2>Browse All Jobs</h2>
        </div>
        <asp:ListView ID="lvJobs" runat="server">
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

                            <asp:Button ID="btnApply" runat="server" CommandName="Apply" CommandArgument='<%# Eval("Job_ID") %>' Text="Apply" CssClass="btnApply btn btn-primary" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
