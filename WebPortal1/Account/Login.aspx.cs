using System;
using System.Web;
using System.Web.UI;

public partial class Account_Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register";
        OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

        var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        if (!String.IsNullOrEmpty(returnUrl))
        {
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }
    }
}