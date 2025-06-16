using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTaskManager
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string СreationDate { get; set; }
        public string Deadline { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public List<User> Responsible {  get; set; }
        public Task(string name, string description, string deadline) 
        {
            this.Name = name;
            this.Description = description;
            this.Deadline = deadline;
        }

    }
}
