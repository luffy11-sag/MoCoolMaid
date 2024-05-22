<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewRatings.aspx.cs" Inherits="MoCoolMaid.viewRatings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid ratings">
        <div class="main-text-rating">
            <asp:Label runat="server" ID="lblHK" Text="" CssClass="rating-hk"></asp:Label>
        </div>
        <asp:ListView ID="lvComments" runat="server">
            <ItemTemplate>
                <div class="card mb-3 rating-card">
                    <div class="card-header">
                        <div class="viewrating-container">
                            <ajaxToolkit:Rating ID="Rating1" runat="server"
                                CurrentRating='<%# Eval("Rating_Num") %>'
                                ReadOnly="true"
                                StarCssClass="starRating"
                                FilledStarCssClass="FilledStars"
                                EmptyStarCssClass="EmptyStars"
                                WaitingStarCssClass="WaitingStars">
                            </ajaxToolkit:Rating>
                        </div>
                        <div class="date-container">
                            <small class="text-muted"><%# Eval("Rating_Date", "{0:MMMM dd, yyyy}") %></small>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="card-text"><%# Eval("Review") %></p>
                    </div>
                    <div class="posted-by">
                        <strong>Posted by: </strong>
                        <%# Eval("HoLastName") %> <%# Eval("HoFirstName") %>
                    </div>
                </div>
                <br />
                <br />
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
