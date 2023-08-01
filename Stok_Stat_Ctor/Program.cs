using LibraryStokStat;
namespace Stok_Stat_Ctor
{
    internal class Program
    {       
        static void Main(string[] args)
        {
            NormsIssuance normsIssuance = new NormsIssuance();
            WorkingWithList workingWithList = new();
            workingWithList.OutMessage += DisplMessOut;
            bool isWork = true;
            while (isWork)
            {              
                Console.WriteLine("\n0 - Вывести всех \n1 - Добавить нового \n2 - Удалить \n3 - Нормы \n4 - Выход \n---------------");
                int? inputCommand = 0;
                try
                {
                    string? inputCommandStr = Console.ReadLine();                    
                    if (inputCommandStr != null)
                        inputCommand = int.Parse(inputCommandStr);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Нет такой команды");
                }
                switch (inputCommand)
                {
                    case 0:
                        workingWithList.MonitorOutput();
                        break;
                    case 1:
                        workingWithList.DataInput();
                        break;
                    case 2:
                        workingWithList.PersonRemove();
                        break;
                    case 3:
                        normsIssuance.NormInput();
                        break;
                    case 4:
                        Console.WriteLine("Пока");
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Нет такой команды"); 
                        break;
                }                
            }
        }
        static void DisplMessOut(string message) => Console.WriteLine(message);
    }
}