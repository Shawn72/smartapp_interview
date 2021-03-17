using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

public partial class Login : System.Web.UI.Page
{
    /// MySQL Connection string
    public static readonly string ConString = new DBConfig().MysqLConnector();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_OnClick(object sender, EventArgs e)
    {
        LoginUser();
    }

    protected void LoginUser()
    {
        MySqlConnection con = (dynamic)null;
        
        try
        {
            //now add supplied data to dB

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                login_feedback.InnerHtml = "<div class='alert alert-danger'>Email field cannot be empty!</div>";
                return;
            }


            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                login_feedback.InnerHtml = "<div class='alert alert-danger'>Password field cannot be empty!</div>";
                return;
            }



            using (con = new MySqlConnection(ConString))
            {
                string insertQry =
                    "SELECT COUNT(*) FROM users WHERE email=@Email AND password=@Password";

                con.Open();
                MySqlCommand command = new MySqlCommand(insertQry, con);

                // assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = con;
                command.CommandType = CommandType.Text;
                //use parametarized queries to prevent sql injection
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Password", EncryptP(txtPassword.Text));

                int userIsThere = Convert.ToInt32(command.ExecuteScalar());
                if (userIsThere == 1)
                {
                  
                    string selectQuery = "SELECT user_id, fname, mname, lname, email, id_number, mobile_number FROM users WHERE email=@Useremail ";

                    MySqlCommand command0 = new MySqlCommand(selectQuery, con);

                    //use parametarized queries to prevent sql injection
                    command0.Parameters.AddWithValue("@Useremail", txtEmail.Text);
                    command0.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(command0);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        //set sessions
                        Session["userid"] = dr["user_id"];
                        Session["fname"] = dr["fname"];
                        Session["mname"] = dr["mname"];
                        Session["lname"] = dr["lname"];
                        Session["email"] = dr["email"];
                        Session["idnumber"] = dr["id_number"];
                        Session["mobileno"] = dr["mobile_number"];
                        //Session["accountno"] = dr["account_number"];
                        //Session["accounname"] = dr["account_name"];
                        //Session["accountbalance"] = dr["account_balance"];
                        //Session["accountstatus"] = dr["account_status"];

                    }
                    Session["email"] = txtEmail.Text;
                    Response.Redirect("Default.aspx");
                }
                login_feedback.InnerHtml = "<div class='alert alert-danger'>Invalid login details!</div>";

            }
        }
        catch (MySqlException ex)
        {

            login_feedback.InnerHtml = "<div class='alert alert-danger'>An error occured while logging in: " + ex.Message + "</div>";
        }
        finally
        {
            con.Close();
        }

        //str = "select count(*) from login where UserName=@UserName and Password =@Password";

        //com = new MySqlCommand(str, con);

        //com.CommandType = CommandType.Text;

        //com.Parameters.AddWithValue("@UserName", TextBox_user_name.Text);

        //com.Parameters.AddWithValue("@Password", TextBox_password.Text);

        //obj = com.ExecuteScalar();

        //if (Convert.ToInt32(obj) != 0)

        //{

        //    Response.Redirect("Welcome.aspx");

        //}

        //else

        //{

        //    lb1.Text = "invalid user name and password";

        //}




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
    
