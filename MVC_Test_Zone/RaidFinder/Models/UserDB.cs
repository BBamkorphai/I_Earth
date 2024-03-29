﻿using System;
using Microsoft;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace RaidFinder.Models

{
    public class UserDB
    {
        private static List<User> _Users = new List<User>();

        public static void UpdateDB()
        {
            _Users.Clear();
            using (SqlConnection con = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var user = new User();
                    user.Name = reader["Name"].ToString().Trim(' ');
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.Stat.PowerLevel = Convert.ToInt32(reader["PowerLevel"]);
                    user.Stat.Level = Convert.ToInt32(reader["Lv"]);
                    user.Stat.Class = reader["Class"].ToString().Trim(' ');
                    if (_Users.FirstOrDefault(x => x.UserId == user.UserId) == null)
                    {
                        _Users.Add(user);
                    }
                }
            }
        }

        public static int AddUser(User user)
        {
            if (_Users.Count() == 0) { user.UserId = 1; }
            else if (user.UserId == 0) { user.UserId = _Users.Max(x => x.UserId) + 1; }
            string sqlcmd = null;
            sqlcmd = "INSERT INTO Users ([UserId], [Name], [Class], [PowerLevel], [Lv]) VALUES (@UserId, @Name, @Class, @PowerLevel, @Level)";
            using (SqlConnection con = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
            {
                con.Open();
                //SqlCommand iuon = new SqlCommand("SET IDENTITY_INSERT Users ON", con);
                using(SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = user.UserId;
                    cmd.Parameters.Add("@Name", SqlDbType.Char).Value = user.Name;
                    cmd.Parameters.Add("@PowerLevel", SqlDbType.Int).Value = user.Stat.PowerLevel;
                    cmd.Parameters.Add("@Class", SqlDbType.Char).Value = user.Stat.Class;
                    cmd.Parameters.Add("@Level", SqlDbType.Int).Value = user.Stat.Level;
                    int Out = cmd.ExecuteNonQuery();
                }
                //SqlCommand iuoff = new SqlCommand("SET IDENTITY_INSERT Users OFF", con);
            }
            UpdateDB();
            return user.UserId;
        }

        public static List<User> GetUsers() => _Users;

        public static User? GetUserCopyById(int id)
        {
            var tmp = _Users.FirstOrDefault(x => x.UserId == id);
            if (tmp == null)
            {
                return null;
            }
            return new User { UserId = tmp.UserId, 
                Stat = new Stat { Level = tmp.Stat.Level, Class = tmp.Stat.Class, PowerLevel = tmp.Stat.PowerLevel },
                Name =  tmp.Name, 
                OwnedPostId = tmp.OwnedPostId};
        }
        public static int UpdateUser(int id, User user)
        {
            var tmp = _Users.FirstOrDefault(x => x.UserId == id);
            if (tmp == null)
            {
                return 404;
            }
            else
            {
                DeleteUser(tmp.UserId);
                AddUser(user);
                return 200;
            }
        }

        public static int DeleteUser(int id)
        {
            var tmp = _Users.FirstOrDefault(x => x.UserId == id);
            if(tmp == null) { return 404; }
            else
            {
                string sqlcmd = null;
                sqlcmd = "DELETE FROM Users WHERE UserId=@UserId;";
                using (SqlConnection con = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
                {
                    con.Open();
                    //SqlCommand iuon = new SqlCommand("SET IDENTITY_INSERT Users ON", con);
                    using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = tmp.UserId;
                        int Out = cmd.ExecuteNonQuery();
                    }
                    //SqlCommand iuoff = new SqlCommand("SET IDENTITY_INSERT Users OFF", con);
                }
                UpdateDB();
                return 200;
            }
        }
    }
}
