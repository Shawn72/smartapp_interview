using System;
using System.Web.UI;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string) Session["email"] == null)
        {
            Response.Redirect("Login.aspx");
        }

    }
}