using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class AccountTransactions : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString = @"datasource=localhost;port=3306;username=root;password=root;database=smartapp_db";

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((string)Session["email"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            FetchAccountDetailsonLoad();
            PopulateBillsDdl();
            PopulateInvestmentsDdl();
        }
    }

    protected void FetchAccountDetailsonLoad()
    {
        var accountNo = Request.QueryString["accNo"];
        MySqlConnection con = (dynamic)null;

        try
        {

            //now add supplied data to dB
            using (con = new MySqlConnection(ConString))
            {

                string checkifAccountExists =
                    "SELECT * FROM user_bank_details WHERE account_number = @accountnumber LIMIT 1";
                //check if user exists first
                con.Open();
                MySqlCommand commandXt = new MySqlCommand(checkifAccountExists, con);

                //use parametarized queries to prevent sql injection
                commandXt.Parameters.AddWithValue("@accountnumber", accountNo);

                int accountIsThere = (int) commandXt.ExecuteScalar();

                if (accountIsThere > 0)
                {
                    //get account_id, user_id
                    var accountbalance = (dynamic) null;
                    var activeornot = (dynamic)null;
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(commandXt);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        accountbalance = dr["account_balance"];
                        activeornot = dr["account_status"];
                    }
                    txtAccountNum.Text = accountNo;
                    txtAccountBalance.Text = accountbalance.ToString();
                    if (activeornot == "inactive")
                    {
                        trx_feedback.InnerHtml = "<div class='alert alert-danger'>Your Account is Inactive! </div>";
                        btnDeposit.Enabled = false;
                        btnWithdraw.Enabled = false;
                    }
                }
            }
        }
        catch (MySqlException ex)
        {
           
            trx_feedback.InnerHtml = "<div class='alert alert-danger'>No record was inserted due to this error: " + ex.Message + "</div>";
        }
        finally
        {
            con.Close();
        }

    }

    protected void btnDeposit_OnClick(object sender, EventArgs e)
    {
        AddDeposit();
    }

    protected void AddDeposit()
    {
        MySqlTransaction mysqlTrx = (dynamic)null;
        MySqlConnection con = (dynamic)null;
        var accountNo = Request.QueryString["accNo"];
       
        try
        {
            //now add supplied data to dB

            if (string.IsNullOrWhiteSpace(txtAmountDeposited.Text.ToString(CultureInfo.InvariantCulture)))
            {
                trx_feedback.InnerHtml = "<div class='alert alert-danger'>Input amount to Deposit!</div>";
                return;
            }

            using (con = new MySqlConnection(ConString))
            {

                string checkifAccountExists = "SELECT * FROM user_bank_details WHERE account_number = @accountnumber LIMIT 1";
                //check if user exists first
                con.Open();
                MySqlCommand commandXt = new MySqlCommand(checkifAccountExists, con);

                //use parametarized queries to prevent sql injection
                commandXt.Parameters.AddWithValue("@accountnumber", accountNo);

                int accountIsThere = (int)commandXt.ExecuteScalar();

                if (accountIsThere == 1)
                {
                    //get account_id, user_id
                    var accountid = (dynamic)null;
                    var userid = (dynamic)null;

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(commandXt);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        accountid = dr["id"];
                        userid = dr["user_id"];
                    }

                    //account exist, now update it with deposit
                    string updateQry =
                        "UPDATE user_bank_details SET account_balance = account_balance+@amounttodeposit WHERE account_number=@account_number";

                    MySqlCommand commandUpdt = new MySqlCommand(updateQry, con);
                    // Start a local transaction
                    mysqlTrx = con.BeginTransaction();
                    // assign both transaction object and connection to Command object for a pending local transaction
                    commandUpdt.Connection = con;
                    commandUpdt.Transaction = mysqlTrx;

                    //use parametarized queries to prevent sql injection
                    commandUpdt.Parameters.AddWithValue("@amounttodeposit", Convert.ToDecimal(txtAmountDeposited.Text));
                    commandUpdt.Parameters.AddWithValue("@account_number", accountNo);

                    if (commandUpdt.ExecuteNonQuery() == 1)
                    {
                        //update is success, now update transactions table


                        string insertQrytoTrxs =
                            "INSERT INTO general_transactions(user_id, account_id, transaction_description, source_account, account_number_paid_to, transaction_amount, transaction_type, transaction_date) " +
                            "VALUES(@userid, @accountid, @trxdescription, @sourceaccount, @accountnumberpaidto, @trxamount, @trxtype, @trxdate)";

                        MySqlCommand commandP = new MySqlCommand(insertQrytoTrxs, con);
                        //use parametarized queries to prevent sql injection
                        commandP.Parameters.AddWithValue("@userid", userid);
                        commandP.Parameters.AddWithValue("@accountid", accountid);
                        commandP.Parameters.AddWithValue("@trxdescription", "Deposit to Account");
                        commandP.Parameters.AddWithValue("@sourceaccount", accountNo);
                        commandP.Parameters.AddWithValue("@accountnumberpaidto", accountNo);
                        commandP.Parameters.AddWithValue("@trxamount", Convert.ToDecimal(txtAmountDeposited.Text));
                        commandP.Parameters.AddWithValue("@trxtype", "Deposit");
                        commandP.Parameters.AddWithValue("@trxdate", DateTime.Now);

                        if (commandP.ExecuteNonQuery() == 1)
                        {
                            //now commit
                            mysqlTrx.Commit();
                            Response.Redirect("AccountTransactions.aspx?accNo=" + accountNo);
                            trx_feedback.InnerHtml = "<div class='alert alert-success' style='display:block'>Amount Deposit success</div>";

                        }
                      
                    }
                }
                con.Close();
                trx_feedback.InnerHtml = "<div class='alert alert-danger'>Account not found!</div>";
            }
        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback the transaction if any error occurs during the process of inserting
                con.Open();
                mysqlTrx.Rollback();
                con.Close();
            }
            catch (MySqlException ex2)
            {
                if (mysqlTrx.Connection != null)
                {
                    trx_feedback.InnerHtml = "<div class='alert alert-danger'>An exception of type " + ex2.GetType() + " was encountered while attempting to roll back the transaction!</div>";
                }
            }
            trx_feedback.InnerHtml = "<div class='alert alert-danger'>deposit not success due to this error: " + ex.Message +"</div>";
        }
        finally
        {
            con.Close();
        }
    }

    protected void btnWithdraw_OnClick(object sender, EventArgs e)
    {
        DoAWithdraw();
    }

    protected void DoAWithdraw()
    {
        MySqlTransaction mysqlTrx = (dynamic)null;
        MySqlConnection con = (dynamic)null;
        var accountNo = Request.QueryString["accNo"];

        try
        {
            //now add supplied data to dB

            if (string.IsNullOrWhiteSpace(txtAmounttoWithdraw.Text.ToString(CultureInfo.InvariantCulture)))
            {
                trx_feedback.InnerHtml = "<div class='alert alert-danger'>Input amount to withdraw!</div>";
                return;
            }

            using (con = new MySqlConnection(ConString))
            {

                string checkifAccountExists = "SELECT * FROM user_bank_details WHERE account_number = @accountnumber LIMIT 1";
                //check if user exists first
                con.Open();
                MySqlCommand commandXt = new MySqlCommand(checkifAccountExists, con);

                //use parametarized queries to prevent sql injection
                commandXt.Parameters.AddWithValue("@accountnumber", accountNo);

                int accountIsThere = (int)commandXt.ExecuteScalar();

                if (accountIsThere == 1)
                {
                    //get account_id, user_id
                    var accountid = (dynamic)null;
                    var userid = (dynamic)null;

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(commandXt);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        accountid = dr["id"];
                        userid = dr["user_id"];
                    }

                    //account exist, now update it with deposit
                    string updateQry =
                        "UPDATE user_bank_details SET account_balance = account_balance-@amounttowithdraw WHERE account_number=@account_number";

                    MySqlCommand commandUpdt = new MySqlCommand(updateQry, con);
                    // Start a local transaction
                    mysqlTrx = con.BeginTransaction();
                    // assign both transaction object and connection to Command object for a pending local transaction
                    commandUpdt.Connection = con;
                    commandUpdt.Transaction = mysqlTrx;

                    //use parametarized queries to prevent sql injection
                    commandUpdt.Parameters.AddWithValue("@amounttowithdraw", Convert.ToDecimal(txtAmounttoWithdraw.Text));
                    commandUpdt.Parameters.AddWithValue("@account_number", accountNo);

                    if (commandUpdt.ExecuteNonQuery() == 1)
                    {
                        //update is success, now update transactions table


                        string insertQrytoTrxs =
                            "INSERT INTO general_transactions(user_id, account_id, transaction_description, source_account, account_number_paid_to, transaction_amount, transaction_type, transaction_date) " +
                            "VALUES(@userid, @accountid, @trxdescription, @sourceaccount, @accountnumberpaidto, @trxamount, @trxtype, @trxdate)";

                        MySqlCommand commandP = new MySqlCommand(insertQrytoTrxs, con);
                        //use parametarized queries to prevent sql injection
                        commandP.Parameters.AddWithValue("@userid", userid);
                        commandP.Parameters.AddWithValue("@accountid", accountid);
                        commandP.Parameters.AddWithValue("@trxdescription", "Withdraw from Account");
                        commandP.Parameters.AddWithValue("@sourceaccount", accountNo);
                        commandP.Parameters.AddWithValue("@accountnumberpaidto", accountNo);
                        commandP.Parameters.AddWithValue("@trxamount", Convert.ToDecimal(txtAmounttoWithdraw.Text));
                        commandP.Parameters.AddWithValue("@trxtype", "Withdraw");
                        commandP.Parameters.AddWithValue("@trxdate", DateTime.Now);

                        if (commandP.ExecuteNonQuery() == 1)
                        {
                            //now commit
                            mysqlTrx.Commit();
                            Response.Redirect("AccountTransactions.aspx?accNo=" + accountNo);
                            trx_feedback.InnerHtml = "<div class='alert alert-success' style='display:block'>Amount withdraw success</div>";

                        }

                    }
                }
                con.Close();
                trx_feedback.InnerHtml = "<div class='alert alert-danger'>Account not found!</div>";
            }
        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback the transaction if any error occurs during the process of inserting
                con.Open();
                mysqlTrx.Rollback();
                con.Close();
            }
            catch (MySqlException ex2)
            {
                if (mysqlTrx.Connection != null)
                {
                    trx_feedback.InnerHtml = "<div class='alert alert-danger'>An exception of type " + ex2.GetType() + " was encountered while attempting to roll back the transaction!</div>";
                }
            }
            trx_feedback.InnerHtml = "<div class='alert alert-danger'>withdraw not success due to this error: " + ex.Message + "</div>";
        }
        finally
        {
            con.Close();
        }
    }

    protected void PopulateBillsDdl()
    {
        MySqlConnection con = (dynamic)null;
        try
        {
            using (con = new MySqlConnection(ConString))
            {
                string selectQuery =
                    "SELECT description, bill_account_no FROM bills";
               
                MySqlCommand command0 = new MySqlCommand(selectQuery, con);

                command0.CommandType = CommandType.Text;
                command0.Connection = con;
                con.Open();
                ddlBills.DataSource = command0.ExecuteReader();
                ddlBills.DataTextField = "description";
                ddlBills.DataValueField = "bill_account_no";
                ddlBills.DataBind();
                con.Close();
            }
            ddlBills.Items.Insert(0, new ListItem("--Select Bill--", "0"));
        }
        catch (MySqlException ex)
        {

        }
        finally
        {
            con.Close();
        }
    }

    protected void btnPayBill_OnClick(object sender, EventArgs e)
    {

        PayABill();
    }

    protected void PayABill()
    {
        MySqlTransaction mysqlTrx = (dynamic)null;
        MySqlConnection con = (dynamic)null;
        var accountNo = Request.QueryString["accNo"];

        try
        {
            //now add supplied data to dB

            if (ddlBills.SelectedIndex == 0)
            {
                trx_feedback.InnerHtml = "<div class='alert alert-danger'>Kindly select the Bill to pay!</div>";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBillCost.Text.ToString(CultureInfo.InvariantCulture)))
            {
                trx_feedback.InnerHtml = "<div class='alert alert-danger'>Input amount to pay!</div>";
                return;
            }

            using (con = new MySqlConnection(ConString))
            {

                string checkifAccountExists = "SELECT * FROM user_bank_details WHERE account_number = @accountnumber LIMIT 1";
                //check if user exists first
                con.Open();
                MySqlCommand commandXt = new MySqlCommand(checkifAccountExists, con);

                //use parametarized queries to prevent sql injection
                commandXt.Parameters.AddWithValue("@accountnumber", accountNo);

                int accountIsThere = (int)commandXt.ExecuteScalar();

                if (accountIsThere == 1)
                {
                    //get account_id, user_id
                    var accountid = (dynamic)null;
                    var userid = (dynamic)null;

                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(commandXt);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        accountid = dr["id"];
                        userid = dr["user_id"];
                    }

                    //account exist, now update it with deposit
                    string updateQry =
                        "UPDATE user_bank_details SET account_balance = account_balance-@amounttopay WHERE account_number=@account_number";

                    MySqlCommand commandUpdt = new MySqlCommand(updateQry, con);
                    // Start a local transaction
                    mysqlTrx = con.BeginTransaction();
                    // assign both transaction object and connection to Command object for a pending local transaction
                    commandUpdt.Connection = con;
                    commandUpdt.Transaction = mysqlTrx;

                    //use parametarized queries to prevent sql injection
                    commandUpdt.Parameters.AddWithValue("@amounttopay", Convert.ToDecimal(txtBillCost.Text));
                    commandUpdt.Parameters.AddWithValue("@account_number", accountNo);

                    if (commandUpdt.ExecuteNonQuery() == 1)
                    {
                        //update is success, now update transactions table


                        string insertQrytoTrxs =
                            "INSERT INTO general_transactions(user_id, account_id, transaction_description, source_account, account_number_paid_to, transaction_amount, transaction_type, transaction_date) " +
                            "VALUES(@userid, @accountid, @trxdescription, @sourceaccount, @accountnumberpaidto, @trxamount, @trxtype, @trxdate)";

                        MySqlCommand commandP = new MySqlCommand(insertQrytoTrxs, con);
                        //use parametarized queries to prevent sql injection
                        commandP.Parameters.AddWithValue("@userid", userid);
                        commandP.Parameters.AddWithValue("@accountid", accountid);
                        commandP.Parameters.AddWithValue("@trxdescription", "Pay Bill from Account to: "+ ddlBills.SelectedItem.Text);
                        commandP.Parameters.AddWithValue("@sourceaccount", accountNo);
                        commandP.Parameters.AddWithValue("@accountnumberpaidto", ddlBills.SelectedValue);
                        commandP.Parameters.AddWithValue("@trxamount", Convert.ToDecimal(txtBillCost.Text));
                        commandP.Parameters.AddWithValue("@trxtype", "Bill Payment");
                        commandP.Parameters.AddWithValue("@trxdate", DateTime.Now);

                        if (commandP.ExecuteNonQuery() == 1)
                        {
                            //now commit
                            mysqlTrx.Commit();
                            Response.Redirect("AccountTransactions.aspx?accNo=" + accountNo);
                            trx_feedback.InnerHtml = "<div class='alert alert-success' style='display:block'>Amount withdraw success</div>";

                        }

                    }
                }
                con.Close();
                trx_feedback.InnerHtml = "<div class='alert alert-danger'>Account not found!</div>";
            }
        }
        catch (MySqlException ex)
        {
            try
            {
                //rollback the transaction if any error occurs during the process of inserting
                con.Open();
                mysqlTrx.Rollback();
                con.Close();
            }
            catch (MySqlException ex2)
            {
                if (mysqlTrx.Connection != null)
                {
                    trx_feedback.InnerHtml = "<div class='alert alert-danger'>An exception of type " + ex2.GetType() + " was encountered while attempting to roll back the transaction!</div>";
                }
            }
            trx_feedback.InnerHtml = "<div class='alert alert-danger'>withdraw not success due to this error: " + ex.Message + "</div>";
        }
        finally
        {
            con.Close();
        }
    }

    protected void PopulateInvestmentsDdl()
    {
        MySqlConnection con = (dynamic)null;
        try
        {
            using (con = new MySqlConnection(ConString))
            {
                string selectQuery =
                    "SELECT investment_code, investment_description FROM investments";

                MySqlCommand command0 = new MySqlCommand(selectQuery, con);

                command0.CommandType = CommandType.Text;
                command0.Connection = con;
                con.Open();
                ddlInvestment.DataSource = command0.ExecuteReader();
                ddlInvestment.DataTextField = "investment_description";
                ddlInvestment.DataValueField = "investment_code";
                ddlInvestment.DataBind();
                con.Close();
            }
            ddlInvestment.Items.Insert(0, new ListItem("--Choose Investment--", "0"));
        }
        catch (MySqlException ex)
        {

        }
        finally
        {
            con.Close();
        }
    }

    protected void btnPayInvestment_OnClick(object sender, EventArgs e)
    {
        
    }
}