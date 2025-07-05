using System;

namespace StaffManager
{
    internal class Program
    {
        static int ReturnId(string message = "")
        {
            int userId;
            do
            {
                Console.WriteLine(message);
            }
            while (!Int32.TryParse(Console.ReadLine(), out userId));
            return userId;
        }

        static void Main(string[] args)
        {
            bool programm = true;
            EmployeeManager.LoadFromFile();
            
            do
            {
                Console.WriteLine(@"
1. Добавить сотрудника c полным описанием
2. Добавить сотрудника с частичным описанием
3. Получить информацию о сотруднике
4. Обновить данные сотрудника
5. Удалить сотрудника
6. Очистить экран
7. Выйти
Выберите действие:");
                int choice = ReturnId();
                switch (choice)
                {
                    case 1:
                        int newId = ReturnId("Введите id сотрудника: ");
                        Employee newEmployee = EmployeeManager.CreateEmployee(newId);
                        if (newEmployee != null)
                        {
                            try
                            {
                                EmployeeManager.Add(newEmployee);
                            }                            
                            catch (AddIdException)
                            {
                                Console.WriteLine("Пользователь с таким Id уже существует");
                            }
                            catch (NullReferenceException)
                            {
                                Console.WriteLine("Невозможно добавить данного пользователя");
                            }
                        }                                                                       
                        break;
                    case 2:
                        int userId = ReturnId("Введите id сотрудника: ");
                        Console.WriteLine("Введите имя сотрудика: ");
                        string name = Console.ReadLine();                        
                        Console.WriteLine("Введите тип оплаты сотрудника(оклад:1/ставка:2)");
                        string typeSalary = Console.ReadLine();
                        Employee newBriefEmployee = null;
                        if ((typeSalary != "1")&&(typeSalary != "2"))
                        {
                            Console.WriteLine("Введен некорректный тип олаты для сотрудника");
                            break;
                        } 
                        else if (typeSalary == "1")
                        {
                            newBriefEmployee = new FullTimeEmployee(userId, name);
                        }
                        else if (typeSalary == "2")
                        {
                            newBriefEmployee = new PartTimeEmployee(userId, name);                            
                        }
                        try 
                        { 
                            EmployeeManager.Add(newBriefEmployee);
                        }
                        catch (AddIdException)
                        {
                            Console.WriteLine("Пользователь с таким Id уже существует");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Невозможно добавить данного пользователя");
                        }
                        break;
                    case 3:
                        int viewedId = ReturnId("Введите id сотрудника: ");
                        try
                        {
                            Employee viewedEmployee = EmployeeManager.Get(viewedId);
                            if (viewedEmployee != null)
                            {
                                string salaryType = (viewedEmployee.Type == "FullTime") ? "оклад" : "часовая ставка";
                                Console.WriteLine($"Сотрудник - Имя:{viewedEmployee.Name}, должность:{viewedEmployee.Post}, {salaryType}: {viewedEmployee.BaseSalary}");
                            }
                        }
                        catch (SreachNullException)
                        {
                            Console.WriteLine("Пользователь с таким id не найден");
                        }
                        break;
                    case 4:
                        int updateId = ReturnId("Введите id сотрудника: ");
                        try
                        {                            
                            Employee updateEmployee = EmployeeManager.Get(updateId);
                            string salaryType = (updateEmployee.Type == "FullTime") ? "оклад" : "часовая ставка";
                            Console.WriteLine($"Сотрудник - Имя:{updateEmployee.Name}, должность:{updateEmployee.Post}, {salaryType}: {updateEmployee.BaseSalary}");
                            Console.WriteLine(@"Выберите параметр, который нужно изменить
1.тип оплаты 
2.оклад(часовую ставку)
3.должность
");
                            switch (Int32.Parse(Console.ReadLine()))
                            {
                                case 1:
                                    EmployeeManager.Update(updateEmployee, "type");
                                    break;
                                case 2:
                                    EmployeeManager.Update(updateEmployee, "salary");
                                    break;
                                case 3:
                                    EmployeeManager.Update(updateEmployee, "post");
                                    break;
                                default:
                                    Console.WriteLine("Некорректный ввод");
                                    break;
                            }
                        }
                        catch (SreachNullException)
                        {
                            Console.WriteLine("Пользователь с таким id не найден");
                        }
                        break;
                    case 5:
                        int delId = ReturnId("Введите id сотрудника: ");
                        try
                        {
                            EmployeeManager.Delete(delId);
                            Console.WriteLine("Пользователь успешно удален");
                        }
                        catch(DeliteIdException)
                        {
                            Console.WriteLine("Пользователь с таким Id не найден");
                        }
                        break;
                    case 6:
                        Console.Clear();
                        break;
                    case 7:
                        programm = false;
                        break;
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;

                }

            } while (programm == true);
        }
    }
}
