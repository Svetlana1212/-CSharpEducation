using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTaskManager
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime СreationDate { get; set; }
        public DateTime Deadline { get; set; }

        private string status = "В работе";
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (DateTime.Today > Deadline)
                    status = "Просрочена";
            }
        }
        public string Priority { get; set; }
        public List<User> Responsible {  get; set; }

        public WorkTask() { }
        public WorkTask(string name, 
                        string description, 
                        DateTime deadline,
                        string priority) 
        {
            this.Name = name;
            this.Description = description;
            this.Deadline = deadline;
            this.Priority = priority;
        }

    }
}
