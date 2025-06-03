using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using Microsoft.VisualBasic;
using static System.Reflection.Metadata.BlobBuilder;

namespace PhonebookLibrary
{
    public class Phonebook
    {
        public static string path = "phonebook.txt";
        public static List<Abonent> abonents = new List<Abonent>();
        public static void ReadBook()
        {

            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; i = i + 3)
                {
                    Abonent abonent = new Abonent(lines[i], lines[i + 1]);
                    abonent.Description = lines[i + 2];
                    abonents.Add(abonent);
                }
            }
        }
        public static bool Add(Abonent abonent)
        {
            Abonent found = abonents.Find(item => item.Phone == abonent.Phone);
            Abonent found1 = abonents.Find(item => item.Name == abonent.Name);
            if (found != null)
            {
                Console.WriteLine("абонент c таким номером телефона уже существует");
                return false;
            }
            else if (found1 != null)
            {
                Console.WriteLine("абонент c таким именем уже существует");
                return false;
            }
            else
            {
                abonents.Add(abonent);
                return true;
            }
            
        }
        public static bool Delete(string name)
        {
            var obj = abonents.FirstOrDefault(item => item.Name == name);
            if (obj != null)
            {
                int index = abonents.IndexOf(obj);
                abonents.RemoveAt(index);
                return true;
            }
            return false;
            
        }
        public static void Search(string phone=null, string name=null)
        {            
            Abonent found =(phone!=null) ? (abonents.Find(item => item.Phone == phone)): (abonents.Find(item => item.Name == name));           
            if (found!=null)
            {
                Console.WriteLine("Имя:{0}, Телефон:{1}, Дополнительная информация:{2}", found.Name, found.Phone, (found.Description!="")? found.Description:"отсутствует");
            }
            else
            {
                Console.WriteLine("Такой абонент не найден");
            }
        }
        public static bool WriteDown()
        {
            using StreamWriter sw = File.CreateText(path);
            foreach (var item in abonents)
            {
                sw.WriteLine(item.Name);
                sw.WriteLine(item.Phone);
                sw.WriteLine(item.Description);
            }        
            return true;
        }
    }
}
