using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace LibraryStokStat
{
    internal class PositionNew:IDateEntryValidation
    {         
        public static List<PositionNew> listPositionNew = new();
        private static readonly string fileNamePosition = "PositionJson.json";

        public event WorkingWithList.DisplMessage? OutMessage;

        [JsonProperty("Наименование")]
        public string? NamePosition { get; private set; }  // Наименование позиции
        [JsonProperty("Срок норм выдачи")]
        public int NormTermDay { get; private  set; } // Срок норм в днях(количество дней)
        [JsonProperty("Номер позиции")]
        public int NumberPosition { get; private set; } // Номер позиции
        [JsonProperty("Размер")]
        public int NormSise  { get; set; }

        public PositionNew(string? namePosition, int normTermDay, int numberPositon)
        {
            NamePosition = namePosition;
            NormTermDay = normTermDay;
            NumberPosition = numberPositon;
        }
        public PositionNew(string namePosition)
        {
            NamePosition = NamePosition;
        }
        public PositionNew(){}
        public void PosInputNewDate()
        {
            bool isWorkPositNew = true;
            while (isWorkPositNew)
            {               
                Console.Write("Наименование: ");
                NamePosition = Console.ReadLine();
                Console.Write("Срок норм выдачи(лет): ");
                string? normTerD = Console.ReadLine();
                if (normTerD != null) NormTermDay = int.Parse(normTerD);                              
                PositionNew? positionNormsNew = new(NamePosition, NormTermDay, NumberPosition);
                int lastIdPosition = 0;
                if (listPositionNew != null)
                    lastIdPosition = listPositionNew.Count == 0 ? 0 : listPositionNew.Last().NumberPosition; //Получаем последний Id           
                else
                    listPositionNew = new List<PositionNew>();
                positionNormsNew.SetNewId(lastIdPosition + 1); //Присваиваем следующий ID
                listPositionNew.Add(positionNormsNew);
                Console.WriteLine("Внести еще позицию?\n0 - Нет\n1 - Да");
                int? command = 0;
                try
                {
                    string? commandSrt = Console.ReadLine();                   
                    if (commandSrt != null)
                        command = int.Parse(commandSrt);
                }
                catch (Exception)
                {
                    Console.WriteLine("Нет такой команды");                   
                }
                switch (command)
                {
                    case 0:
                        isWorkPositNew = false;
                        if (positionNormsNew is IDateEntryValidation iDateEntryValid)
                        {
                            iDateEntryValid.SaveInJson(in listPositionNew, fileNamePosition);
                        }
                        break;
                    default:
                        isWorkPositNew = true; break;
                }
            }                      
        }
        public void SetNewId(int id)
        {
            NumberPosition = id;
        }
    }
}
