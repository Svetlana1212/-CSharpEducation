using System.Collections.Generic;
using System.Threading.Tasks;
using ClassTaskManager;

namespace TaskManagerUp
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine(@"1. Авторизация
2. Регистрация");
            int.TryParse(Console.ReadLine(), out int number);
            if (number == 1)
            {
                
                Console.WriteLine("Введите имя пользователя");
                string name=Console.ReadLine();
                Console.WriteLine("Введите email");
                string email=Console.ReadLine();
                Console.Clear();
                User Ivan = new User("Иван", "Иванов", "email");
                User Petr = new User("Петр", "Петров", "email");
                DateTime date = new DateTime(2023, 3, 11, 10, 30, 0);
                DateTime date1 = new DateTime(2025, 9, 11, 10, 30, 0);
                DateTime date2 = new DateTime(2025, 8, 15, 12, 20, 0);
                WorkTask task1 = new WorkTask("Создать задачи", "это первая очень трудная задача", date);
                task1.Id = 1;
                task1.Status = "Назначен ответственный";
                task1.Priority = "средняя";
                task1.Responsible.Add(Ivan);
                
                TaskManager.tasks.Add(task1);
                
                WorkTask task2 = new WorkTask("Задача2", "Не очень трудная", date1);
                task2.Id = 2;
                task2.Priority = "средняя";
                task2.Status = "Свободная";
                
                TaskManager.tasks.Add(task2);
                TaskManager.AddResponsible(task2, Petr);

                WorkTask task3 = new WorkTask("Задача3", "Не очень трудная,но бесячая", date2);
                task3.Id = 3;
                task3.Status = "Свободная";
                task3.Priority = "важная";
                TaskManager.tasks.Add(task3);
                TaskManager.WriteDown();
                string str = "0,Иван,Иванов,email|";
                string[] lUser = str.Split("|");
                for (int n=0; n<lUser.Length;n++)
                {
                    string[] linUser = lUser[n].Split(",");
                    
                    if (linUser.Length > 1)
                    {
                        for (int i = 0; i < linUser.Length-1; i = i + 3)
                        {
                            string id = linUser[i];
                            string name1 = linUser[i + 1];
                            string surname = linUser[i + 2];
                            string email1 =  linUser[i + 3];
                            User user1 = new User(name1, surname, email1);
                            user1.Id = Int32.Parse(id);
                        }
                    }

                }
                 string[] linesUser = lUser[0].Split(",");
                User user = new User(linesUser[1], linesUser[2], linesUser[3]);
                user.Role = "admin";
                
                WorkTask myTask=TaskManager.tasks[0];
                TaskManager.СheckingTheDeadline(myTask);
                                
                List <WorkTask> filtrTask=TaskManager.Filter("responsibleSurname", "Петров");
                TaskManager.ListOutput(filtrTask);
                User CurrentUser = user;

                if (CurrentUser != null && CurrentUser.Role != "admin")
                {
                    Console.WriteLine(@"1. Мои задачи
                                        2. Свободные задачи
                                        3. Найти задачу");
                    int.TryParse(Console.ReadLine(), out int num);
                    
                    if (num == 1)
                    {
                        List<WorkTask> works = TaskManager.List(CurrentUser);
                        Console.WriteLine(@"1.Изменить статус задачи
                        2.Оставить комментарий к задаче");
                    }
                }
                else if (CurrentUser.Role == "admin")
                {
                    
                    Console.WriteLine(@"1. Список задач
                                        2. Свободные задачи
                                        3. Найти все задачи пользователя
                                        4. Добавить задачу
                                        5. Назначить отвественного задаче
                                        6. Редактировать задачу
                                        7. Добавить комментарий к задаче
                                        8. Удалить задачу");
                    int.TryParse(Console.ReadLine(), out int n);
                    if (n == 1)
                    {
                        List<WorkTask> works = TaskManager.List(CurrentUser);
                        TaskManager.Sort(works, "Priority");
                        TaskManager.ListOutput(works);
                    }
                }
                else
                {
                    Console.WriteLine("Пользователь не найден");
                }



            }
        }
    }
}
