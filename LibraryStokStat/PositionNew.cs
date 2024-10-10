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
    internal class PositionNew : IDateEntryValidation
    {
        WorkingWithList workingList = new();
        
        public static List<PositionNew> listPositionNew = new();
        private static readonly string fileNamePosition = "PositionJson.json";
        

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
        public PositionNew(string? namePosition) => NamePosition = namePosition;
        public PositionNew() { }
        public void PosInputNewDate()
        {
            var positionNew = new PositionNew();
            bool isWorkPositNew = true;
            while (isWorkPositNew)
            { 
                bool getNamePosition = true;
                while (getNamePosition)
                {
                    Console.Write("Наименование: ");
                    string? namePos = Console.ReadLine();
                    InputCheck(namePos,out getNamePosition);
                    if (namePos != null) NamePosition = namePos;
                }
                bool getTernValue = true;
                while (getTernValue)
                {
                    Console.Write("Срок норм выдачи(лет): ");
                    try
                    {
                        string? normTerD = Console.ReadLine();
                        if (normTerD != null) NormTermDay = int.Parse(normTerD);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Нет такой команды");  //Доработать. Не переходит
                        getTernValue = true;
                    }                   
                }                                             
                PositionNew? positionNormsNew = new(NamePosition, NormTermDay, NumberPosition);
                int lastIdPosition = 0;
                if (listPositionNew != null)
                    lastIdPosition = listPositionNew.Count == 0 ? 0 : listPositionNew.Last().NumberPosition; //Получаем последний Id           
                else
                    listPositionNew = new List<PositionNew>();
                positionNormsNew.SetNewId(lastIdPosition + 1); //Присваиваем следующий ID
                listPositionNew.Add(positionNormsNew);

                bool enterPosYN = true;
                while (enterPosYN)
                {
                    Console.WriteLine ("Внести еще позицию?\n0 - Нет\n1 - Да");
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
                        enterPosYN = false;
                    }

                    switch (command)
                    {
                        case 0:
                            isWorkPositNew = false;
                            if (positionNormsNew is IDateEntryValidation iDateEntryValid)
                            {
                                iDateEntryValid.SaveInJson(in listPositionNew, fileNamePosition);
                            }
                            enterPosYN = false;
                            break;
                            case 1:
                        default:
                                break;
                    }
                    isWorkPositNew = false;
                }
                
            }                      
        }
        public void SetNewId(int id)
        {
            NumberPosition = id;
        }
        public bool InputCheck(string? keyboardInput, out bool getRet) //Проверка на пустую строку ввода с клавиатуры
        {
            if (keyboardInput == string.Empty)
            {
                Console.WriteLine("Строка не должна быть пустой.");
                return getRet = true;
            }
            else            
                return getRet = false;
        }
    }
}
