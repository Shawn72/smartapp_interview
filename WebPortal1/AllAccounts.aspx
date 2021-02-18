<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllAccounts.aspx.cs" Inherits="AllAccounts" MasterPageFile="~/Site.master"  EnableEventValidation="false" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  
    <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                       
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item"><a href="#">Home</a></li>
                            <li class="breadcrumb-item active">Accounts Details</li>
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
                    <asp:GridView ID="gridViewAccounts" runat="server" CssClass="table table-hover" GridLines="None" AutoGenerateColumns="false" EmptyDataText = "No files available!">
                        <Columns>
                            <asp:BoundField DataField="account_number" HeaderText="Account Number" />
                            <asp:BoundField DataField="account_balance" HeaderText="Account Balance" /> 
                            <asp:BoundField DataField="account_status" HeaderText="Account Status" /> 
                            <asp:TemplateField HeaderText="Go Transact">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewAccount" Text = "Go Transact" CommandArgument = '<%# Eval("account_number") %>' runat="server" OnClick="lnkViewAccount_OnClick"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Recent Transactions">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewRecenttransactions" Text = "Recent Transactions" CommandArgument = '<%# Eval("account_number") %>' runat="server" OnClick="lnkViewRecenttransactions_OnClick"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Deactivate/Close">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewDeactivate" Text = "Deactivate/Close" CommandArgument = '<%# Eval("account_number") %>' runat="server" OnClick="lnkViewDeactivate_OnClick"  AutoPostBack="true" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                        </Columns>
                    </asp:GridView>
                </div>
             </div>
        </section>
    
</asp:Content>

