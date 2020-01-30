using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
static class Objects // Created by: Joshua C. Magoliman
{
    public static MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
    public static MySqlCommand cmd;
    public static MySqlDataReader dr;

    public static MySqlDataAdapter da;
    public static DataTable dt;
    public static DataSet ds;

}

