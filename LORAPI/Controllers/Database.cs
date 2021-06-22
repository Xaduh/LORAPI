using LORAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LORAPI.Controllers
{
    public class Database
    {
        public int UserLogin(string username, string password)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(@"Data Source=192.168.4.110\LOR\MSSQL,1433;Initial Catalog=LORDB;MultipleActiveResultSets=true;User ID=LORUser;Password=Passw0rd")) 
            {
                //con.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from [User] where Username = @username and [Password] = @password", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    try
                    {
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            id = Convert.ToInt32(sdr[0]);
                        }
                    }
                    catch (SqlException ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            return id;
        }
    }
}
