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
                User CurrentUser = UserManager.Search(name, email);
                if (CurrentUser!=null&& CurrentUser.Role!="admin") 
                {
                    
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
