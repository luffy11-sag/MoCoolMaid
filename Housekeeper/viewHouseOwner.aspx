<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewHouseOwner.aspx.cs" Inherits="MoCoolMaid.Housekeeper.viewHouseOwner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="main-text1">
            <h2>Registered House Owners</h2>
        </div>
        <div class="card filterHK">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <label for="txtSearch">Search:</label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control filterSearch" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label for="ddlDistrict">District:</label>
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control filterddl" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-md-6">
                        <label for="ddlGender">Gender:</label>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control filterddl" AutoPostBack="true">
                            <asp:ListItem Text="Select Gender" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvHO" runat="server" DataKeyNames="HO_ID" OnPagePropertiesChanging="lvHO_PagePropertiesChanging">
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
                                    <p class="card-text"><strong>Location:</strong> <%# Eval("District") %></p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlGender" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvHO" PageSize="12">
            <Fields>
                <asp:NumericPagerField ButtonCount="5" />
            </Fields>
        </asp:DataPager>
    </div>
</asp:Content>
