using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class AddNewAccount : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString = @"datasource=localhost;port=3306;username=root;password=root;database=smartapp_db";


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAddNewAcc_OnClick(object sender, EventArgs e)
    {
        AddAcccount();
    }

    protected void AddAcccount()
    {
        MySqlConnection con = (dynamic)null;

        var userId = Convert.ToInt32(Session["userid"]) ;
        var accountName = Session["fname"] +" "+Session["lname"];

        try
        {

            //now add supplied data to dB

            if (string.IsNullOrWhiteSpace(txtaccountNumber.Text))
            {
                add_acc_feedback.InnerHtml = "<div class='alert alert-danger'>Account number field Empty!</div>";
                return;
            }


            if (string.IsNullOrWhiteSpace(txtAccountBalance.Text))
            {
                add_acc_feedback.InnerHtml = "<div class='alert alert-danger'>Account balance field Empty!</div>";
                return;
            }
            

            using (con = new MySqlConnection(ConString))
            {
                string insertQry =
                    "INSERT INTO user_bank_details(user_id, account_number, account_name, account_balance, account_status) VALUES(@userid, @accountnumber, @accountname, @accountbalance, @accountstat)";

                con.Open();
                MySqlCommand command = new MySqlCommand(insertQry, con);
               
                // assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = con;

                //use parametarized queries to prevent sql injection
                command.Parameters.AddWithValue("@userid", userId);
                command.Parameters.AddWithValue("@accountnumber", txtaccountNumber.Text);
                command.Parameters.AddWithValue("@accountname", accountName);
                command.Parameters.AddWithValue("@accountbalance", Convert.ToDecimal(txtAccountBalance.Text));
                command.Parameters.AddWithValue("@accountstat", "inactive");

                if (command.ExecuteNonQuery() == 1)
                {
                    add_acc_feedback.InnerHtml = "<div class='alert alert-success'> Account successfully created!</div>";
                    return;
                }
                con.Close();
                add_acc_feedback.InnerHtml = "<div class='alert alert-danger'>Error occured while creating account!</div>";
            }
        }
        catch (MySqlException ex)
        {
            //return Ok("No record was inserted due to this error: " + ex.Message);
            add_acc_feedback.InnerHtml = "<div class='alert alert-danger'>No record was inserted due to this error: " + ex.Message + "</div>";
        }
        finally
        {
            con.Close();
        }
    }

}