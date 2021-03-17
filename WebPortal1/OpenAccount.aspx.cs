using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

public partial class OpenAccount : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString = new DBConfig().MysqLConnector();


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_OnClick(object sender, EventArgs e)
    {

        AddNewUser();
    }

    protected void AddNewUser()
    {
        MySqlTransaction mysqlTrx = (dynamic)null;
        MySqlConnection con = (dynamic)null;

        try
        {

            //now add supplied data to dB

            if (string.IsNullOrWhiteSpace(txtFname.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Firstname field Empty!</div>";
                return;
            }


            if (string.IsNullOrWhiteSpace(txtLname.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Lastname field Empty!</div>";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtIDNumber.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Id Number field Empty!</div>";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Phonenumber field Empty!</div>";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Account Number field Empty!</div>";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Email field Empty!</div>";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword1.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>password field Empty!</div>";
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword2.Text))
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>password confirm field Empty!</div>";
                return;
            }

            if (txtPassword1.Text!=txtPassword2.Text)
            {
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Password mismatch!</div>";
                return;
            }
            

            using (con = new MySqlConnection(ConString))
            {
                string insertQry =
                    "INSERT INTO users(fname, mname, lname, email, password) VALUES(@fname, @mname, @lname, @email, @password)";

                con.Open();
                MySqlCommand command = new MySqlCommand(insertQry, con);
                // Start a local transaction
                mysqlTrx = con.BeginTransaction();
                // assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = con;
                command.Transaction = mysqlTrx;

                //use parametarized queries to prevent sql injection
                command.Parameters.AddWithValue("@fname", txtFname.Text);
                command.Parameters.AddWithValue("@mname", txtMname.Text);
                command.Parameters.AddWithValue("@lname", txtLname.Text);
                command.Parameters.AddWithValue("@email", txtEmail.Text);
                command.Parameters.AddWithValue("@password", EncryptP(txtPassword2.Text));

                if (command.ExecuteNonQuery() == 1)
                {
                    var userid = (dynamic)null;

                    //insert to bank details table
                    //get userid first
                    string checkifUserExists = "SELECT * FROM users WHERE email = @email LIMIT 1";
                    //check if agent exists first
                    MySqlCommand commandX = new MySqlCommand(checkifUserExists, con);
                    //use parametarized queries to prevent sql injection
                    commandX.Parameters.AddWithValue("@email", txtEmail.Text);
                    commandX.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(commandX);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    { userid = dr["user_id"]; }

                    string insertQrytoBankdetails =
                        "INSERT INTO user_bank_details(user_id, account_number, account_name, account_balance, account_status) " +
                        "VALUES(@userid, @accountnumber, @accountname, @accountbalance, @accountstatus)";

                    MySqlCommand commandP = new MySqlCommand(insertQrytoBankdetails, con);
                    //use parametarized queries to prevent sql injection
                    commandP.Parameters.AddWithValue("@userid", userid);
                    commandP.Parameters.AddWithValue("@accountnumber", txtAccountNumber.Text);
                    commandP.Parameters.AddWithValue("@accountname", txtFname.Text + " " + txtLname.Text);
                    commandP.Parameters.AddWithValue("@accountbalance", Convert.ToDecimal(0));
                    commandP.Parameters.AddWithValue("@accountstatus", "inactive");

                    if (commandP.ExecuteNonQuery() == 1)
                    {
                        //commit the insert transaction to dB  after inserting to 2nd table
                        mysqlTrx.Commit();
                        register_feedback.InnerHtml = "<div class='alert alert-success'>User account successfully created!</div>";
                        return;
                    }

                }
                con.Close();
                register_feedback.InnerHtml = "<div class='alert alert-danger'>Error occured while creating account!</div>";
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
                    //return Ok("An exception of type " + ex2.GetType() + " was encountered while attempting to roll back the transaction!");
                    register_feedback.InnerHtml = "<div class='alert alert-danger'>An exception of type " + ex2.GetType() + " was encountered while attempting to roll back the transaction!</div>";
                }
            }
            //return Ok("No record was inserted due to this error: " + ex.Message);
            register_feedback.InnerHtml = "<div class='alert alert-danger'>No record was inserted due to this error: "+ ex.Message + "</div>";
        }
        finally
        {
            con.Close();
        }
    }


    static string EncryptP(string mypass)
    {
        //encryptpassword:
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] data = md5.ComputeHash(utf8.GetBytes(mypass));
            return Convert.ToBase64String(data);
        }
    }
}