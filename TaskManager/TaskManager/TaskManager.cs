using System.Reflection.Metadata;

namespace ClassTaskManager
{
    public class TaskManager: DataСollectionInterface
    {
        public static string path = "task.txt";
        public static List<Task> tasks = new List<Task>();
        
        public static void Read ()
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; i = i + 7)
                {
                    Task task = new Task(Int32.Parse(lines[i]), lines[i + 1], lines[i + 2], DateTime.Parse(lines[i+3]));
                    task.СreationDate = DateTime.Parse(lines[i+4]);
                    task.Status = lines[i + 5];
                    task.Priority = lines[i + 6];
                    string [] Respons = lines[i + 7].Split("|");
                    foreach (string item in Respons) 
                    {
                        string[] linUser=item.Split(",");
                        for (int n = 0; n < linUser.Length; n = n + 4)
                        {
                            User user = new User(linUser[n + 1], linUser[n + 2], linUser[n+3]);
                            user.Id = Int32.Parse(linUser[n]);
                            task.Responsible.Add(user);
                        }
                            
                    }                     
                    tasks.Add(task);
                }
            }
        }
        public static void List(int Id)
        {

        }
        public static bool Add(Task task)
        {
            return true;
        }
        public static bool Search(Task task) 
        {
            return true;
        }
        public static bool Sort(string sort)
        {
            return true;
        }
        public static bool Filter(string filter)
        {
            return true;
        }
        public static bool Update(Task task)
        {
            return true;
        }
        public static bool Delete(Task task) 
        {
            return true;
        }
        public static bool AddComment (Task task, string comment)
        {
            return true;
        }
        public static bool DeleteComment(Task task, string comment)
        {
            return true;
        }
        public static bool SendMessage()
        {
            return true;
        }
        public static bool AddResponsible(Task task, User user)
        {
            return true;
        }
        
        public static bool GenerateAReport()
        {
            return true;
        }
        public static bool WriteDown(List<Task> tasks)
        {
            using StreamWriter sw = File.CreateText(path);
            foreach (var item in tasks)
            {
                sw.WriteLine($"Id: {item.Id}");
                sw.WriteLine($"Name: {item.Name}");
                sw.WriteLine($"Description: {item.Description}");                
                sw.WriteLine($"Deadline: {item.Deadline}");
                sw.WriteLine($"СreationDate: {item.СreationDate}");
                sw.WriteLine($"Status: {item.Status}");
                sw.WriteLine($"Priority: {item.Priority}");
                foreach (User user in item.Responsible)
                {
                    sw.Write($"{user.Id}, {user.Name},{user.Surname},{user.Email}|");
                }
            }
            return true;
        }
    }
}
