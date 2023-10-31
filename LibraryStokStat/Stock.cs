using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryStokStat
{
    internal class Stock : PositionNew
    {               
        public int Size { get; private set; }
        public int Quantity { get; private set; }
        public Stock(string namePos, int size, int quality) : base(namePos) 
        {
            Quantity = quality;
            Size = size;
        }
    }
}
