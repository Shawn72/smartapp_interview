<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewAccount.aspx.cs" Inherits="AddNewAccount" MasterPageFile="~/Site.master"%>

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
                    <li class="breadcrumb-item active">Add Account </li>
                </ol>
            </div>
        </div>
    </div><!-- /.container-fluid -->
</section>

<!-- Main content -->
<section class="content">
<div class="container-fluid">

<div class="row">
    <div id="add_acc_feedback" runat="server"></div>
<!-- left column -->
<div class="col-md-6">
    <!-- general form elements -->
    <div class="card card-primary">
        <div class="card-header">
            <h3 class="card-title">Add account </h3>
        </div>
        <!-- /.card-header -->
        <!-- form start -->
        <div class="card-body">
            <div class="form-group">
                <label for="">Account Number</label>
                <asp:TextBox runat="server" ID="txtaccountNumber"  CssClass="form-control" placeholder="account number" ></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="">Account Balance</label>
                <asp:TextBox runat="server" ID="txtAccountBalance"  CssClass="form-control" onkeypress="return isNumber(event)" placeholder="account balance"></asp:TextBox>
            </div>
           
        </div>
        <!-- /.card-body -->

        <div class="card-footer">
            <asp:Button runat="server" ID="btnAddNewAcc"  Text="Add Account" OnClick="btnAddNewAcc_OnClick" CssClass="btn btn-primary btn-block"/>
        </div>
    </div>
    <!-- /.card -->
</div>
</div>
    
</div>
    
  </section>
</asp:Content>