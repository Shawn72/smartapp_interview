using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class AllAccounts : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString =
        @"datasource=localhost;port=3306;username=root;password=root;database=smartapp_db";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string) Session["email"] == null)
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
        MySqlConnection con = (dynamic) null;
        var userId = Convert.ToInt32(Session["userid"]);
        try
        {
            using (con = new MySqlConnection(ConString))
            {
                string selectQuery =
                    "SELECT account_number, account_name, account_balance, account_status FROM user_bank_details WHERE user_id=@UserId ";
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

    protected void lnkViewAccount_OnClick(object sender, EventArgs e)
    {
        var linkButton = sender as LinkButton;
        string accountNumber = "";
        if (linkButton != null)
        {
            accountNumber = linkButton.CommandArgument;
        }
        Response.Redirect("AccountTransactions.aspx?accNo=" + accountNumber);
    }

    protected void lnkViewRecenttransactions_OnClick(object sender, EventArgs e)
    {
        var linkButton = sender as LinkButton;
        string accountNumber = "";
        if (linkButton != null)
        {
            accountNumber = linkButton.CommandArgument;
        }
        Response.Redirect("RecentTransactions.aspx?accNo=" + accountNumber);
    }

    protected void lnkViewDeactivate_OnClick(object sender, EventArgs e)
    {
        var linkButton = sender as LinkButton;
        string accountNumber = "";
        if (linkButton != null)
        {
            accountNumber = linkButton.CommandArgument;
            MySqlConnection con = (dynamic)null;

            try
            {

                using (con = new MySqlConnection(ConString))
                {
                    //account exist, now update it with deposit
                    string updateQry =
                        "UPDATE user_bank_details SET account_status = @accountstat WHERE account_number=@account_number";
                    con.Open();
                    MySqlCommand commandUpdt = new MySqlCommand(updateQry, con);

                    // assign both transaction object and connection to Command object for a pending local transaction
                    commandUpdt.Connection = con;

                    //use parametarized queries to prevent sql injection
                    commandUpdt.Parameters.AddWithValue("@accountstat", "inactive");
                    commandUpdt.Parameters.AddWithValue("@account_number", accountNumber);

                    if (commandUpdt.ExecuteNonQuery() == 1)
                    {
                        //update is success, now update transactions table
                        trx_feedback.InnerHtml =
                            "<div class='alert alert-success'>Account Deactivated /Closed successfully!</div>";

                    }

                }

            }
            catch (MySqlException ex)
            {

                trx_feedback.InnerHtml = "<div class='alert alert-danger'>deactivate not success due to this error: " +
                                         ex.Message + "</div>";
            }
            finally
            {
                con.Close();
            }
        }
        
    }

    protected void DeactivateAccount(string accountNum)
    {
       
        MySqlConnection con = (dynamic) null;

        try
        {

            using (con = new MySqlConnection(ConString))
            {
                //account exist, now update it with deposit
                string updateQry =
                    "UPDATE user_bank_details SET account_status = @accountstat WHERE account_number=@account_number";
                con.Open();
                MySqlCommand commandUpdt = new MySqlCommand(updateQry, con);
              
                // assign both transaction object and connection to Command object for a pending local transaction
                commandUpdt.Connection = con;

                //use parametarized queries to prevent sql injection
                commandUpdt.Parameters.AddWithValue("@accountstat", "inactive");
                commandUpdt.Parameters.AddWithValue("@account_number", accountNum);

                if (commandUpdt.ExecuteNonQuery() == 1)
                {
                    //update is success, now update transactions table
                    trx_feedback.InnerHtml =
                        "<div class='alert alert-success'>Account Deactivated /Closed successfully!</div>";

                }

            }

        }
        catch (MySqlException ex)
        {
           
            trx_feedback.InnerHtml = "<div class='alert alert-danger'>deactivate not success due to this error: " +
                                     ex.Message + "</div>";
        }
        finally
        {
            con.Close();
        }

    }
}