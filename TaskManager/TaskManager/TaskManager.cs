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
                for (int i = 0; i < lines.Length; i = i + 3)
                {
                    Task task = new Task(lines[i], lines[i + 1], lines[i+2]);
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
        public static bool AddResponsible(Task task, User user)
        {
            return true;
        }
        public static bool SendMessage()
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
            foreach (var item in data)
            {
                sw.WriteLine($"Id: {item.Id}");
                sw.WriteLine($"Name: {item.Name}");
                sw.WriteLine(item.Phone);
                sw.WriteLine(item.Description);
            }
            return true;
        }
    }
}
