using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTaskManager
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; }
        public string Surname { get;}
        public string Description { get; set; }
        public string Statistics  {  get; set; }
        public string Email {  get; set; }
        public string JobTitle {  get; set; }
        public string Role {  get; set; }
        public User(string name,string surname,string email)
        {
            
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
        }
        
    }
}
