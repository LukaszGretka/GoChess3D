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
    }
}
