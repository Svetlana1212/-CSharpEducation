using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Управляет списком сотрудников
    /// </summary>
    public class EmployeeManager: IEmployeeManager<Employee>
    {
        /// <summary>
        /// Путь к файлу, где записаны сотрудники
        /// </summary>
        private static string Path = "staff.txt";

        /// <summary>
        /// Коллекция для хранения списка сотрудиков
        /// </summary>
        public static List<Employee> Staffs = new List<Employee>();

        /// <summary>
        /// Создает нового сотрудника
        /// </summary>
        /// <param name="id">id сотрудника </param>
        /// <returns>Возвращает объект класса Employee</returns>
        public static Employee CreateEmployee(int id)
        {
            Console.WriteLine("Введите имя сотрудника");
            string name = Console.ReadLine();                     
            Console.WriteLine("Введите ставку для оплаты");
            decimal salary;
            try
            {
                salary = Decimal.Parse(Console.ReadLine());
            }            
            catch (FormatException)
            {
                Console.WriteLine("Неправильный формат ставки для оплаты");
                salary = 0;
            }          
            Console.WriteLine("Введите должность сотрудника");
            string post = Console.ReadLine();
            Console.WriteLine("Введите тип оплаты сотрудника(оклад:1/ставка:2)");
            string typeSalary = Console.ReadLine();
            Employee newEmployee;
            if (typeSalary == "1") 
            { 
                newEmployee = new FullTimeEmployee(id,name);
                newEmployee.Type = "FullTime";
            }
            else if(typeSalary == "2")
            {
                newEmployee = new PartTimeEmployee(id,name);
                newEmployee.Type = "PartTime";
            }
            else
            {
                Console.WriteLine("Введен некорректный тип олаты для сотрудника");
                return null;
            }            
            
            newEmployee.BaseSalary=salary;
            newEmployee.Post = post;
            return newEmployee;
        }

        /// <summary>
        /// Добавляет сотрудика в список
        /// </summary>
        /// <param name="employee">Объек класса Employee(сотрудик) </param>
        /// <exception cref="AddIdException"></exception>
        public static void Add(Employee employee) 
        {
            if(Staffs.FirstOrDefault(u => u.Id == employee.Id) != null)
            {
                throw new AddIdException();
            } 
            else 
            {
                Staffs.Add(employee);
                Write();
                Console.WriteLine("Пользователь успешно добавлен");
            }                            
        }

        /// <summary>
        /// Находит сотрудика в списке
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns>Объект сотрудика</returns>
        /// <exception cref="SreachNullException">Если объект пустой</exception>
        public static Employee Get(int id)
        {
            Employee user = Staffs.Find(item => item.Id == id);
            if (user == null)
            {
                throw new SreachNullException();              
            }
            return user;
        }

        /// <summary>
        /// Редактирует информацию о сотруднике
        /// </summary>
        /// <param name="employee">Объект сотрудника</param>
        /// <param name="parametr">Параметр для редактирования</param>
        public static void Update(Employee employee, string parametr) 
        {
            switch (parametr)
            {
                case "salary":
                    Console.Write("Введите новую ставку для расчета заработной платы:");
                    try
                    {
                        employee.BaseSalary = Decimal.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Неправильный формат часовой ставки");                        
                    }
                    break;
                case "post":
                    Console.WriteLine("Введите новую должность:");
                    employee.Post = Console.ReadLine();
                    break;
                case "type":
                    Console.WriteLine("Введите новый тип оплаты пользователя(оклад-1/часовая ставка -2): ");
                    string input = Console.ReadLine();
                    if (input == "1" || input == "2")
                    {
                        employee.Type = (input == "1") ? "FullTime" : "PartTime";                        
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод");
                    }                    
                    break;
                default:
                    Console.WriteLine("Некорректное значение параметра");
                    break;
            }
            Write();
            Console.WriteLine("Информация обновлена");                           
        }

        /// <summary>
        /// Удаляет сотрудика из списка
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <returns>true в случае успеха, иначе false </returns>
        /// <exception cref="DeliteIdException"></exception>
        public static bool Delete(int id)
        {
            Employee employee = Staffs.FirstOrDefault(u => u.Id == id);
            if(employee == null)
            {
                throw new DeliteIdException();
            }
            Console.WriteLine($"Пользователь - {employee.Name}, {employee.Post} будет удален. Продолжить? (да/нет)");
            if (Console.ReadLine() == "да")
            {
                Staffs.Remove(employee);
                Write();                
                return true;
            }
            return false;
        }
        /// <summary>
        /// Считывает данные из файла в список
        /// </summary>
        /// <returns>Список сотрудников</returns>
        public static bool LoadFromFile()
        {
            if (!File.Exists(Path)) return false;
            string[] lines = File.ReadAllLines(Path);
            if (lines.Length >= 1)
            {
                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');
                    for (int i = 0; i < parts.Length; i = i + 5)
                    {
                        Employee employee;
                        if (parts [i+4] == "FullTime") 
                        {
                            employee = new FullTimeEmployee (Int32.Parse(parts[i]),parts[i + 1]);
                        }
                        else
                        {
                            employee = new PartTimeEmployee (Int32.Parse(parts[i]),parts[i + 1]);
                        }                    
                        employee.BaseSalary = Decimal.Parse(parts[i + 2]);
                        employee.Post = parts[i + 3];
                        employee.Type = parts[i + 4];
                        Staffs.Add(employee);
                    }

                }

            }
            return true;
        }

        /// <summary>
        /// Записывет данные в файл
        /// </summary>
        /// <returns>true в случае успеха, иначе false</returns>
        public static bool Write()
        {
            using StreamWriter sw = File.CreateText(Path);
            foreach (var item in Staffs)
            {
                sw.WriteLine($"{item.Id}|{item.Name}|{item.BaseSalary}|{item.Post}|{item.Type}");
            }
            return true;
        }
    }
}
