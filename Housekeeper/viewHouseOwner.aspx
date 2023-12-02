<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewHouseOwner.aspx.cs" Inherits="MoCoolMaid.Housekeeper.viewHouseOwner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
    <div class="main-text1">
        <h2>Registered House Owners</h2>
    </div>
    <asp:ListView ID="lvHO" runat="server" DataKeyNames="HO_ID" >
        <LayoutTemplate>
            <div class="row">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="job-card">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title"><%# Eval("User_LName") %> <%# Eval("User_FName") %></h5>
                        <div class="card-line"></div>
                        <p class="card-text"><strong>Email:</strong> <%# Eval("User_Email") %></p>   
                        <p class="card-text"><strong>Phone:</strong> <%# Eval("User_Phone") %></p>
                        <p class="card-text"><strong>Location:</strong> <%# Eval("User_City") %></p>                      
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
</div>
</asp:Content>
