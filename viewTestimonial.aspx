<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewTestimonial.aspx.cs" Inherits="MoCoolMaid.viewTestimonial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid testi">
        <asp:ListView ID="lvTestimonials" runat="server" OnPagePropertiesChanging="lvTestimonials_PagePropertiesChanging">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-header">
                        <div class="viewpic-container">
                            <asp:Image ID="profilePic" runat="server" ImageUrl='<%# Eval("Testimonial_PP", "~/images/testimonial/{0}") %>' CssClass="card-image" />
                        </div>
                        <div class="date-container">
                            <small class="text-muted"><%# Eval("Testimonial_Date", "{0:MMMM dd, yyyy}") %></small>
                        </div>
                    </div>
                    <div class="card-body">
                        <p class="card-text"><%# Eval("Testimonial_Desc") %></p>
                    </div>
                    <div class="posted-by">
                        <strong>Posted by: </strong>
                        <%# Eval("User_FName") %> <%# Eval("User_LName") %>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvTestimonials" PageSize="3">
            <Fields>
                <asp:NumericPagerField ButtonCount="5" />
            </Fields>
        </asp:DataPager>
    </div>
</asp:Content>
