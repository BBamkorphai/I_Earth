using System;
using Microsoft;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace RaidFinder.Models

{
    public class IndexModels
    {
        private static List<RaidingPostModels> _Posts = new List<RaidingPostModels>();

        public static void UpdatePostDB()
        {
            _Posts.Clear();
            using (SqlConnection con = new SqlConnection("data source=LAPTOP-ISNE8U4H;initial catalog=UserDB;trusted_connection=true"))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Post", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var post = new RaidingPostModels();
                    post.Name = reader["Name"].ToString();
                    post.PowerLevel = Convert.ToInt32(reader["PowerLevel"]);
                    post.MaxSize = Convert.ToInt32(reader["MaxSize"]);
                    post.Description = reader["Description"].ToString();
                    post.OwnerId = Convert.ToInt32(reader["OwnerId"]);
                    var tmp = reader["PartyList"].ToString().Split('s').Where(x => int.TryParse(x, out _)).Select(int.Parse).ToList();
                    foreach (var item in tmp)
                    {
                        post.PartyList.Add(UserDB.GetUserCopyById(item));
                    }
                    post.TimeOut = reader["TimeOut"].ToString();
                    post.PostId = Convert.ToInt32(reader["PostId"]);

                    _Posts.Add(post);
                }
            }
        }

        public static void AddPost(RaidingPostModels post)
        {
            if (_Posts.Count == 0) { post.PostId = 1; }
            else if (post.PostId == 0) { post.PostId = _Posts.Max(x => x.PostId) + 1; }
            string sqlcmd = null;
            sqlcmd = "INSERT INTO Post ([Name], [PowerLevel], [MaxSize], [Description], [OwnerId], [PartyList], [TimeOut], [PostId]) VALUES (@Name, @PowerLevel, @MaxSize, @Description, @OwnerId, @PartyList, @TimeOut , @PostId);";
            using (SqlConnection con = new SqlConnection("data source=LAPTOP-ISNE8U4H;initial catalog=UserDB;trusted_connection=true"))
            {
                con.Open();
                //SqlCommand iuon = new SqlCommand("SET IDENTITY_INSERT Users ON", con);
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    cmd.Parameters.Add("@PostId", SqlDbType.Int).Value = post.PostId;
                    cmd.Parameters.Add("@Name", SqlDbType.Char).Value = post.Name;
                    cmd.Parameters.Add("@PowerLevel", SqlDbType.Int).Value = post.PowerLevel;
                    cmd.Parameters.Add("@MaxSize", SqlDbType.Int).Value = post.MaxSize;
                    cmd.Parameters.Add("@Description", SqlDbType.Char).Value = post.Description;
                    cmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = post.OwnerId;
                    cmd.Parameters.Add("@TimeOut", SqlDbType.DateTime).Value = DateTime.Now;
                    //var tmp = new List<String>();
                    //foreach (var user in post.PartyList)
                    //{
                    //    tmp.Add(user.UserId.ToString());
                    //}
                    cmd.Parameters.Add("@PartyList", SqlDbType.Char).Value = post.OwnerId.ToString();
                    int Out = cmd.ExecuteNonQuery();
                }
                //SqlCommand iuoff = new SqlCommand("SET IDENTITY_INSERT Users OFF", con);
            }
            UpdatePostDB();
        }

        public static List<RaidingPostModels> GetPosts() => _Posts;

        public static RaidingPostModels? GetPostCopyById(int id)
        {
            var tmp = _Posts.FirstOrDefault(x => x.PostId == id);
            if (tmp == null)
            {
                return null;
            }
            return new RaidingPostModels
            {
                PostId = tmp.PostId,
                PowerLevel = tmp.PowerLevel,
                Name = tmp.Name,
                MaxSize = tmp.MaxSize,
                Description = tmp.Description,
                OwnerId = tmp.OwnerId,
                PartyList = tmp.PartyList,
                TimeOut = tmp.TimeOut
            };
        }
        public static int UpdatePost(int id, RaidingPostModels post)
        {
            var tmp = _Posts.FirstOrDefault(x => x.PostId == id);
            if (tmp == null)
            {
                return 404;
            }
            else
            {
                post.PartyList = tmp.PartyList;
                DeletePost(id);
                AddPost(post);
                return 200;
            }
        }

        public static int DeletePost(int id)
        {
            var tmp = _Posts.FirstOrDefault(x => x.PostId == id);
            if (tmp == null) { return 404; }
            else
            {
                string sqlcmd = null;
                sqlcmd = "DELETE FROM Post WHERE PostId=@PostId;";
                using (SqlConnection con = new SqlConnection("data source=LAPTOP-ISNE8U4H;initial catalog=UserDB;trusted_connection=true"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                    {
                        cmd.Parameters.Add("@PostId", SqlDbType.Int).Value = tmp.PostId;
                        int Out = cmd.ExecuteNonQuery();
                    }
                }
                UpdatePostDB();
                return 200;
            }
        }
    }
}