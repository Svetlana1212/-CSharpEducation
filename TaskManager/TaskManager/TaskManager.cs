using System.Reflection.Metadata;

namespace ClassTaskManager
{
    public class TaskManager: DataСollectionInterface
    {
        public static string path = "task.txt";
        public static List<WorkTask> tasks = new List<WorkTask>();
        
        public static void Read ()
        {
            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; i = i + 3)
                {
                    WorkTask task = new WorkTask(lines[i], lines[i + 1], lines[i+2]);
                    tasks.Add(task);
                }
            }
        }
        public static void List(int Id)
        {

        }

        public static bool AddTask()
        {
            Console.Write("Введите название задачи: ");
            string name = Console.ReadLine();

            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();

            Console.Write("Введите срок выполнения задачи (ДД.ММ.ГГГГ): ");
            DateTime deadline = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Укажите приоритет ('Обычный' / 'Срочный'): ");
            string priority = Console.ReadLine();

            WorkTask newWorkTask = new WorkTask(name, description, deadline, priority)
            {
                Id = tasks.Count + 1,
                СreationDate = DateTime.Today                
            };

            tasks.Add(newWorkTask);
            Console.WriteLine($"Задача успешно поставлена!");
            return true;
        }

        public static bool SearchTask(WorkTask task) 
        {
            Console.Write("Введите название задачи: ");
            string name = Console.ReadLine();
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Name == name);
                    return true;
            }
            return false;
        }
        public static bool Sort(string sort)
        {
            return true;
        }
        /*public static bool Filter(string filter)
        {
            return true;
        }*/
        public static bool Update(WorkTask task)
        {
            return true;
        }
        public static bool DeleteTask(int id, WorkTask task)
        {
            Console.Write("Введите номер задачи: ");
            var userImput = Console.ReadLine();
            id = Convert.ToInt32(userImput);
            if (id > tasks.Count)
            {
                return false;
            }
            else
            {
                tasks.Remove(task);
                return true;
            }
                
        }
        public static bool AddComment (WorkTask task, string comment)
        {
            return true;
        }
        public static bool DeleteComment(WorkTask task, string comment)
        {
            return true;
        }
        public static bool AddResponsible(WorkTask task, User user)
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

    }
}
