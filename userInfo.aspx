<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="userInfo.aspx.cs" Inherits="MoCoolMaid.userInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container">
        <div class="main-text justify-content-center d-flex">
            <h3>My Account</h3>
        </div>
        <asp:Panel runat="server" ID="pnlProfilePic" Visible="False">
            <div class="card mb-3">
                <div class="card-body profile-section text-center">
                    <asp:Image ID="profilePic" runat="server" CssClass="profile-picture" ImageUrl="path_to_image.jpg" />
                    <div class="profile-btn-group">
                        <a data-bs-toggle="modal" href="#editPicModal">Edit Picture <i class="fa-solid fa-pen-to-square icon"></i></a>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="card mb-3">
            <div class="card-body info-section">
                <div class="info-group">
                    <h2>User Info</h2>
                    <div class="info-item">
                        <label for="lblUsername" class="lblUserInf">Username:</label>
                        <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                        <a href="/editUsername.aspx"><i class="fa-solid fa-pen-to-square icon"></i></a>
                    </div>
                    <div class="info-item">
                        <label for="lblEmail">Email:</label>
                        <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                        <div class="btn-group">
                            <asp:Button ID="resetPasswordBtn" runat="server" Text="Reset Password" CssClass="reset-btn" OnClick="resetPasswordBtn_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="card mb-3">
            <div class="card-body info-section">
                <div class="info-group">
                    <h2>Contact Details</h2>
                    <div class="info-item">
                        <label for="lblLocation">Location:</label>
                        <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
                        <a data-bs-toggle="modal" href="#editLocationModal"><i class="fa-solid fa-pen-to-square icon"></i></a>
                    </div>
                    <div class="info-item">
                        <label for="lblPhone">Phone:</label>
                        <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                        <a data-bs-toggle="modal" href="#editPhoneModal"><i class="fa-solid fa-pen-to-square icon"></i></a>
                    </div>
                </div>
            </div>
        </div>

        <asp:Panel runat="server" ID="pnlCV" Visible="false">
            <div class="card mb-3">
                <div class="card-body cv-section">
                    <div class="info-group">
                        <h2>CV Section</h2>
                        <a id="cvLink" class="cvLink" runat="server" target="_blank">View CV</a>
                        <a id="updateCV" class="cvLink" runat="server" data-bs-toggle="modal" href="#editCVModal">Update CV</a>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <div class="modal fade" id="editLocationModal" tabindex="-1" role="dialog" aria-labelledby="editLocationModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editLocationModalLabel">Edit Location</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <asp:Button ID="btnUpdateLocation" runat="server" Text="Save" CssClass="btn btn-primary link-Button" OnClick="btnUpdateLocation_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function editLocationModal() {
            $(document).ready(function () {
                $('#editLocationModal').modal('show');
            });
        }
    </script>

    <div class="modal fade" id="editPhoneModal" tabindex="-1" role="dialog" aria-labelledby="editPhoneModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editPhoneModalLabel">Edit Phone</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                    <asp:Label runat="server" ID="lblMsgPhone"></asp:Label>
                    <asp:Button ID="btnUpdatePhone" runat="server" Text="Save" CssClass="btn btn-primary link-Button" OnClick="btnUpdatePhone_Click" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function editPhoneModal() {
            $(document).ready(function () {
                $('#editPhoneModal').modal('show');
            });
        }
    </script>

    <div class="modal fade" id="editPicModal" tabindex="-1" role="dialog" aria-labelledby="editPicModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editPicModalLabel">Edit Profile Picture</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label for="fuPP">Upload a Profile Picture:</label>
                    <asp:FileUpload ID="fuPP" runat="server" CssClass="form-control" />
                    <asp:Label runat="server" ID="Label1"></asp:Label>
                    <asp:Button ID="btnUpdatePic" runat="server" Text="Save" CssClass="btn btn-primary link-Button" OnClick="btnUpdatePic_Click" />
                    <asp:Label runat="server" ID="lblUpdatePicMsg" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <script>
        function editPicModal() {
            $(document).ready(function () {
                $('#editPicModal').modal('show');
            });
        }
    </script>

    <div class="modal fade" id="editCVModal" tabindex="-1" role="dialog" aria-labelledby="editCVModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editCVModalLabel">Update CV</h5>
                    <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <label for="fuPP">Update CV:</label>
                    <asp:FileUpload ID="fuCV" runat="server" CssClass="form-control" />
                    <asp:Label runat="server" ID="lblmsgCV"></asp:Label>
                    <asp:Button ID="btnUpdateCV" runat="server" Text="Save" CssClass="btn btn-primary link-Button" OnClick="btnUpdateCV_Click" />
                </div>
            </div>
        </div>
    </div>
    <script>
        function editCVModal() {
            $(document).ready(function () {
                $('#editCVModal').modal('show');
            });
        }
    </script>
</asp:Content>
