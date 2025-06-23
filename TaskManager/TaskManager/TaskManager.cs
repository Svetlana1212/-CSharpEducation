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
        public static string reportPath = "report.txt";
        
        public static List<WorkTask> tasks = new List<WorkTask>();
        public static int WorkTaskId = tasks.Count + 1;

        
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
                    if (lines[i + 7] != "ответственный не назначен")
                    {
                        string[] Respons = lines[i + 7].Split("|");
                        foreach (string item in Respons)
                        {
                            string[] linUser = item.Split(",");
                            if (linUser.Length > 1)
                            {
                                for (int n = 0; n < linUser.Length - 1; n = n + 3)
                                {
                                    User user = new User(linUser[n + 1], linUser[n + 2], linUser[n + 3]);
                                    user.Id = Int32.Parse(linUser[n]);
                                    task.Responsible.Add(user);
                                }
                            }
                        }
                    }
                    tasks.Add(task);
                }
            }
            return true;
        }
        public static void deadlineСheck(WorkTask task)
        {
            int result = DateTime.Compare(DateTime.Today, task.Deadline);
            if (result > 0) 
            {
                task.Status = "Просрочена";
            }
        }
        public static List<WorkTask> List(User user)
        {
            List<WorkTask> myTasks = new List<WorkTask>();
            foreach (WorkTask task in tasks)
            {
                deadlineСheck(task);
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

            WorkTask newWorkTask = new WorkTask(name, description, deadline)
            {
                Id = tasks.Count + 1,
                СreationDate = DateTime.Today
            };
            newWorkTask.Priority = priority;
            newWorkTask.Status = "Свободная";
            tasks.Add(newWorkTask);
            WriteDown();
            Console.WriteLine($"Задача успешно поставлена!");
            return true;
        }
        public static WorkTask SearchTask(string name)
        {
            WorkTask SearchTask= new WorkTask(null,null,DateTime.Now);
            foreach(WorkTask task in tasks)
            {
                if (task.Name == name)
                {
                    SearchTask = task;
                }
                
            }
            return SearchTask;
        }
        public static bool Update()
        {
            Console.Write("Введите название задачи: ");
            string name = Console.ReadLine();
            WorkTask task = SearchTask(name);
            Console.Write("Введите описание задачи: ");
            task.Description = Console.ReadLine();
            Console.Write("Введите срок выполнения задачи (ДД.ММ.ГГГГ): ");
            DateTime deadline = Convert.ToDateTime(Console.ReadLine());
            task.Deadline = deadline;
            Console.Write("Укажите приоритет ('Обычный' / 'Срочный'): ");
            task.Priority = Console.ReadLine();
            Console.Write("Укажите статус ('Свободная' / 'Назначен ответственный'/'В обсуждении'/'В работе'/'Выполнена'/'Закрыта'): ");
            task.Status = Console.ReadLine();
            WriteDown();
            Console.WriteLine($"Задача успешно отредактирована!");
            return true;
        }
        public static bool DeleteTask()
        {
            Console.WriteLine("Введите наименование задачи: ");
            string name = Console.ReadLine();
            WorkTask task = SearchTask(name);
            tasks.Remove(task);
            WriteDown();
            //if (WriteDown())
            //{
                Console.WriteLine($"Задача успешно удалена!");
                return true;
            //}
           // return false;
        }


        public static bool Sort(List<WorkTask> works, string sort)
        {
            if(sort=="Name")
                works.Sort((task1, task2) => task1.Name.CompareTo(task2.Name));
            else if (sort == "Description")
                works.Sort((task1, task2) => task1.Description.CompareTo(task2.Description));
            else if(sort == "Deadline")
                works.Sort((task1, task2) => task1.Deadline.CompareTo(task2.Deadline));
            else if (sort == "СreationDate")
                works.Sort((task1, task2) => task1.СreationDate.CompareTo(task2.СreationDate));
            else if(sort == "Priority")
                works.Sort((task1, task2) => task1.Priority.CompareTo(task2.Priority));
            else if(sort == "Status")
                works.Sort((task1, task2) => task1.Status.CompareTo(task2.Status));            
            else
            {
                return false;
            }
            return true;
        }
        public static List<WorkTask> Filter(List<WorkTask> works, string condition,string meaning)
        {
            List<WorkTask> filtrTask = new List<WorkTask>();
           
            foreach (WorkTask task in works)
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
                /*else if (condition == "responsibleSurname")
                {
                    User found = task.Responsible.Find(item => item.Surname == meaning);                    
                    filter = (found!=null) ? true : false;
                }*/
                else if (condition == "responsibleId")
                {
                    
                    User found = task.Responsible.Find(item => item.Id == Int32.Parse(meaning));
                    filter = (found != null)  ? true : false;
                }
                else
                {
                    Console.WriteLine("Некорректные условия фильтрации");
                }
                if (filter)
                {
                    filtrTask.Add(task);
                }
            }              
            
            return filtrTask;
        }

       /* public static void AddComment(WorkTask task, User user, string comment)
        {
            CommentManager.AddComment(task.Id, user, comment);
        }

        public static void ShowComments(WorkTask task)
        {
            CommentManager.ShowComments(task.Id, UserManager.users);
        }


        public static bool DeleteComment(int commentId, User currentUser)
        {
            return CommentManager.DeleteComment(commentId, currentUser);
        }

        public static bool EditComment(int commentId, User currentUser, string newText)
        {
            return CommentManager.EditComment(commentId, currentUser, newText);
        }*/

        public static bool SendMessage(WorkTask task, User user)
        {
             
            MailAddress from = new MailAddress("somemail@gmail.com", "Tom");
            MailAddress to = new MailAddress(user.Email);

            
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Вас назначили ответственным за задачу";
            m.Body = "<h2>Вас назначили ответственным на задачу</h2>"+task.Id+" "+task.Name;
            m.IsBodyHtml = true;

             
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 465);
            smtp.Credentials = new NetworkCredential("pl.swetik@yandex.ru", "Sos197sos");
            smtp.EnableSsl = true;
            smtp.Send(m);

            try
            {
                smtp.Send(m);
                Console.WriteLine("Письмо отправлено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при отправке письма: " + ex.Message);
            }
            
            return true;
        }
        
        public static bool AddResponsible(WorkTask task, User user)
        {
            User found = task.Responsible.Find(item => item.Id == user.Id);
            bool res=false;
            if (found==null) 
            {
                task.Responsible.Add(user);
                if (task.Status == "Свободная") 
                {
                    task.Status = "назначен ответственный";
                }
                if (WriteDown())
                {
                    res=true;
                }
            }                
            else 
            {
                Console.WriteLine("Пользователь уже назначен ответственным для этой задачи");                
            }
            return res;
                //SendMessage(task, user);               
        }
        public static bool DelAllResponsible(WorkTask task)
        {
            task.Responsible.Clear();
            task.Status = "Свободная";            
            return true;
        }
        public static void ListOutput(List<WorkTask> workTasks)
        {
            foreach (WorkTask task in workTasks)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{task.Name} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Описание: {task.Description}");
                Console.Write($"Срок выполнения: {task.Deadline} ");
                Console.Write($"Статус: {task.Status} ");
                Console.WriteLine($"Приоритет: {task.Priority}");
                if (task.Status!="Свободная")
                {
                    Console.Write("Ответственные: ");
                    foreach (User item in task.Responsible)
                    {
                        Console.Write($" {item.Name}");
                        Console.Write($" {item.Surname}  ");
                    }
                }
                Console.WriteLine();

                
            }
        }
        public static void TaskInfo(WorkTask task)
        {
            Console.WriteLine($"Задача номер {task.Id}");
            Console.WriteLine(task.Name);
            Console.WriteLine(task.Description);
            Console.WriteLine($"Срок выолнения: {task.Deadline}");
            Console.WriteLine($"Дата создания: {task.СreationDate}");
            Console.WriteLine($"Статус: {task.Status}");
            Console.WriteLine($"Приоритет: {task.Priority}");
            Console.Write("Ответственные:");
            foreach (User item in task.Responsible)
            {
                Console.Write($" {item.Name}");
                Console.Write($" {item.Surname}");                
            }
            /*foreach (string comment in comments)
            {
                Console.WriteLine(comment);
            }*/

        }

        public static bool GenerateAReport()
        {
            using StreamWriter sw = File.CreateText(reportPath);
            foreach (var item in tasks)
            {
                sw.Write($"Номер задачи: {item.Id}");
                sw.Write($" Название: {item.Name}");
                sw.Write($" Описание: {item.Description}");
                sw.Write($" Дедлайн: {item.Deadline}");
                sw.Write($" Дата создания: {item.СreationDate}");
                sw.Write($" Текущий статус: {item.Status}");
                sw.Write($" Приоритет: {item.Priority}");
                if (item.Responsible.Count != 0)
                {
                    foreach (User user in item.Responsible)
                    {
                        sw.Write($" Ответственные: {user.Id}, {user.Name},{user.Surname},{user.Email}\n");
                    }
                }
                else
                {
                    sw.WriteLine("ответственный не назначен");
                }

            }
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
                    sw.Write("\n");
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
