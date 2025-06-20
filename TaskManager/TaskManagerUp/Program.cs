using System.Threading.Tasks;
using ClassTaskManager;

namespace TaskManagerUp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserManager.LoadFromFile();

            Console.WriteLine(@"1. Авторизация
2. Регистрация");
            int.TryParse(Console.ReadLine(), out int number);

            if (number == 1)
            {
                Console.WriteLine("Введите имя пользователя:");
                string name = Console.ReadLine();
                Console.WriteLine("Введите email:");
                string email = Console.ReadLine();

                User CurrentUser = UserManager.Search(name, email);

                if (CurrentUser != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Добро пожаловать, {CurrentUser.Name}!");

                    if (CurrentUser.Role != "admin")
                    {
                        Console.WriteLine(@"
1. Мои задачи
2. Свободные задачи
3. Найти задачу
4. Выйти");

                        int.TryParse(Console.ReadLine(), out int num);
                        switch (num)
                        {
                            case 1:
                                TaskManager.List(CurrentUser);
                                break;
                            case 2:
                                TaskManager.Filter(TaskManager.tasks, "status", "Свободная"); // Свободные задачи без ответственного
                                break;
                            case 3:
                                Console.WriteLine("Функция поиска задачи пока не реализована.");
                                break;
                            case 4:
                                return;
                        }
                    }
                    else // если админ
                    {
                        while (true)
                        {
                            Console.WriteLine(@"
1. Список задач
2. Свободные задачи
3. Найти задачи пользователя
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

                            int.TryParse(Console.ReadLine(), out int adminChoice);

                            switch (adminChoice)
                            {
                                case 1:
                                    TaskManager.List(CurrentUser); // Показать все
                                    break;
                                case 2:
                                    TaskManager.Filter(TaskManager.tasks, "status", "Свободная"); // Без ответственных
                                    break;
                                case 3:
                                    Console.Write("Введите ID пользователя: ");
                                    int.TryParse(Console.ReadLine(), out int uid)
                                        TaskManager.Filter(TaskManager.tasks, "responsibleId", uid.ToString());
                                        break;
                                        Console.WriteLine("Некорректный ввод");
                                case 4:
                                            Console.WriteLine("Добавление задачи пока не реализовано.");
                                            break;
                                        case 5:
                                    Console.WriteLine("Введите наименование задачи: ");
                                    

                                    WorkTask task = TaskManager.SearchTask(Console.ReadLine());
                                    Console.WriteLine("Введите имя пользователя: ");
                                    string name = Console.ReadLine();
                                    Console.WriteLine("Введите email пользователя: ");
                                    string email = Console.ReadLine();
                                    User user = Search(name, email);
                                        TaskManager.AddResponsible(task, user)
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
                }
                else
                {
                    Console.WriteLine("Пользователь не найден.");
                }
            }
            else if (number == 2)
            {
                UserManager.Add();
                Console.WriteLine("Регистрация завершена.");
            }
        }
    }
}