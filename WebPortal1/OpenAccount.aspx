<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpenAccount.aspx.cs" Inherits="OpenAccount" MasterPageFile="~/Registration.master" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

<div class="card">
    <div class="card-body register-card-body">
        <div id="register_feedback" runat="server"></div>
        <p class="login-box-msg">Submit details to open an account</p>

            <div class="input-group mb-3">
            
                <asp:TextBox runat="server" ID="txtFname" CssClass="form-control"  placeholder="first name"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-user"></span>
                    </div>
                </div>
            </div>
            
            <div class="input-group mb-3">
              
                <asp:TextBox runat="server" ID="txtMname" CssClass="form-control"  placeholder="mid name"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-user"></span>
                    </div>
                </div>
            </div>
            
            <div class="input-group mb-3">
              
                <asp:TextBox runat="server" ID="txtLname" CssClass="form-control"  placeholder="last name"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-user"></span>
                    </div>
                </div>
            </div>
            
            <div class="input-group mb-3">
               
                <asp:TextBox runat="server" ID="txtIDNumber" CssClass="form-control"  placeholder="id number"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-user"></span>
                    </div>
                </div>
            </div>
            
            <div class="input-group mb-3">
                <asp:TextBox runat="server" ID="txtPhoneNumber" CssClass="form-control"  placeholder="phone number" onblur="PhoneNoValidator(this)"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-user"></span>
                    </div>
                </div>
            </div>
                        
            <div class="input-group mb-3">
              
                <asp:TextBox runat="server" ID="txtAccountNumber" CssClass="form-control"  placeholder="account number"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-user"></span>
                    </div>
                </div>
            </div>

            <div class="input-group mb-3">
               
                <asp:TextBox  runat="server" type="email" ID="txtEmail" CssClass="form-control"  placeholder="email" onblur="EmailValidator(this)"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-envelope"></span>
                    </div>
                </div>
            </div>
            <div class="input-group mb-3">
               
                <asp:TextBox runat="server" TextMode="Password" ID="txtPassword1" CssClass="form-control"  placeholder="password"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-lock"></span>
                    </div>
                </div>
            </div>
            <div class="input-group mb-3">
              
                <asp:TextBox runat="server" TextMode="Password" ID="txtPassword2" CssClass="form-control"  placeholder="confirm password"></asp:TextBox>
                <div class="input-group-append">
                    <div class="input-group-text">
                        <span class="fas fa-lock"></span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-8">
                    <div class="icheck-primary">
                        <input type="checkbox" id="agreeTerms" name="terms" value="agree"/>
                        <label for="agreeTerms">
                            I agree to the <a href="#">terms</a>
                        </label>
                    </div>
                </div>
                <!-- /.col -->
                <div class="col-4">
                    <asp:Button runat="server" ID="btnRegister" CssClass="btn btn-primary btn-block" Text="Register" OnClick="btnRegister_OnClick"/>
                </div>
                <!-- /.col -->
            </div>    
      

        <a href="Login.aspx" class="text-center">I already have anAccount</a>
    </div>
    <!-- /.form-box -->
</div><!-- /.card -->
    

</asp:Content>

