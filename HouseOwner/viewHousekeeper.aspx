<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="viewHousekeeper.aspx.cs" Inherits="MoCoolMaid.HouseOwner.viewHousekeeper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container-fluid">
        <div class="main-text1">
            <h2>Registered Housekeepers</h2>
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
                <asp:ListView ID="lvHK" runat="server" DataKeyNames="HK_ID" OnSelectedIndexChanged="lvHK_SelectedIndexChanged" OnPagePropertiesChanging="lvHK_PagePropertiesChanging">
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
                                    <p class="card-text"><strong>Location:</strong> <%# Eval("District") %></p>

                                    <asp:LinkButton ID="lnkViewProfile" runat="server" CommandName="ViewHK" CommandArgument='<%# Eval("HK_ID") %>' CssClass="btn btn-primary link-Button" OnClick="lnkViewProfile_Click">View Profile</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
                <div class="modal fade" id="housekeeperModal" tabindex="-1" role="dialog" aria-labelledby="housekeeperModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="housekeeperModalLabel">Housekeeper Details</h5>
                                <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:Image ID="profilePic" runat="server" CssClass="card-image" />
                                <br />
                                <br />
                                <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblFirstName" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <a id="cvLink" class="cvLink" runat="server" href="#" target="_blank">View CV</a>
                                <br />
                                <asp:HiddenField ID="hfUserID" runat="server" />
                                <asp:LinkButton runat="server" ID="lbtnViewRating" Text="View Ratings" CssClass="btn btn-outline-info linkButton" CommandArgument='<%# Eval("HK_ID") %>' OnClick="lbtnViewRating_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlGender" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvHK" PageSize="8">
            <Fields>
                <asp:NumericPagerField ButtonCount="5" />
            </Fields>
        </asp:DataPager>

    </div>

    <script>
        function viewHKModal() {
            $(document).ready(function () {
                $('#housekeeperModal').modal('show');
            });
        }
    </script>
</asp:Content>
