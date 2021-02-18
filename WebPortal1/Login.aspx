<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/Login.master"%>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<div class="login-box">
    <!-- /.login-logo -->
    <div class="card">
        <div class="card-body login-card-body">
            <div id="login_feedback" runat="server"></div>
            <p class="login-box-msg">Sign in to start your session</p>
                <div class="input-group mb-3">
                   
                    <asp:TextBox runat="server" type="email" ID="txtEmail" CssClass="form-control"  placeholder="email address"></asp:TextBox>
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-envelope"></span>
                        </div>
                    </div>
                </div>
                <div class="input-group mb-3">
                    <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" CssClass="form-control"  placeholder="password"></asp:TextBox>
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-8">
                        <div class="icheck-primary">
                            <input type="checkbox" id="remember"/>
                            <label for="remember">
                                Remember Me
                            </label>
                        </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-4">
                        <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-primary btn-block" Text="Login" OnClick="btnLogin_OnClick"/>
                    </div>
                    <!-- /.col -->
                </div>

            <p class="mb-1">
                <a href="forgot-password.html">I forgot my password</a>
            </p>
            <p class="mb-0">
                <a href="OpenAccount.aspx" class="text-center">Register a new membership</a>
            </p>
        </div>
        <!-- /.login-card-body -->
    </div>
</div>
<!-- /.login-box -->
 </asp:Content>


