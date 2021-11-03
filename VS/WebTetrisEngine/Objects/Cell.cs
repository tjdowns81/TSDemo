using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTetrisEngine.Objects
{
    public class Cell
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int[,] Cells { get; set; }
    }
}
