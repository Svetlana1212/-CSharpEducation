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
        public static void ReadBook(List<Abonent> collection)
        {

            string[] lines = File.ReadAllLines(path);
            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; i = i + 3)
                {
                    Abonent abonent = new Abonent(lines[i], lines[i + 1]);
                    abonent.Description = lines[i + 2];
                    collection.Add(abonent);
                }
            }
        }
        public static bool Add(Abonent abonent, List<Abonent> collection)
        {
            Abonent found = collection.Find(item => item.Phone == abonent.Phone);
            Abonent found1 = collection.Find(item => item.Name == abonent.Name);
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
                collection.Add(abonent);
                return true;
            }
            
        }
        public static bool Delete(string name, List<Abonent> collection)
        {
            var obj = collection.FirstOrDefault(item => item.Name == name);
            if (obj != null)
            {
                int index = collection.IndexOf(obj);
                collection.RemoveAt(index);
                return true;
            }
            return false;
            
        }
        public static void Search(List<Abonent> collection, string phone=null, string name=null)
        {            
            Abonent found =(phone!=null) ? (collection.Find(item => item.Phone == phone)): (collection.Find(item => item.Name == name));           
            if (found!=null)
            {
                Console.WriteLine("Имя:{0}, Телефон:{1}, Дополнительная информация:{2}", found.Name, found.Phone, (found.Description!="")? found.Description:"отсутствует");
            }
            else
            {
                Console.WriteLine("Такой абонент не найден");
            }
        }
        public static bool WriteDown(List<Abonent> data)
        {
            using StreamWriter sw = File.CreateText(path);
            foreach (var item in data)
            {
                sw.WriteLine(item.Name);
                sw.WriteLine(item.Phone);
                sw.WriteLine(item.Description);
            }        
            return true;
        }
    }
}
