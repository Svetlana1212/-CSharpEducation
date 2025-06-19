using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ClassTaskManager
{
    public class TaskManager: DataСollectionInterface
    {
        public static string path = "task.txt";
        public static List<WorkTask> tasks = new List<WorkTask>();
        public static int WorkTaskId = tasks.Count + 1;

        public static void СheckingTheDeadline(WorkTask myTask)
        {
            if (DateTime.Compare(myTask.Deadline, DateTime.Now) < 0)
            {
                myTask.Status = "Просрочено";

            }
        }
        public static bool Read ()
        {
            string[] lines = File.ReadAllLines(path);
            DateTime date = new DateTime(2023, 3, 11, 10, 30, 0);
            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; i = i + 8)
                {
                    WorkTask task = new WorkTask(lines[i + 1], lines[i + 2], DateTime.Parse(lines[i+3]));
                    task.Id = Int32.Parse(lines[i]);
                    task.СreationDate = DateTime.Parse(lines[i+4]);
                    task.Status = lines[i + 5];
                    task.Priority = lines[i + 6];
                    if(lines[i + 7]!="ответственный не назначен")
                    {

                    }
                    string [] Respons = lines[i + 7].Split("|");
                    foreach (string item in Respons) 
                    {
                        string[] linUser=item.Split(",");
                        if (linUser.Length > 1)
                        {
                            for (int n = 0; n < linUser.Length - 1; n = n + 3)
                            {
                                User user = new User(linUser[n+1], linUser[n+2], linUser[n + 3]);
                                user.Id = Int32.Parse(linUser[n]);
                                task.Responsible.Add(user);
                            }
                        }
                    }
                    
                    tasks.Add(task);
                }
            }
            return true;
        }
        
        public static List<WorkTask> List(User user)
        {
            List<WorkTask> myTasks = new List<WorkTask>();
            foreach (WorkTask task in tasks)
            {
                СheckingTheDeadline(task);
                if (user.Role == "admin")
                {      
                    myTasks.Add(task);
                }
                else
                {
                    foreach (User item in task.Responsible)
                    {
                        if (item.Id == user.Id)
                        {
                            myTasks.Add(task);
                        }
                    }
                    
                }
            }
            
            return myTasks;
        }
        public static void TaskInfo(WorkTask task)
        {
            Console.WriteLine(task.Id);
            Console.WriteLine(task.Name);
            Console.WriteLine(task.Description);
            Console.WriteLine($"Срок выолнения: {task.Deadline}"); 
            Console.WriteLine($"Дата создания: {task.СreationDate}");
            Console.WriteLine($"Номер: { task.Status}");        
            Console.WriteLine(task.Priority);
            foreach(User item in task.Responsible)
            {
                Console.Write($"{item.Name}");                
                Console.Write($" {item.Surname}");
                Console.WriteLine();
            }
            /*foreach (string comment in comments)
            {
                Console.WriteLine(comment);
            }*/

        }
       /* public static bool Add(WorkTask task)
        {
            return true;
        }*/
        /*public static bool Search(WorkTask task) 
        {
            return true;
        }*/
        public static bool Sort(string sort, string sortingDirection)
        {
            return true;
        }
        public static List<WorkTask> Filter(string condition,string meaning)
        {
            List<WorkTask> filtrTask = new List<WorkTask>();
           
            foreach (WorkTask task in tasks)
            {

                bool filter=false;
                if (condition == "status")
                {
                    filter = (task.Status == meaning) ? true : false;
                }
                else if (condition == "priority")
                {
                    filter = (task.Priority == meaning) ? true : false;
                }
                else if (condition == "responsibleSurname")
                {
                    User found = task.Responsible.Find(item => item.Surname == meaning);                    
                    filter = (found!=null) ? true : false;
                }
                else if (condition == "responsibleId")
                {
                    
                    User found = task.Responsible.Find(item => item.Id == Int32.Parse(meaning));
                    filter = (found != null)  ? true : false;
                }
                else
                {
                    Console.WriteLine("Некорректные условия поиска");
                }
                if (filter)
                {
                    filtrTask.Add(task);
                }
            }              
            
            return filtrTask;
        }
        public static bool Update(WorkTask task)
        {
            return true;
        }
        /*public static bool Delete(WorkTask task) 
        {
            return true;
        }*/
        /*public static bool AddComment (WorkTask task, string comment)
        {
            return true;
        }
        public static bool DeleteComment(WorkTask task, string comment)
        {
            return true;
        }*/
        public static bool SendMessage(string email)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("yourusername@gmail.com", "yourpassword"),
                EnableSsl = true
            };

            MailMessage message = new MailMessage
            {
                From = new MailAddress("yourusername@gmail.com"),
                Subject = "Тестовое сообщение",
                Body = "Это тело тестового сообщения."
            };
            message.To.Add(email);

            try
            {
                client.Send(message);
                Console.WriteLine("Письмо отправлено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке письма: " + ex.Message);
            }
            finally
            {
                message.Dispose();
                client.Dispose();
            }
            return true;
        }
        
        public static bool AddResponsible(WorkTask task, User user)
        {
            task.Responsible.Add(user);
            task.Status = "назначен ответственный";
           // SendMessage(user.Email);
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
                        sw.Write($"{user.Id}, {user.Name},{user.Surname},{user.Email}|\n");
                    }
                }
                else
                {
                    sw.WriteLine("ответственный не назначен");
                }

            }
            return true;
        }
    }
}
