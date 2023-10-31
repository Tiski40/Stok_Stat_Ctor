using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStokStat
{
    public class SaveDateInJsonCsv:IDateEntryValidation
    {
        public readonly List<Person>? personsCsvList;
        public readonly Person? personCsv;
        public SaveDateInJsonCsv(){}
        public SaveDateInJsonCsv(Person personeCsv) => personCsv = personeCsv;
        public SaveDateInJsonCsv(List<Person> personList) => personsCsvList = personList;
        private static readonly string fileName = "PersonJson.json";

        public event WorkingWithList.DisplMessage? OutMessage;

        public async Task SaveInJsonCsv(List<Person>? person)
        {
            var saveDateInJsonCsv = new SaveDateInJsonCsv();
            if (saveDateInJsonCsv is IDateEntryValidation dateEntryValidation)
            {
                await Task.Run(() => dateEntryValidation.SaveInJson(person, fileName));
            }                
            new SaveDateInJsonCsv(person).SaveToCsv();
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
