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
    }
}