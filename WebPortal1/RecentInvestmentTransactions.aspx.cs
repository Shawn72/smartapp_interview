using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class RecentInvestmentTransactions : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString = new DBConfig().MysqLConnector();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInvestmentTransactions();
        }
    }
    protected void LoadInvestmentTransactions()
    {
        MySqlConnection con = (dynamic)null;
        var accId = Request.QueryString["accNo"];
        var accountNumber = (dynamic) null;

        try
        {
            using (con = new MySqlConnection(ConString))
            {

                string checkifAccountExists = "SELECT * FROM user_bank_details WHERE id = @account_id LIMIT 1";
                //check if user exists first
                
                con.Open();
                MySqlCommand commandXt = new MySqlCommand(checkifAccountExists, con);

                //use parametarized queries to prevent sql injection
                commandXt.Parameters.AddWithValue("@account_id", Convert.ToInt32(accId));

                int accountIsThere = (int)commandXt.ExecuteScalar();

                if (accountIsThere >= 1)
                {
                    DataTable dtb = new DataTable();
                    MySqlDataAdapter dab = new MySqlDataAdapter(commandXt);
                    dab.Fill(dtb);
                    foreach (DataRow dr in dtb.Rows)
                    {
                        accountNumber = dr["account_number"];
                    }
                }


                string selectQuery =
                    "SELECT i.investment_description, itrx.transaction_value, itrx.investment_transaction_type, itrx.transaction_date FROM user_bank_details a, investments i, investment_transactions itrx  " +
                    "WHERE i.id = itrx.investment_id AND a.id =itrx.user_account_id AND itrx.user_account_id=@AccountId ORDER BY itrx.transaction_date DESC";
       
                MySqlCommand command0 = new MySqlCommand(selectQuery, con);

                //use parametarized queries to prevent sql injection
                command0.Parameters.AddWithValue("@AccountId", Convert.ToInt32(accId));
                command0.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(command0);
                da.Fill(dt);
                gridViewTransactions.DataSource = dt;
                gridViewTransactions.DataBind();
                txtAccountNum.Text = accountNumber;
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
}