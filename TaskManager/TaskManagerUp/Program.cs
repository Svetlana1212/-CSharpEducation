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
                User Ivan = new User("Иван", "Иванов", "email");
                WorkTask task1 = new WorkTask(1, "Задача1", "Очень трудная", DateTime.Today);
                Console.WriteLine(task1.Id);
                task1.Responsible.Add(Ivan);
                TaskManager.tasks.Add(task1);
                TaskManager.WriteDown();
                string str = "0, Иван,Иванов,email|";
                string[] lUser = str.Split("|");
                foreach (string item in lUser)
                {
                    string[] linUser = item.Split(",");
                    int n = linUser.Length;
                    //Console.WriteLine(linUser[n-2]);
                    User user1 = new User(linUser[n-3], linUser[n-2], linUser[n-1]);
                    user1.Id = Int32.Parse(linUser[n-4]);
                    Console.WriteLine(user1.Email);

                }
                 string[] linesUser = lUser[0].Split(",");
                User user = new User(linesUser[1], linesUser[2], linesUser[3]);
                //TaskManager.Read();
                //WorkTask myTask=TaskManager.tasks[0];
                
                User CurrentUser = user;//UserManager.Search(name, email);
                
                
                if (CurrentUser!=null&& CurrentUser.Role!="admin") 
                {
                    TaskManager.Read();
                    Console.WriteLine(@"1. Мои задачи
                                        2. Свободные задачи
                                        3. Найти задачу");
                    int.TryParse(Console.ReadLine(), out int num);
                    if (num == 1)
                    {
                        
                        TaskManager.List(CurrentUser.Id);
                        Console.WriteLine(@"1.Изменить статус задачи
                                            2.Оставить комментарий к задаче");
                    }
                }else if (CurrentUser.Role == "admin")
                {
                    TaskManager.Read();
                    Console.WriteLine(@"1. Список задач
                                        2. Свободные задачи
                                        3. Найти все задачи пользователя
                                        4. Добавить задачу
                                        5. Назначить отвественного задаче
                                        6. Редактировать задачу
                                        7. Добавить комментарий к задаче
                                        8. Удалить задачу");
                }
                else
                {
                    Console.WriteLine("Пользователь не найден");
                }
                


            }
        }
    }
}
