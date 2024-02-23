namespace RaidFinder.Models
{
    public class UserDB
    {
        private static List<User> _Users = new List<User>()
        {
            new User {UserId = 1, Name = "Test", OwnedPostId = [], Stat = new Stat {PowerLevel = 14000, Class = "Monk", Level = 80 }},
            new User {UserId = 2, Name = "Test2", OwnedPostId = [], Stat = new Stat {PowerLevel = 15000, Class = "Prist", Level = 85 }},
            new User {UserId = 3, Name = "Test3", OwnedPostId = [], Stat = new Stat {PowerLevel = 27000, Class = "Mage", Level = 87 }},
        };

        public static void AddUser(User user)
        {
            if (_Users.Count() == 0) 
            { 
                user.UserId = 1;
                _Users.Add(user);
                return ;
            }
            user.UserId = _Users.Max(x => x.UserId) + 1;
            _Users.Add(user);
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
                tmp.Stat.Class = user.Stat.Class;
                tmp.Stat.PowerLevel = user.Stat.PowerLevel;
                tmp.Stat.Level = user.Stat.Level;
                tmp.Name = user.Name;
                return 200;
            }
        }

        public static int DeleteUser(int id)
        {
            var tmp = _Users.FirstOrDefault(x => x.UserId == id);
            if(tmp == null) { return 404; }
            else
            {
                _Users.Remove(tmp);
                return 200;
            }
        }
    }
}
