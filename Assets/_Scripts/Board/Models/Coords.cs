namespace Assets._Scripts.Board.Models
{
    internal class Coords
    {
        internal char Row { get; private set; }

        internal char Column { get; private set; }

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
