using System.Reflection.Metadata;

namespace ClassTaskManager
{
    public class TaskManager: DataСollectionInterface
    {
        public static string path = "task.txt";
        public static List<WorkTask> tasks = new List<WorkTask>();
        
        public static bool Read ()
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; i = i + 7)
                {
                    WorkTask task = new WorkTask(Int32.Parse(lines[i]), lines[i + 1], lines[i + 2], DateTime.Parse(lines[i+3]));
                    task.СreationDate = DateTime.Parse(lines[i+4]);
                    task.Status = lines[i + 5];
                    task.Priority = lines[i + 6];
                    string [] Respons = lines[i + 7].Split("|");
                    foreach (string item in Respons) 
                    {
                        string[] linUser=item.Split(",");
                        User user = new User(linUser[1], linUser[2], linUser[3]);
                        user.Id = Int32.Parse(linUser[0]);
                        task.Responsible.Add(user);                          
                    }  
                    if(DateTime.Compare(task.Deadline, DateTime.Now) < 0) { task.Status = "Просрочено"; }
                    tasks.Add(task);
                }
            }
            return true;
        }
        public static void List(int Id)
        {

        }
        public static bool Add(WorkTask task)
        {
            return true;
        }
        public static bool Search(WorkTask task) 
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
        public static bool Update(WorkTask task)
        {
            return true;
        }
        public static bool Delete(WorkTask task) 
        {
            return true;
        }
        public static bool AddComment (WorkTask task, string comment)
        {
            return true;
        }
        public static bool DeleteComment(WorkTask task, string comment)
        {
            return true;
        }
        public static bool SendMessage()
        {
            return true;
        }
        public static bool AddResponsible(WorkTask task, User user)
        {
            return true;
        }
        
        public static bool GenerateAReport()
        {
            return true;
        }
        public static bool WriteDown()
        {
            using StreamWriter sw = File.CreateText(path);
            foreach (var item in tasks)
            {
                sw.WriteLine($"{item.Id}");
                sw.WriteLine($"{item.Name}");
                sw.WriteLine($"{item.Description}");                
                sw.WriteLine($"{item.Deadline}");
                sw.WriteLine($"{item.СreationDate}");
                sw.WriteLine($"{item.Status}");
                sw.WriteLine($"{item.Priority}");
                if (item.Responsible.Count!= 0)
                {
                    foreach (User user in item.Responsible)
                    {
                        sw.Write($"{user.Id}, {user.Name},{user.Surname},{user.Email}|");
                    }
                }
            }
            return true;
        }
    }
}
