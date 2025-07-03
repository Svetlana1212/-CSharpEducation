using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace StaffManager
{
    /// <summary>
    /// Менеджер сотрудиков.
    /// Для добавления, удаления, рассчета заработной платы сотрудников
    /// </summary>
    public class StaffManager
    {
        /// <summary>
        /// Путь к файлу, где храниться информация о сотрудиках
        /// </summary>
        private static string Path = "staff.txt";

        /// <summary>
        /// Список пользователей, объектов класса Staff
        /// </summary>
        public static List<Staff> Users = new List<Staff>(); 
        
        /// <summary>
        /// Выводит на экран список пользователей 
        /// </summary>
        /// <param name="staff">Список пользователей</param>
        public static void List(List<Staff> staff)
        {           
            foreach (var user in staff)
            {
                string description = (user.Description != string.Empty) ? user.Description : "не предоставлена";
                string email = (user.Email != string.Empty) ? user.Email : "нет";
                string jobTitle = (user.JobTitle != string.Empty) ? user.JobTitle : "не указана";
                Console.WriteLine($"ID: {user.Id}) {user.Name} {user.Surname}, должность: {jobTitle},  Email: {email}, часовая ставка: {user.HourlyRate}, оклад: {user.Salary},  дополнительная информация:  {description}");
            }
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        /// <returns>Объект нового пользователя</returns>
        public static Staff CreateStaff()
        {           
            bool result = true;
            int Id=0;
            do
            {
                Console.Write("Введите Id: ");
                if (Int32.TryParse(Console.ReadLine(), out int userId))
                {
                    Id = userId;
                    result = true;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод");
                    result = false;
                }
            } while (result == false);            
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию: ");
            string surname = Console.ReadLine();
            Console.Write("Введите описание или введите Enter: ");
            string description = Console.ReadLine();
            Console.Write("Введите должность: ");
            string jobTitle = Console.ReadLine();
            Staff staff = new Staff(Id, name, surname, jobTitle);
            Console.Write("Введите email: ");
            string email = Console.ReadLine();
            Console.Write("Введите часовую ставку (руб): ");
            double hourlyRate;
            if(Double.TryParse(Console.ReadLine(), out hourlyRate))
            {
                staff.HourlyRate = hourlyRate;
            }
            else
            {
                staff.HourlyRate = 0;
            }              
            Console.Write("Введите оклад (руб): ");
            double salary;
            if (Double.TryParse(Console.ReadLine(), out salary))
            {
                staff.Salary = salary;
            }
            else
            {
                staff.Salary = 0;
            }
            staff.Email = email;
            staff.Description = description;
            return staff;
        }

        /// <summary>
        /// Добавляет нового пользователя в коллекцию
        /// </summary>
        /// <param name="newUser"> Пользователь которого надо добавить</param>
        /// <returns>true при успешном добавлении, иначе false</returns>
        public static bool Add(Staff newUser)
        {            
            if ((Users.FirstOrDefault(u => u.Name == newUser.Name)!=null)
                && (Users.FirstOrDefault(u => u.Surname == newUser.Surname)!=null)
                && (Users.FirstOrDefault(u => u.JobTitle == newUser.JobTitle) != null))
            {
                Console.WriteLine($"Такой сотрудик уже есть {newUser.Name} {newUser.Surname} - {newUser.JobTitle}");
                return false;
            }
            else if ((Users.FirstOrDefault(u => u.Id == newUser.Id) != null))
            {
                Console.WriteLine("Пользователь с таким Id уже существует");
                return false;
            }
            else
            {
                Users.Add(newUser);
                Write();
                Console.WriteLine("Пользователь успешно добавлен");
                return true;
            }            
        }        
        
        /// <summary>
        /// Редактирует информацию о cотруднике
        /// </summary>
        /// <param name="id">Id сотрудика</param>
        /// <param name="typeParameter">тип информации для редактирования(дополнительная информация,должность, оклад, часовая ставка, email</param>
        /// <param name="parametr">новое значение парамета</param>
        /// <returns></returns>
        public static bool Update(int id, string typeParameter, string parametr)
        {
            Staff user = Users.Find(item => item.Id == id);
            if (user == null)
            {
                Console.WriteLine("Пользователь не найден");
                return false;
            }
            switch (typeParameter)
            {
                case "description": 
                    user.Description = parametr;
                    break;
                case "jobTitle": 
                    user.JobTitle = parametr;
                    break;
                case "email": 
                    user.Email = parametr;
                    break;
                case "salary":
                 
                    try
                    {
                        double salary = Convert.ToDouble(parametr);
                        user.Salary = salary;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Неправильный формат оклада");
                        return false;
                    }                    
                    break;
                case "hourlyRate":
                    try
                    {
                        double hourlyRate = Convert.ToDouble(parametr);
                        user.HourlyRate = hourlyRate;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Неправильный формат часовой ставки");
                        return false;
                    }
                    break;
                default:
                    return false;                    
            }
            Write();
            Console.WriteLine("Информация обновлена");
            return true;
        } 
        
        /// <summary>
        /// Удаляет сотрудника
        /// </summary>
        /// <param name="Id">Id сотрудника</param>
        /// <returns>true при усешном удалении, иначе false</returns>
        public static bool Delete(int Id)
        {               
            Staff user = Users.FirstOrDefault(u => u.Id == Id);
            if (user != null)
            {
                Console.WriteLine($"Пользователь {user.Name} {user.Surname} {user.JobTitle} будет удален. Продолжить? (да/нет)");
                if (Console.ReadLine() == "да") 
                {
                    Users.Remove(user);
                    Write();                    
                    return true;
                }
                    
            }            
            return false;
        }

        /// <summary>
        /// Находит сотрудника по Id
        /// </summary>
        /// <param name="Id">Id сотрудника</param>
        /// <returns>Объект сотрудника</returns>
        public static Staff Shreach(int Id)
        {
            return Users.Find(item => item.Id == Id);
        }

        /// <summary>
        /// Считывает файл в список
        /// </summary>
        /// <returns>Список сотрудиков</returns>
        public static bool LoadFromFile()
        {
            if (!File.Exists(Path)) return false;
            string[] lines = File.ReadAllLines(Path);            
            if (lines.Length >= 1)
            {
                foreach (string line in lines)
                {                
                    string[] parts = line.Split('|');                                   
                    for (int i = 0; i < parts.Length; i = i + 8)
                    {
                        Staff user = new Staff(int.Parse(parts[i]), parts[i + 1], parts[i + 2], parts[i + 4]);
                        user.Description = parts[i + 3];
                        user.Email = parts[i + 5];
                        user.HourlyRate = Convert.ToDouble(parts[i + 6]);
                        user.Salary = Convert.ToDouble(parts[i + 7]);
                        Users.Add(user);
                    }
                    
                }

            }
            return true;
        }

        /// <summary>
        /// Записывает список сотрудников в файл
        /// </summary>
        /// <returns>true при усешном удалении, иначе</returns>
        public static bool Write()
        {
            using StreamWriter sw = File.CreateText(Path);
            foreach (var item in Users)
            {
                sw.WriteLine($"{item.Id}|{item.Name}|{item.Surname}|{item.Description}|{item.JobTitle}|{item.Email}|{ item.HourlyRate}|{ item.Salary}");
            }
            return true;
        }       

    }
}


