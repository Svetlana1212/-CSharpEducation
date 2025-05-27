using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using PhonebookLibrary;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhonebookAbonent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List <Abonent> abonents = new List <Abonent>();            
            if (!File.Exists(Phonebook.path))
            {
                Phonebook.WriteDown(abonents);
            }            
            Phonebook.ReadBook(abonents);            
                        
            do
            {
                Console.WriteLine(@"Меню:
                1.Создать абонента
                2.Удалить абонента
                3.Найти абонента по номеру телефона
                4.Найти абонента по имени
                5.Показать список абонентов
                6.Выйти из приложения");
                int.TryParse(Console.ReadLine(), out int number);
                if (number == 1)
                {
                    Console.WriteLine("Введите имя абонента");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите номер телефона абонента");
                    string phone = Console.ReadLine();
                    Abonent abonent = new Abonent(name, phone);
                    Console.WriteLine("Введите дополнительную информацию или нажмите Enter");
                    abonent.Description = Console.ReadLine();
                    if(Phonebook.Add(abonent, abonents)&& Phonebook.WriteDown(abonents)) 
                    {
                        Console.WriteLine("Абонент записан в телефонную книгу");
                    }
                    abonents.Clear();
                    Phonebook.ReadBook(abonents);                    
                }
                else if (number == 2)
                {
                    Console.WriteLine("Введите имя абонента");
                    string name = Console.ReadLine();
                    if(Phonebook.Delete(name, abonents) && Phonebook.WriteDown(abonents))
                    {
                        Console.WriteLine("Абонент успешно удален из телефонной книги");
                    }
                    abonents.Clear();
                    Phonebook.ReadBook(abonents);
                }
                else if (number == 3)
                {   
                    Console.WriteLine("Введите телефон абонента");
                    string phone = Console.ReadLine();
                    Phonebook.Search(abonents, phone);
                    
                }
                else if (number == 4)
                {
                    Console.WriteLine("Введите имя абонента");
                    string name = Console.ReadLine();
                    Phonebook.Search(abonents, null, name);
                }
                else if (number == 5)
                {
                    for (int i = 0; i < abonents.Count; i++)
                    {
                        Console.WriteLine(abonents[i].Name);
                        Console.WriteLine(abonents[i].Phone);
                    }
                }
                else if (number == 6)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод");
                }
            } while(true);
            


        }

    }
}
