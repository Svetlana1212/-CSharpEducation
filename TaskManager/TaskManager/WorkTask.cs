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
        public DateTime Deadline { get; set; }
        public DateTime СreationDate { get; set; }
        public string Status { get; set; }        
        public string Priority { get; set; }
        public List<User> Responsible = new List<User>();
        public WorkTask(string name, string description, DateTime deadline) 
        {
            this.Name = name;
            this.Description = description;
            this.Deadline = deadline;
        }

    }
}
