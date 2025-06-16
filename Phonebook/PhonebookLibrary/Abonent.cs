using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace PhonebookLibrary
{
    public class Abonent
    {
        public string Name { get; }
        public string Phone { get; set; }
        public string Description {  get; set; }
        public Abonent(string name, string phone)
        {
            this.Name = name;
            this.Phone = phone;
        }
    }
}
