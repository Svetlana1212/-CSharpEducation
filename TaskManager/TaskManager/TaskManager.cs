using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ClassTaskManager
{
    public class TaskManager: DataСollectionInterface
    {
        public static bool AddTask()
        {
            Console.Write("Введите название задачи: ");
            string name = Console.ReadLine();

            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();

            Console.Write("Введите срок выполнения задачи (ДД.ММ.ГГГГ): ");
            DateTime deadline = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Укажите приоритет ('Обычный' / 'Срочный'): ");
            string priority = Console.ReadLine();

            WorkTask newWorkTask = new WorkTask(name, description, deadline)
            {
                Id = tasks.Count + 1,
                СreationDate = DateTime.Today                
            };

            tasks.Add(newWorkTask);
            Console.WriteLine($"Задача успешно поставлена!");
            return true;
        }

        public static WorkTask SearchTask(string name)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Name == name)
                    return tasks[i];
            }
                        
        }
        public static bool DeleteTask(int id, WorkTask task)
        {
            Console.Write("Введите номер задачи: ");
            var userImput = Console.ReadLine();
            id = Convert.ToInt32(userImput);
            if (id > tasks.Count)
            {
                return false;
            }
            else
            {
                tasks.Remove(task);
                return true;
            }
                
        }
        
    }
}
