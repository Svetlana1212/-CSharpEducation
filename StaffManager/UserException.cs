using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManager
{
    /// <summary>
    /// Если пользователь с таким id уже есть
    /// </summary>
    public class AddIdException: Exception
    {

    }

    /// <summary>
    /// Если удаляемый пользователь не найден
    /// </summary>
    public class DeliteIdException: Exception
    {

    }

    /// <summary>
    /// Если пользователь не найден
    /// </summary>
    public class SreachNullException : Exception
    {

    }

}
