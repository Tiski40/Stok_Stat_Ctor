using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStokStat
{
    public class NormsIssuance
    {
        
        public void NormInput()
        {
            var positionNew = new PositionNew("Test", 1);
            bool isWorkNorms = true;
            while (isWorkNorms)
            {
                Console.WriteLine("\n0 - Внести новую позицию \n1 - Редактировать позицию\n2 - Выход");
                int? inputCommandNorms = 0;
                try
                {
                    string? inputNormsCommandStr = Console.ReadLine();
                    if (inputNormsCommandStr != null)
                        inputCommandNorms = int.Parse(inputNormsCommandStr);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Нет такой команды");
                }
                switch (inputCommandNorms)
                {
                    case 0:
                        positionNew.PosInputNewDate();
                        break;
                    case 1:
                        break; 
                    case 2:
                        isWorkNorms = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
