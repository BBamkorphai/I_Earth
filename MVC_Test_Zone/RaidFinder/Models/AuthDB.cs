using System.Data.SqlClient;
using System.Data;
namespace RaidFinder.Models
{
    public class AuthDB
    {
        private static List<Auth> _Auths = new List<Auth>();

        public static void UpdateDB()
        {
            _Auths.Clear();
            using (SqlConnection con = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Auth", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var auth = new Auth();
                    auth.Username = reader["Username"].ToString().Trim(' ');
                    auth.UserId = Convert.ToInt32(reader["UserId"]);
                    auth.Password = reader["Pass"].ToString().Trim(' ');
                    if (_Auths.FirstOrDefault(x => x.UserId == auth.UserId) == null)
                    {
                        _Auths.Add(auth);
                    }
                }
            }
        }

        public static void AddUser(Auth auth)
        {
            UpdateDB();
            if (_Auths.Count() == 0) { auth.UserId = 1; }
            else if (auth.UserId == 0) { auth.UserId = _Auths.Max(x => x.UserId) + 1; }
            string sqlcmd = null;
            sqlcmd = "INSERT INTO Auth ([Username], [Pass], [UserId]) VALUES (@Username, @Pass, @userId)";
            using (SqlConnection con = new SqlConnection("Server=localhost;Database=UserDB;Trusted_Connection=True;"))
            {
                con.Open();
                //SqlCommand iuon = new SqlCommand("SET IDENTITY_INSERT Users ON", con);
                using (SqlCommand cmd = new SqlCommand(sqlcmd, con))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.Int).Value = auth.UserId;
                    cmd.Parameters.Add("@Pass", SqlDbType.Char).Value = auth.Username;
                    cmd.Parameters.Add("@UserId", SqlDbType.Char).Value = auth.UserId;
                    int Out = cmd.ExecuteNonQuery();
                }
                //SqlCommand iuoff = new SqlCommand("SET IDENTITY_INSERT Users OFF", con);
            }
            UpdateDB();
        }

        public static List<Auth> GetUsers() => _Auths;

        public static Auth? GetUserCopyById(int id)
        {
            var tmp = _Auths.FirstOrDefault(x => x.UserId == id);
            if (tmp == null)
            {
                return null;
            }
            return new Auth
            {
                UserId = tmp.UserId,
                Username = tmp.Username,
                Password = tmp.Password,
            };
        }
        public static int UpdateUser(int id, Auth auth)
        {
            var tmp = _Auths.FirstOrDefault(x => x.UserId == id);
            if (tmp == null)
            {
                return 404;
            }
            else
            {
                DeleteUser(tmp.UserId);
                AddUser(auth);
                return 200;
            }
        }

        public static int DeleteUser(int id)
        {
            var tmp = _Auths.FirstOrDefault(x => x.UserId == id);
            if (tmp == null) { return 404; }
            else
            {
                string sqlcmd = null;
                sqlcmd = "DELETE FROM Auth WHERE UserId=@UserId;";
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
        public static int Authentication(Auth auth)
        {
            foreach (var user in _Auths){
                if (user.Username == auth.Username && user.Password == auth.Password) {
                    return user.UserId;
                }
            }
            return 0;
        }
    }
}
