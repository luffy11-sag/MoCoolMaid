<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="statistics.aspx.cs" Inherits="MoCoolMaid.Admin.statistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div id="PieChartContainer" class="container-fluid">
        <div class="main-text">
            <h2>Users Registered</h2>
        </div>
        <div class="row justify-content-center">
            <div class="col-md-6">
                <ajaxToolkit:PieChart ID="PieChart1" runat="server" CssClass="piechart" ChartHeight="650" ChartWidth="650" BorderStyle="None">
                    <PieChartValues>
                        <ajaxToolkit:PieChartValue Category="Housekeepers" Data="1" PieChartValueColor="#0E426C" />
                        <ajaxToolkit:PieChartValue Category="House Owners" Data="1" PieChartValueColor="#D08AD9" />
                    </PieChartValues>
                </ajaxToolkit:PieChart>
            </div>
        </div>
    </div>
    <div class="statsection container-fluid">
        <div class="horizontal-line"></div>
        <div class="main-text">
            <h2>General Web Statistics</h2>
        </div>
        <div class="row">
            <div class="col-md-3 statcard">
                <div class="card">
                    <div class="card-title">
                        <h4>Jobs Posted</h4>
                        <div class="horizontal-line"></div>
                    </div>
                    <div class="card-body">
                        <asp:Label runat="server" ID="lblJobsPosted" Text="" CssClass="stats"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-3 statcard">
                <div class="card">
                    <div class="card-title">
                        <h4>Application Submitted</h4>
                        <div class="horizontal-line"></div>
                    </div>
                    <div class="card-body">
                        <asp:Label runat="server" ID="lblAppSubmitted" Text="" CssClass="stats"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-3 statcard">
                <div class="card">
                    <div class="card-title">
                        <h4>Sucessfull Applications</h4>
                        <div class="horizontal-line"></div>
                    </div>
                    <div class="card-body">
                        <asp:Label runat="server" ID="lblAppSuccess" Text="" CssClass="stats"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-3 statcard">
                <div class="card">
                    <div class="card-title">
                        <h4>Blocked Users</h4>
                        <div class="horizontal-line"></div>
                    </div>
                    <div class="card-body">
                        <asp:Label runat="server" ID="lblUsersBlocked" Text="" CssClass="stats"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
