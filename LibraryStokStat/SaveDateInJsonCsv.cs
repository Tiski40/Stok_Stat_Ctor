using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStokStat
{
    internal class SaveDateInJsonCsv
    {
        public readonly List<Person>? personsCsvList;
        private readonly Person? personCsv;
        public SaveDateInJsonCsv(){}
        public SaveDateInJsonCsv(Person personeCsv) => personCsv = personeCsv;
        public SaveDateInJsonCsv(List<Person> personList) => personsCsvList = personList;
        private static readonly string fileName = "PersonJson.json";        
        public async Task SaveInJsonCsv(List<Person> person)
        {
            await Task.Run(() => SaveInJson(in person));
            new SaveDateInJsonCsv(person).SaveToCsv();
        }
        public void SaveInJson(in List<Person> people)
        {
            var settings = new JsonSerializerSettings  // Задаем формат Json
            {
                Formatting = Formatting.Indented,
            };
            string output = JsonConvert.SerializeObject(people, settings);  // Создаем строку в формате Json      
            File.WriteAllText(fileName, output);  // Записываем файл Json на диск
        }
        public void SaveToCsv(string path = "", string filaNameCsv = "PersonCsv", string delimiter = ";") // Сохранение в формате CSV
        {
            using StreamWriter writer = new($"{path}{filaNameCsv}.csv", false, Encoding.UTF8);
            writer.WriteLine($"Таб.номер{delimiter}Фамилия{delimiter}Имя{delimiter}Дата приема на работу");
            if (personsCsvList == null)
                return;
            foreach (var personCard in personsCsvList)
            {
                writer.WriteLine($"{personCard.Id}{delimiter}{personCard.SurName}{delimiter}{personCard.Name}{delimiter}{personCard.EmploymentDate}");
            }
        }               
    }
}
