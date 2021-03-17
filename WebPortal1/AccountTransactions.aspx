<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountTransactions.aspx.cs" Inherits="AccountTransactions" MasterPageFile="~/Site.master"  EnableEventValidation="false" %>

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
                    <li class="breadcrumb-item active">Accounts Transactions</li>
                </ol>
            </div>
        </div>
    </div><!-- /.container-fluid -->
</section>

<!-- Main content -->
<section class="content">
    <div class="container-fluid">
    
     <div class="row">
       
         <div class="col-md-12">
             <div id="trx_feedback" runat="server"></div>
         <!-- general form elements -->
         <div class="card card-primary">
             <div class="card-header">
             </div>
             <!-- /.card-header -->
             <!-- form start -->
             <div class="form-group">
                 <label for="">Account Number</label>
                 <asp:TextBox runat="server" ID="txtAccountNum" ReadOnly="True" CssClass="form-control" ></asp:TextBox>
             </div>
             <div class="form-group">
                 <label for="">Account Balance (KES)</label>
                 <asp:TextBox runat="server" ID="txtAccountBalance" ReadOnly="True" CssClass="form-control" ></asp:TextBox>
             </div>
             <!-- /.card-body -->

             <div class="card-footer">
                 
             </div>
         </div>
         <!-- /.card -->
         </div>
     </div>

    <div class="row">
    <!-- left column -->
    <div class="col-md-6">
        <!-- general form elements -->
        <div class="card card-primary">
            <div class="card-header">
                <h3 class="card-title">Deposit </h3>
            </div>
            <!-- /.card-header -->
            <!-- form start -->
                <div class="card-body">
                    <div class="form-group">
                        <label for="">Amount to Deposit</label>
                        <asp:TextBox runat="server" ID="txtAmountDeposited"  CssClass="form-control" onkeypress="return isNumber(event)" ></asp:TextBox>
                    </div>
                </div>
                <!-- /.card-body -->

                <div class="card-footer">
                    <asp:Button runat="server" ID="btnDeposit"  Text="Deposit" OnClick="btnDeposit_OnClick" CssClass="btn btn-primary btn-block"/>
                </div>
        </div>
        <!-- /.card -->
    </div>

    <!-- right column -->
    <div class="col-md-6">
        <!-- general form elements -->
        <div class="card card-primary">
            <div class="card-header">
                <h3 class="card-title"> Withdraw </h3>
            </div>
            <!-- /.card-header -->
            <!-- form start -->
            <div class="card-body">
               <div class="form-group">
                    <label for="">Amount to Withdraw</label>
                    <asp:TextBox runat="server" ID="txtAmounttoWithdraw" CssClass="form-control" ></asp:TextBox>
                </div>
            </div>
            <!-- /.card-body -->

            <div class="card-footer">
                <asp:Button runat="server" ID="btnWithdraw" CssClass="btn btn-primary btn-block" Text="Withdraw" OnClick="btnWithdraw_OnClick"/>
            </div>
        </div>
        <!-- /.card -->
    </div>
   </div>

    <div class="row">
        <!-- left column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Pay Bills </h3>
                </div>
                <!-- /.card-header -->
                <!-- form start -->
                <div class="card-body">
                    <div class="form-group">
                        <label for="">Select Bill</label>
                        <asp:DropDownList runat="server" ID="ddlBills"  CssClass="form-control"/>
                    </div>

                    <div class="form-group">
                        <label for="">Amount to Pay</label>
                        <asp:TextBox runat="server" ID="txtBillCost"  CssClass="form-control" onkeypress="return isNumber(event)" ></asp:TextBox>
                    </div>
                </div>
                <!-- /.card-body -->

                <div class="card-footer">
                    <asp:Button runat="server" ID="btnPayBill"  Text="Pay Bill" OnClick="btnPayBill_OnClick" CssClass="btn btn-primary btn-block"/>
                </div>
            </div>
            <!-- /.card -->
        </div>
        
        <!-- right column -->
        <div class="col-md-6">
            <!-- general form elements -->
            <div class="card card-primary">
                <div class="card-header">
                    <h3 class="card-title">Pay for Investments </h3>
                </div>
                <!-- /.card-header -->
                <!-- form start -->
                <div class="card-body">
                    <div class="form-group">
                        <label for="">Select Investment</label>
                        <asp:DropDownList runat="server" ID="ddlInvestment"  CssClass="form-control"/>
                    </div>

                    <div class="form-group">
                        <label for="">Amount to Pay</label>
                        <asp:TextBox runat="server" ID="txtPayInvestment"  CssClass="form-control" onkeypress="return isNumber(event)" ></asp:TextBox>
                    </div>
                </div>
                <!-- /.card-body -->

                <div class="card-footer">
                    <asp:Button runat="server" ID="btnPayInvestment"  Text="Pay Investment" OnClick="btnPayInvestment_OnClick" CssClass="btn btn-primary btn-block"/>
                </div>
            </div>
            <!-- /.card -->
        </div>

    </div>
        <a href="AllAccounts.aspx" ><< Back</a>

    </div>
</section>
    

 </asp:Content>

