using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Staff
    {
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя сотрудика
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Фамилия сотрудика
        /// </summary>
        public string Surname { get; }

        /// <summary>
        /// Дополнительная информация о сотрудике
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Должность сотрудика
        /// </summary>
        public string JobTitle { get; set; }

        /// <summary>
        /// Электронная почта сотрудика
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Часовая ставка сотрудика
        /// </summary>
        public double HourlyRate {  get; set; }

        /// <summary>
        /// Оклад сотрудика
        /// </summary>
        public double Salary {  get; set; }

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="id">Id сотрудника</param>
        /// <param name="name">Имя сотрудника</param>
        /// <param name="surname">Фамилия сотрудника</param>
        /// <param name="jobTitle">Должность сотрудника</param>
        public Staff(int id, string name, string surname, string jobTitle)
        {
            this.Id = id;
            this.Name = name;
            this.Surname = surname;
            this.JobTitle = jobTitle;
        }

        /// <summary>
        /// Начисляет зароботную плату по окладу
        /// </summary>
        /// <param name="monthDaysCount"> Количество рабочих дней в месяце</param>
        /// <param name="workDaysCount">Количество дней, отработанных сотрудником в месяце</param>
        /// <returns>Возвращает произведение оклада сотрудника на количество обработанных дней, разделенное на количество рабочих дней в месяце</returns>
        /// <exception cref="MonthDaysCountException"> Исключение для обработки случаев, когда введено не корректное количество рабочих дней в месяце</exception>
        public double СalculateSalary(int monthDaysCount, int workDaysCount)
        {
            if (monthDaysCount<=18 || monthDaysCount>25) 
            {
                throw new MonthDaysCountException();
            }
            return Math.Round((this.Salary / monthDaysCount) * workDaysCount, 2);      
        }

        /// <summary>
        /// Начисляет заработную плату по часовой ставке
        /// </summary>
        /// <param name="workCount">Количество отработанных часов</param>
        /// <returns>Возвращает произведение часовой ставки на количество обработанных часов</returns>
        public double PieceworkPay(int workCount)
        {
            return Math.Round(this.HourlyRate * workCount);
        }
    }
}
