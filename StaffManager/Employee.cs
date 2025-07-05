using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Сотрудик
    /// </summary>
    public abstract class Employee
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
        public string Type { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <param name="name">имя сотрудника</param>
        public Employee(int id, string name) 
        { 
            this.Id = id;
            this.Name = name;
        }
        public decimal CalculateSalary()
        {
            decimal salary=0;
            return  salary;
        }
    }
}
