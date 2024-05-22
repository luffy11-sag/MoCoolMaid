<%@ Page Title="Home" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="MoCoolMaid.home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="masthead">
        <div class="color-overlay d-flex justify-content-center align-items-center">
            <h1>Where Cleanliness Meets Convenience</h1>
        </div>
    </div>

    <section>
        <div class="container">
            <asp:Panel runat="server" ID="pnlJobsAvailable">
                <div class="horizontal-line"></div>
                <div class="row d-flex justify-content-center">
                    <div class="col-md-10 col-xl-8 text-center">
                        <h3 class="mb-4">Jobs Available</h3>
                        <p class="mb-4 pb-2 mb-md-5 pb-md-0">
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Fugit, error amet
numquam iure provident voluptate esse quasi, veritatis totam voluptas nostrum
quisquam eum porro a pariatur veniam.
                        </p>
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
                                <div class="col-md-4">
                                    <label for="ddlDistrict">District:</label>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control filterddl" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label for="ddlGender">Salary:</label>
                                    <asp:DropDownList ID="ddlSalary" runat="server" CssClass="form-control filterddl" AutoPostBack="true">
                                        <asp:ListItem Text="Select Salary" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Above Rs.10000" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Above Rs.20000" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Above Rs.25000" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4">
                                    <label for="ddlCategory">Category:</label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control filterddl" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="lblmsg" Text=""></asp:Label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:ListView ID="lvJobs" runat="server" DataKeyNames="Job_ID" AllowPaging="true" OnPagePropertiesChanging="lvJobs_PagePropertiesChanging">
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

                                                <asp:LinkButton ID="lnkApply" runat="server" CommandName="Apply" CommandArgument='<%# Eval("Job_ID") %>' Text="Apply" CssClass="btnApply btn btn-primary" OnClick="lnkApply_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSalary" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtSearch" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvJobs" PageSize="4">
                        <Fields>
                            <asp:NumericPagerField ButtonCount="5" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </asp:Panel>
        </div>
    </section>
    <section id="testimonials">
        <div class="container-fluid testi">
            <div class="main-text">
                <h2>Testimonials</h2>
            </div>
            <div id="carouselTestimonials" class="carousel slide" data-bs-ride="carousel">
                <div class="carousel-inner">
                    <asp:Repeater ID="rptTestimonials" runat="server">
                        <ItemTemplate>
                            <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
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
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselTestimonials" data-bs-slide="prev" style="color: black;">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselTestimonials" data-bs-slide="next" style="color: black;">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
            <div class="viewTestiBtn">
                <a href="/viewTestimonial" class="viewTesti">View More</a>
                <a href="/testimonials" class="viewTesti">Write Testimonial</a>
            </div>
        </div>
    </section>
</asp:Content>
