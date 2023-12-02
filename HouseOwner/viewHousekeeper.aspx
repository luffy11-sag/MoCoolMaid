<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewHousekeeper.aspx.cs" Inherits="MoCoolMaid.HouseOwner.viewHousekeeper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="main-text1">
            <h2>Registered Housekeepers</h2>
        </div>
        <asp:ListView ID="lvHK" runat="server" DataKeyNames="HK_ID" OnSelectedIndexChanged="lvHK_SelectedIndexChanged">
            <LayoutTemplate>
                <div class="row">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </div>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="job-card">
                    <div class="card">
                        <div class="card-body">
                            <asp:Image ID="profilePic" runat="server" ImageUrl='<%# Eval("HK_PP", "~/images/{0}") %>' CssClass="card-image" />
                            <h5 class="card-title"><%# Eval("User_LName") %> <%# Eval("User_FName") %></h5>
                            <p class="card-text"><strong>Location:</strong> <%# Eval("User_City") %></p>

                            <asp:LinkButton ID="lnkViewProfile" runat="server" CommandName="ViewHK" CssClass="btn btn-primary link-Button" OnClick="lnkViewProfile_Click">View Profile</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
