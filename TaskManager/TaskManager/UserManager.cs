using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTaskManager
{
    public class UserManager : DataСollectionInterface
    {
        public static string path = "user.txt";
        public static List<User> users = new List<User>();
        public static void List()
        {
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, {user.Name} {user.Surname}, Email: {user.Email}, Role: {user.Role}");
            }
        }
        public static bool Add()
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();

            Console.Write("Введите фамилию: ");
            string surname = Console.ReadLine();

            Console.Write("Введите email: ");
            string email = Console.ReadLine();

            Console.Write("Введите должность: ");
            string jobTitle = Console.ReadLine();

            Console.Write("Введите роль (например, исполнитель, менеджер): ");
            string role = Console.ReadLine();

            User newUser = new User(name, surname, email)
            {
                Id = users.Count + 1,
                JobTitle = jobTitle,
                Role = role
            };

            users.Add(newUser);
            SaveToFile();
            return true;
        }
        public static User Search(string name, string email)
        {
            return users.FirstOrDefault(u => u.Name == name && u.Email == email);
        }
        public static bool Update()
        {
            Console.Write("Введите ID пользователя для обновления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                User user = users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    Console.WriteLine("Пользователь не найден.");
                    return false;
                }

                Console.Write("Введите новое описание: ");
                user.Description = Console.ReadLine();

                Console.Write("Введите новую должность: ");
                user.JobTitle = Console.ReadLine();

                Console.Write("Введите новую роль: ");
                user.Role = Console.ReadLine();

                SaveToFile();
                return true;
            }
            return false;
        }
        public static bool Delete()
        {
            Console.Write("Введите ID пользователя для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                User user = users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    users.Remove(user);
                    SaveToFile();
                    return true;
                }
            }
            return false;
        }

        public static void LoadFromFile()
        {
            if (!File.Exists(path)) return;

            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length >= 6)
                {
                    User user = new User(parts[1], parts[2], parts[3])
                    {
                        Id = int.Parse(parts[0]),
                        JobTitle = parts[4],
                        Role = parts[5]
                    };
                    users.Add(user);
                }
            }
        }

        public static void SaveToFile()
        {
            using StreamWriter sw = new StreamWriter(path);
            foreach (var user in users)
            {
                sw.WriteLine($"{user.Id}|{user.Name}|{user.Surname}|{user.Email}|{user.JobTitle}|{user.Role}");
            }
        }

    }
}

