using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;

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
            try 
            {
                StaffManager.LoadFromFile();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Ошибка чтения файла: {e.Message}");                
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Ошибка чтения из файла: {e.Message}");
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Ошибка формата данных: {e.Message}");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }

            bool programm = true;
            
            do
            {
                Console.WriteLine(@"
1. Все сотрудники
2. Добавить сотрудника
3. Посмотреть информацию о сотруднике
4. Удалить сотрудника
5. Редактировать информацию о сотруднике
6. Рассчитать зарплату сотрудику
7. Очистить экран
8. Выйти");
                int choice = ReturnId();
                List<Staff> StaffList = new List<Staff>();
                switch (choice)
                {
                    case 1:                        
                        Console.WriteLine("Список сотрудников");
                        StaffManager.List(StaffManager.Users);
                        break;
                    case 2:                        
                        Staff user = StaffManager.CreateStaff();
                        StaffManager.Add(user);
                        break;
                    case 3:
                        int staffId = ReturnId("Введите Id сотрудника: ");
                        Staff staff = StaffManager.Shreach(staffId);
                        if (staff != null)
                        {
                            StaffList.Add(staff);
                            StaffManager.List(StaffList);
                        }
                        else
                        {
                            Console.WriteLine("Пользователь не найден");
                        }
                        break;
                    case 4:
                        int delId = ReturnId("Введите Id сотрудника: ");
                        if (StaffManager.Delete(delId))
                        {
                            Console.WriteLine("Пользователь успешно удален");
                        }
                        else
                        {
                            Console.WriteLine("Удаление отменено");

                        }
                        break;
                    case 5:
                        int updateUserId = ReturnId("Введите Id сотрудника: ");
                        Console.WriteLine(@"Выберите параметр для редактирования:
1.описание 
2.должность 
3.email,
4.оклад, 
5.часовая ставка");
                        int typeNumber = ReturnId();
                        string typeParameter = "";
                        switch (typeNumber)
                        {
                            case 1:
                                typeParameter = "description";
                                break;
                            case 2:
                                typeParameter = "jobTitle";
                                break;
                            case 3:
                                typeParameter = "email";
                                break;
                            case 4:
                                typeParameter = "salary";
                                break;
                            case 5:
                                typeParameter = "hourlyRate";
                                break;
                            default:
                                Console.WriteLine("Некорректный ввод");
                                break;

                        }
                        Console.WriteLine("Введите значение параметра:");
                        string parameter = Console.ReadLine();
                        StaffManager.Update(updateUserId, typeParameter, parameter);                                                
                        break;
                    case 6:
                        Console.WriteLine(@"Выберите способ расчета зарплаты:
1. По окладу
2. Сдельная");
                        int typeSalary = ReturnId();
                        int userSalary = ReturnId("Введите Id сотрудника: ");
                        Staff salaryStaff = StaffManager.Shreach(userSalary);
                        if (salaryStaff != null)
                        {
                            if (typeSalary == 1)
                            {
                                int monthDaysCount = ReturnId("Введите количество рабочих дней в месяце (число от 21 до 25)");
                                int workDaysCount = ReturnId("Введите количество отработанных сотрудником дней в месяце");
                                try
                                {
                                    double workSalary = salaryStaff.СalculateSalary(monthDaysCount, workDaysCount);
                                    Console.WriteLine($" {salaryStaff.JobTitle} {salaryStaff.Surname} {salaryStaff.Name} заработал в текущем месяце {workSalary} руб.  ");
                                }
                                catch (MonthDaysCountException)
                                {
                                    Console.WriteLine("Некорректный ввод количества рабочих дней в месяце(число от 21 до 25)");
                                }
                            }
                            else if (typeSalary == 2)
                            {
                                int workCount = ReturnId("Введите количество отработанного времени(час)");
                                double workPieceworkPay = salaryStaff.PieceworkPay(workCount);
                                Console.WriteLine($" {salaryStaff.JobTitle} {salaryStaff.Surname} {salaryStaff.Name} заработал в текущем месяце {workPieceworkPay} руб.  ");
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Пользователь не найден");
                        }
                        break;
                    case 7:
                        Console.Clear();                        
                        break;
                    case 8:                        
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
