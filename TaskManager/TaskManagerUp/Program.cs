using System.Collections.Generic;
using System.Threading.Tasks;
using ClassTaskManager;
using static System.Net.Mime.MediaTypeNames;

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
                if (CurrentUser == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Добро пожаловать, {CurrentUser.Name}!");
                    Console.ForegroundColor = ConsoleColor.White;
                    do {                 
                        
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
                                    if (freeTasks.Count > 0)
                                    {
                                        TaskManager.ListOutput(freeTasks);
                                    }
                                    else 
                                    {
                                        Console.WriteLine("Нет свободных задач");
                                    }                      
                                    break;
                                case 3:
                                    Console.WriteLine("Введите название задачи: ");
                                    string currentTaskName = Console.ReadLine();
                                    WorkTask currentTask = TaskManager.SearchTask(currentTaskName);
                                    if (currentTask != null)
                                    {
                                        TaskManager.TaskInfo(currentTask);
                                        Console.WriteLine();
                                        CommentManager.ShowComments(currentTask.Id, UserManager.users);
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
                                                    Console.WriteLine($"Вы взяли в работу задачу {currentTask.Name}");
                                                }
                                            }
                                            else if (currenNumber == 2)
                                            {
                                                if (currentTask.Responsible.Find(item => item.Id == CurrentUser.Id) != null)
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
                                                else
                                                {
                                                    Console.WriteLine("Вы не можете менять статус данной задачи");
                                                }
                                            }

                                            else if (currenNumber == 3)
                                            {
                                                Console.WriteLine("Введите текст комментария: ");
                                                string Commenttext = Console.ReadLine();
                                                CommentManager.AddComment(currentTask.Id, CurrentUser, Commenttext);
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
4. Добавить задачу
5. Назначить ответственного к задаче
6. Редактировать задачу
7. Добавить комментарий к задаче
8. Удалить задачу
9. Список пользователей
10. Добавить пользователя
11. Редактировать пользователя
12. Удалить пользователя
13. Выйти");

                            if (int.TryParse(Console.ReadLine(), out int adminChoice))
                            {
                                switch (adminChoice)
                                {
                                    case 1:
                                        List<WorkTask> AllTask = TaskManager.List(CurrentUser);
                                        TaskManager.ListOutput(AllTask);
                                        Console.WriteLine(@" 
1.Сортировать по имени 
2.По описанию
3.По дедлайну
4.По статусу
5.По приоритету");
                                        if (int.TryParse(Console.ReadLine(), out int sortChoice))
                                        {
                                            if (sortChoice == 1) { TaskManager.Sort(AllTask, "Name"); }
                                            else if (sortChoice == 2) { TaskManager.Sort(AllTask, "Description"); }
                                            else if (sortChoice == 3) { TaskManager.Sort(AllTask, "Deadline"); }
                                            else if (sortChoice == 4) { TaskManager.Sort(AllTask, "Status"); }
                                            else if (sortChoice == 5) { TaskManager.Sort(AllTask, "Priority"); }
                                            TaskManager.ListOutput(AllTask);
                                        }
                                        break;
                                    case 2:
                                        List<WorkTask> FreeTask = TaskManager.Filter(TaskManager.tasks, "status", "Свободная"); // Без ответственных
                                        TaskManager.ListOutput(FreeTask);
                                        break;
                                    case 3:
                                        Console.Write("Введите ID пользователя: ");
                                        int.TryParse(Console.ReadLine(), out int uid);
                                        List<WorkTask> userTaskId = TaskManager.Filter(TaskManager.tasks, "responsibleId", uid.ToString());
                                        TaskManager.ListOutput(userTaskId);
                                        break;
                                    case 4:
                                        TaskManager.AddTask();
                                        break;
                                    case 5:
                                        Console.WriteLine("Введите наименование задачи: ");
                                        WorkTask task = TaskManager.SearchTask(Console.ReadLine());
                                        Console.WriteLine("Введите имя пользователя: ");
                                        string userName = Console.ReadLine();
                                        Console.WriteLine("Введите email пользователя: ");
                                        string userEmail = Console.ReadLine();
                                        User user = UserManager.Search(userName, userEmail);
                                        if (TaskManager.AddResponsible(task, user)) { Console.WriteLine($"Вы назначили ответственного {user.Name} {user.Surname} для задачи {task.Name} "); }

                                        break;
                                    case 6:
                                        TaskManager.Update();
                                        //Update(WorkTask task, DateTime deadline, string name = null, string description = null, string status = null)
                                        break;
                                    case 7:
                                        break;
                                    case 8:
                                        TaskManager.DeleteTask();
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

                    } while (true);
                }
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

