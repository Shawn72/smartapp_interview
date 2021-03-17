using System;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class Investments : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString = new DBConfig().MysqLConnector();
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["email"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            LoadMyBankAccounts();
        }
    }
    protected void LoadMyBankAccounts()
    {
        MySqlConnection con = (dynamic)null;
        var userId = Convert.ToInt32(Session["userid"]);
        try
        {
            using (con = new MySqlConnection(ConString))
            {
                string selectQuery =
                    "SELECT id, account_number, account_name, account_balance, account_status FROM user_bank_details WHERE user_id=@UserId ";
                con.Open();
                MySqlCommand command0 = new MySqlCommand(selectQuery, con);

                //use parametarized queries to prevent sql injection
                command0.Parameters.AddWithValue("@UserId", userId);
                command0.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(command0);
                da.Fill(dt);
                gridViewAccounts.DataSource = dt;
                gridViewAccounts.DataBind();
            }
        }
        catch (MySqlException ex)
        {

        }
        finally
        {
            con.Close();
        }
    }
    protected void lnkViewRecenttransactions_OnClick(object sender, EventArgs e)
    {
        var linkButton = sender as LinkButton;
        string accountNumber = "";
        if (linkButton != null)
        {
            accountNumber = linkButton.CommandArgument;
        }
        Response.Redirect("RecentInvestmentTransactions.aspx?accNo=" + accountNumber);
    }
}