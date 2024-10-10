using LibraryStokStat;
namespace Stok_Stat_Ctor
{
    public class Program
    {       
        static void Main(string[] args)
        {
            NormsIssuance normsIssuance = new NormsIssuance();
            WorkingWithList workingWithList = new();
            workingWithList.OutMessage += DisplMessOut;
            bool isWork = true;
            int? inputCommand = null;
            while (isWork)
            {  
                bool isWorkMainMenu = true;
                while (isWorkMainMenu)
                {
                    Console.WriteLine("\n0 - Вывести всех \n1 - Добавить нового \n2 - Удалить \n3 - Нормы \n4 - Выход \n---------------");
                    try
                    {
                        string? inputCommandStr = Console.ReadLine();
                        if (inputCommandStr != null)
                            inputCommand = int.Parse(inputCommandStr);
                        isWorkMainMenu = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Нет такой команды");
                        isWorkMainMenu = true;
                    }
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
                        isWork = true;
                        break;
                }                
            }
        }
        static void DisplMessOut(string message) => Console.WriteLine(message);
    }
}