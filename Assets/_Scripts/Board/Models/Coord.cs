namespace Assets._Scripts.Board.Models
{
    internal class Coord
    {
        internal char Row { get; private set; }

        internal char Column { get; private set; }

        public Coord(char row, char column)
        {
            Row = row;
            Column = column;
        }
    }
}
