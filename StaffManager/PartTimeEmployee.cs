using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Сотрудник с начислением оплаты по часовой ставке
    /// </summary>
    public class PartTimeEmployee : Employee
    {
        /// <summary>
        /// Id сотрудика
        /// </summary>
        public int Id { get; set; }

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
        public string Post { get; set; }

        /// <summary>
        /// Тип олаты(оклад или часовая ставка)
        /// </summary>
        public string Type = "PartTime";

        public PartTimeEmployee(int id, string name) : base(id, name)
        {
        }

        public decimal CalculateSalary(decimal workCount)
        {
            decimal salary = Math.Round(this.BaseSalary * workCount);
            return salary;
        }
    }
}
