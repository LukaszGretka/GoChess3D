using UnityEngine;

namespace Assets._Scripts.Board.Models
{
    internal class Coords
    {
        [SerializeField]
        internal char Row;

        [SerializeField]
        internal char Column;

        public Coords(char row, char column)
        {
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return string.Format("Row: {0} | Column : {1}", Row, Column);
        }
    }
}
