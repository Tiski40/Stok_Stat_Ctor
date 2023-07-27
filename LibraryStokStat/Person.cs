using System.Collections.Generic;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace LibraryStokStat
{
    public class Person
    {
        [JsonProperty("Имя")]
        public string? Name { get; private set; }
        [JsonProperty("Фамилия")]
        public string? SurName { get;private set; }
        [JsonProperty("Таб.номер")]
        public int Id { get; set; }
        [JsonProperty("Дата приема на работу")]

        public string? EmploymentDate { get; private set; }
        public Person(){}
        public Person(string? name, string? surName, int id)
        {
            Name = name;
            SurName = surName;
            Id = id;
        }
        public Person(string? name, string? surName, int id, string? emplDate)
        {
            Name = name;
            SurName = surName;
            Id = id;
            EmploymentDate = emplDate;
        }       
        public void SetNewId(int id)
        {
            Id = id;
        }  
        public virtual void ListOutput(in List<Person>? listPerson)
        {
            if (!(listPerson == null || listPerson.Count == 0))
            {
                listPerson.Sort((u1, u2) => u1.Id.CompareTo(u2.Id)); //Сортировка по Id
                foreach (Person user in listPerson.OrderBy(user => string.Concat(user.Id, user.Name, user.SurName, user.EmploymentDate)))
                    Console.WriteLine($"Номер:{user.Id,-5}\tИмя:{user.Name,-8}\tФамилия:{user.SurName,-8}\tДата приема на работу: {user.EmploymentDate ?? "Нет данных"}");
            } 
            else
            {
                Console.WriteLine("Нет ни одного сотрудника в списке");
                return;
            }
        }
    }
}