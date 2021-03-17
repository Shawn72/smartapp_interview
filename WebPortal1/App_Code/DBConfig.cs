using System.Configuration;

public class DBConfig
{
    public string ConString = "";
    public string MysqLConnector()
    {
        return  ConString = ConfigurationManager.AppSettings["MYSQL_STRING"];
    }
}