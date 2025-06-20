using System.Threading.Tasks;
using ClassTaskManager;

namespace TaskManagerUp
{
    internal class Program
    {
        static void Main(string[] args)
        {            
                Console.WriteLine(@"
                1. Авторизация
                2. Регистрация");
                TaskManager.Read();
                UserManager.LoadFromFile();
                int.TryParse(Console.ReadLine(), out int number);
            
                if (number == 1) 
                {                    
                        Console.WriteLine("Введите имя пользователя:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите email:");
                        string email = Console.ReadLine();
                        User CurrentUser = UserManager.Search(name, email);
                        if(CurrentUser == null)
                        {
                            Console.WriteLine("Пользователь не найден.");
                        }
                        else
                        {
                            
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Добро пожаловать, {CurrentUser.Name}!");
                        Console.ForegroundColor = ConsoleColor.White;

                    if (CurrentUser.Role != "admin")
                            {
                                Console.WriteLine(@"
    1. Мои задачи
    2. Свободные задачи
    3. Посмотреть информацию о задаче
    4. Выйти");
                            
                            int.TryParse(Console.ReadLine(), out int num);

                            switch (num)
                            {
                                case 1:
                                    List<WorkTask> workTasks = TaskManager.List(CurrentUser);
                                    if (workTasks.Count != 0)
                                    {
                                        TaskManager.ListOutput(workTasks); 
                                    }                                                         
                                    else
                                    {
                                        Console.WriteLine("Нет задач в работе");
                                    }
                                    break;
                                case 2:
                                    List<WorkTask> freeTasks = TaskManager.Filter(TaskManager.tasks, "status", "Свободная");
                                    TaskManager.ListOutput(freeTasks);
                                    break;
                                case 3:
                                    Console.WriteLine("Введите название задачи: ");
                                    WorkTask currentTask = TaskManager.SearchTask(Console.ReadLine());
                                    if (currentTask != null)
                                    {
                                        TaskManager.TaskInfo(currentTask);
                                        Console.WriteLine(@"
    1.Взять задачу в работу
    2.Изменить статус
    3.Добавить комментарий");
                                    if ((int.TryParse(Console.ReadLine(), out int currenNumber))) 
                                    {
                                        if (currenNumber == 1)
                                        {

                                            if (TaskManager.AddResponsible(currentTask, CurrentUser))
                                            {
                                                if (TaskManager.WriteDown()) { Console.WriteLine($"Вы взяли в работу задачу {currentTask.Name}"); }
                                            }
                                        }
                                    else if (currenNumber == 2)
                                    {
                                        Console.WriteLine("Выбирите статус: ");
                                        Console.WriteLine(@"
1.Обсуждение
2.В работе
3.Выполнена");
                                            if (int.TryParse(Console.ReadLine(), out int statusNum))
                                            {
                                                switch (statusNum)
                                                {
                                                    case 1:
                                                        currentTask.Status = "Обсуждение";
                                                        break;
                                                    case 2:
                                                        currentTask.Status = "В работе";
                                                        break;
                                                    case 3:
                                                        currentTask.Status = "Выполнена";
                                                        break;
                                                }
                                                if (TaskManager.WriteDown())
                                                {
                                                    Console.WriteLine("Вы изменили статус задачи");
                                                }
                                            }
                                        }
                                        else if (currenNumber == 3)
                                        {
                                            //добавление комментария
                                        }
                                    }
                                        
                                                    
                                            

                                }                                                       

                                break;
                                case 4:
                                    return;
                             }
                        }
                        else // если админ
                        {
                               Console.WriteLine(@"
1. Список задач
2. Свободные задачи
3. Найти задачи пользователя
4. Посмотреть задачу
4. Добавить задачу
5. Назначить ответственного
6. Редактировать задачу
7. Добавить комментарий
8. Удалить задачу
9. Список пользователей
10. Добавить пользователя
11. Редактировать пользователя
12. Удалить пользователя
13. Выйти");

                                if(int.TryParse(Console.ReadLine(), out int adminChoice))
                                {
                                    switch (adminChoice)
                                    {
                                        case 1:
                                            List<WorkTask> AllTask=TaskManager.List(CurrentUser);
                                            TaskManager.ListOutput(AllTask);
                                            break;
                                        case 2:
                                            List<WorkTask> FreeTask = TaskManager.Filter(TaskManager.tasks, "status", "Свободная"); // Без ответственных
                                            TaskManager.ListOutput(FreeTask);
                                            break;
                                        case 3:
                                            Console.Write("Введите ID пользователя: ");
                                            int.TryParse(Console.ReadLine(), out int uid);
                                            TaskManager.Filter(TaskManager.tasks, "responsibleId", uid.ToString());
                                            break;
                                            Console.WriteLine("Некорректный ввод");
                                        case 4:
                                            Console.WriteLine("Добавление задачи пока не реализовано.");
                                            break;
                                        case 5:
                                            Console.WriteLine("Введите наименование задачи: ");
                                            WorkTask task = TaskManager.tasks[0];//SearchTask(Console.ReadLine());
                                            Console.WriteLine("Введите имя пользователя: ");
                                            string userName = Console.ReadLine();
                                            Console.WriteLine("Введите email пользователя: ");
                                            string userEmail = Console.ReadLine();
                                            User user = UserManager.Search(userName, userEmail);
                                            TaskManager.AddResponsible(task, user);
                                    //Console.WriteLine("Назначение ответственного пока не реализовано.");
                                            break;
                                        case 9:
                                            UserManager.List();
                                            break;
                                        case 10:
                                            UserManager.Add();
                                            break;
                                        case 11:
                                            UserManager.Update();
                                            break;
                                        case 12:
                                            UserManager.Delete();
                                            break;
                                        case 13:
                                            return;
                            }
                        }

                                
                            }
                       
                    } //while (true);
                } 
                    
                
                else if (number == 2)
                {
                    if (UserManager.Add()) 
                    {                     
                        Console.WriteLine("Регистрация завершена");
                    }
                    else
                    {
                        Console.WriteLine("Произошла ошибка");
                    }
            } 
        }
           
    }
}

