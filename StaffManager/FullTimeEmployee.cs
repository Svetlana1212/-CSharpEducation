using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Сотрудник с начислением заработной платы по окладу
    /// </summary>
    public class FullTimeEmployee : Employee
    {
        /// <summary>
        /// Id сотрудика
        /// </summary>
        public int Id {get; set;}

        /// <summary>
        /// Имя сотрудика
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Ставка для начислеия зарлаты
        /// </summary>
        public decimal BaseSalary { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Post {  get; set; }

        /// <summary>
        /// Тип олаты(оклад или часовая ставка)
        /// </summary>
        public string Type = "FullTime";

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">id сотрудика</param>
        /// <param name="name">имя сотрудника</param>
        public FullTimeEmployee(int id, string name) : base(id, name)
        {
        }

        /// <summary>
        /// Метод начисления заработной платы
        /// </summary>
        /// <param name="monthDaysCount">Количество дней в месяце</param>
        /// <param name="workDaysCount">Количество отработанных дней</param>
        /// <returns></returns>
        public decimal CalculateSalary(int monthDaysCount, int workDaysCount)
        {
            decimal salary = Math.Round((this.BaseSalary / monthDaysCount) * workDaysCount, 2);
            return salary;
        }
    }
}
