using System;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["email"] = "";
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Login.aspx");
    }
}