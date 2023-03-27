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
        private static readonly string filaName = "PersonJson.json";
        public async Task SaveInJsonCsv(List<Person> person)
        {
            await Task.Run(() => SaveInJson(in person));
        }         
             public void SaveInJson(in List<Person> people)
             {
                var settings = new JsonSerializerSettings  // Задаем формат Json
                {
                    Formatting = Formatting.Indented,
                };
                string output = JsonConvert.SerializeObject(people, settings);  // Создаем строку в формате Json      
                File.WriteAllText(filaName, output);  // Записываем файл Json на диск
             }                
    }
}
