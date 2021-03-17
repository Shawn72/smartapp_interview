<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecentInvestmentTransactions.aspx.cs" Inherits="RecentInvestmentTransactions" MasterPageFile="~/Site.master" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <p>Transactions for Account:  <asp:TextBox runat="server" ID="txtAccountNum" ReadOnly="True" CssClass="form-control" ></asp:TextBox></p> 
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Accounts Transactions</li>
                    </ol>
                </div>
            </div>
        </div><!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div id="trx_feedback" runat="server"></div>
            <div class="card-body table-responsive p-0">	
                <asp:GridView ID="gridViewTransactions" runat="server" CssClass="table table-hover" GridLines="None" AutoGenerateColumns="false" EmptyDataText = "No files available!">
                    <Columns>
                        <asp:BoundField DataField="investment_description" HeaderText="Transaction Description" />
                        <asp:BoundField DataField="transaction_value" HeaderText="Transaction Amount" /> 
                        <asp:BoundField DataField="transaction_date" HeaderText="Transaction Date/Time" /> 
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <a href="Investments.aspx" ><< Back</a>
    </section>
    
</asp:Content>
