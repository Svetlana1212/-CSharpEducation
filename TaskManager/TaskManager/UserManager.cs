using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTaskManager
{
    public class UserManager: DataСollectionInterface
    {
        public static string path = "user.txt";
        public static List<User> users = new List<User>();
        public static void List()
        {

        }
        public static bool Add()
        {
            return true;
        }
        public static User Search(string name,string email)
        {
            return users[0];
        }
        public static bool Update()
        {
            return true;
        }
        public static bool Delete()
        {
            return true;
        }
    }
}
