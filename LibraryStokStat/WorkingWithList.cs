using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryStokStat
{
    public class WorkingWithList : Person, IDateEntryValidation  //Работа со списком
    {       
        private static List<Person>? listPerson = new();
        private static readonly string fileName = "PersonJson.json";
        static WorkingWithList()   // Стат.констр. Отрабатывает при старте программы.
                                   // Загружает из Json файла данные в List для работы с ними.
        {
            if (File.Exists(fileName) != true)
            File.WriteAllText(fileName, null);
            string personJson = File.ReadAllText(fileName);
            List<Person>? listJsonOut = JsonConvert.DeserializeObject<List<Person>>(personJson);
            if (listJsonOut != null)
            listPerson.AddRange(listJsonOut);
            else
            listPerson = new List<Person>();
            //IssuanceCheck();
        }      
        private static void IssuanceCheck()  // Проверка на выдачу. Запуск из статического конструктора при старте программы
        {
            //Проверка на получение СИЗ 1
            DateTime dateParse;
            DateTime dateToday = DateTime.Today;

            if (!(listPerson == null || listPerson.Count == 0))
            {
                foreach (Person user in listPerson.OrderBy(user => string.Concat(user.Id, user.Name, user.SurName, user.EmploymentDate)))
                {
                    dateParse = DateTime.Parse(user.EmploymentDate ?? "Нет данных о дате приема");
                    var result = (dateToday - dateParse).Days;
                    Console.WriteLine($"{result}");
                }
            }
            else
            {
                Console.WriteLine("Нет ни одного сотрудника для выдачи");
                return;
            }

        }
        public void MonitorOutput() => base.ListOutput(listPerson); //Метод вывода всех сотрудников        
        
        public async void DataInput()//Метод заполнения карточки сотрудника
        {     
            var workingWithList = new WorkingWithList();
            Console.WriteLine("Введите имя:");
            string? name = null;            
            if (workingWithList is IDateEntryValidation dateEntryValidationName) //Проверка введенных строковых данных.Только алфавит RU и EN
                name = dateEntryValidationName.ValidatOfEnteredDataString();
            Console.WriteLine("Введите фамилию:");
            string? surname = null;
            if (workingWithList is IDateEntryValidation dateEntryValidationSurName) //Проверка введенных строковых данных.Только алфавит RU и EN 
                surname = dateEntryValidationSurName.ValidatOfEnteredDataString();
            Console.WriteLine("Дата приема на работу:");
            string? employmentDate = Console.ReadLine();
            if (DateTime.TryParse(employmentDate, out DateTime dateValue) != true) // Конвертация даты в формат dd.MM.yyyy            
                Console.WriteLine("Неверный формат даты");    
            Person user = new(name, surname, 0, dateValue.ToShortDateString());   
            int lastId = 0;
            if (listPerson != null)            
                lastId = listPerson.Count == 0 ? 0 : listPerson.Last().Id; //Получаем последний Id           
            else           
                listPerson = new List<Person>();                           
            user.SetNewId(lastId + 1); //Присваиваем следующий ID
            listPerson.Add(user);
            await new SaveDateInJsonCsv().SaveInJsonCsv(listPerson); //Асинхронный метод сохранение данных в файл Json
        }  
        public void PersonRemove()
        {
            Console.WriteLine("Введите Таб.номер");
            string? idStr = Console.ReadLine();
            int delId = GetIntFromString(idStr);
            if (delId <= 0) Console.WriteLine("Нет такого номера");
            else
            {    
                if (listPerson != null)
                {
                    Person? personDel = listPerson.FirstOrDefault(u => u.Id == delId);
                    if (personDel != null)                   
                        listPerson.Remove(personDel);                    
                }
                else               
                    listPerson = new List<Person>();                                                
            }       
        }
        public int GetIntFromString(string? str)
        {
            int input = 0;
            try
            {
                if (str != null)
                {
                    input = int.Parse(str);
                }               
            }
            catch (FormatException)
            {               
            }
            return input;
        }
    }
}
