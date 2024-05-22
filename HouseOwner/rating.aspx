<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="rating.aspx.cs" Inherits="MoCoolMaid.HouseOwner.rating" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="rating-box">
    <div class="rating-container">
        <h2 style="font-family: Montserrat; text-align: center;">Rate and Review</h2>
        <div class="rating-field">
            <label for="Rating1">Rating:</label>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <ajaxToolkit:Rating ID="Rating1" runat="server"
                        StarCssClass="starRating"
                        FilledStarCssClass="FilledStars"
                        EmptyStarCssClass="EmptyStars"
                        WaitingStarCssClass="WaitingStars">                           
                    </ajaxToolkit:Rating>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="rating-field">
            <label for="txtReview">Review:</label>
            <asp:TextBox runat="server" ID="txtReview" CssClass="review-field" TextMode="MultiLine" />
        </div>
        <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="submit-button" OnClick="btnSubmit_Click" />
    </div>
</div>
</asp:Content>
