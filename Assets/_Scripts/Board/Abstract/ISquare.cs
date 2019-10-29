using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Board.Abstract
{
    public interface ISquare
    {
        char Rank { get; } // row

        char File { get; } // column

        bool IsOccupied { get; }
    }
}
