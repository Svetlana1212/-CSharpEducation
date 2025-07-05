using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Интерфейс для управления сотрудниками
    /// </summary>
    /// <typeparam name="Employee">Объект сотрудника</typeparam>
    internal interface IEmployeeManager<Employee>
    {
        /// <summary>
        /// Добавляет сотрудника в коллекцию
        /// </summary>
        /// <param name="employee"></param>
        void Add(Employee employee) { }

        /// <summary>
        /// Находит сотрудника в коллекции по параметру
        /// </summary>
        /// <param name="name">Имя сотрудника(параметр)</param>
        void Get(string name) {  }

        /// <summary>
        /// Редактирует параметры сотрудника
        /// </summary>
        /// <param name="employee">Объект сотрудника</param>
        void Update(Employee employee) { }
    }
}
