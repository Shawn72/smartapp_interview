using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class RecentTransactions : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString = @"datasource=localhost;port=3306;username=root;password=root;database=smartapp_db";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGeneralTransactions();
        }
    }
    protected void LoadGeneralTransactions()
    {
        MySqlConnection con = (dynamic)null;
       // var userId = Convert.ToInt32(Session["userid"]);
        var accNum = Request.QueryString["accNo"];

        try
        {
            using (con = new MySqlConnection(ConString))
            {
                string selectQuery =
                    "SELECT transaction_description, transaction_amount, transaction_type, transaction_date FROM general_transactions WHERE source_account=@AccountNo ORDER BY transaction_date DESC";
                con.Open();
                MySqlCommand command0 = new MySqlCommand(selectQuery, con);

                //use parametarized queries to prevent sql injection
                command0.Parameters.AddWithValue("@AccountNo", accNum);
                command0.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(command0);
                da.Fill(dt);
                gridViewTransactions.DataSource = dt;
                gridViewTransactions.DataBind();
                txtAccountNum.Text = accNum;
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