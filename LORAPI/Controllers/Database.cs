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
            using (SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=LORDB;MultipleActiveResultSets=true;User ID=LORUser;Password=Passw0rd")) 
            {
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

        public int CheckUsername(string username)
        {
            int id = 0;
            using (SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=LORDB;MultipleActiveResultSets=true;User ID=LORUser;Password=Passw0rd"))
            {
                using (SqlCommand cmd = new SqlCommand($"select * from [User] where Username = @username", con))
                {
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@username", username);
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

        public async Task<List<User>> UsersFriendsList(int id)
        {
            List<User> friendsList = new List<User>();
            await using (SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=LORDB;MultipleActiveResultSets=true;User ID=LORUser;Password=Passw0rd"))
            {
                using (SqlCommand cmd = new SqlCommand($"select * from [User] inner join Friendslist on [User].UserID = Friendslist.UserID where Friendslist.FriendID = {id} and StatusID = 1;", con))
                {
                    con.Open();
                    try
                    {
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            User newUser = new User();
                            newUser.UserID = Convert.ToInt32(sdr[0]);
                            newUser.Username = sdr.GetString(1);
                            newUser.Password = sdr.GetString(2);
                            newUser.Email = sdr.GetString(3);
                            newUser.Role = sdr.GetString(4);
                            friendsList.Add(newUser);
                        }
                    }
                    catch (SqlException ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                using (SqlCommand cmd = new SqlCommand($"select * from [User] inner join Friendslist on [User].UserID = Friendslist.FriendID where Friendslist.UserID = {id} and StatusID = 1;", con))
                {
                    con.Open();
                    try
                    {
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            User newUser = new User();
                            newUser.UserID = Convert.ToInt32(sdr[0]);
                            newUser.Username = sdr.GetString(1);
                            newUser.Password = sdr.GetString(2);
                            newUser.Email = sdr.GetString(3);
                            newUser.Role = sdr.GetString(4);
                            friendsList.Add(newUser);
                        }
                    }
                    catch (SqlException ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            return friendsList;
        }

        public async Task<List<User>> UsersPendingList(int id)
        {
            List<User> friendsList = new List<User>();
            await using (SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=LORDB;MultipleActiveResultSets=true;User ID=LORUser;Password=Passw0rd"))
            {
                using (SqlCommand cmd = new SqlCommand($"select * from [User] inner join Friendslist on [User].UserID = Friendslist.UserID where Friendslist.FriendID = {id} and StatusID = 2;", con))
                {
                    con.Open();
                    try
                    {
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            User newUser = new User();
                            newUser.UserID = Convert.ToInt32(sdr[0]);
                            newUser.Username = sdr.GetString(1);
                            newUser.Password = sdr.GetString(2);
                            newUser.Email = sdr.GetString(3);
                            newUser.Role = sdr.GetString(4);
                            friendsList.Add(newUser);
                        }
                    }
                    catch (SqlException ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }                
            }
            return friendsList;
        }
    }
}
