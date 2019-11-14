namespace Assets._Scripts.Board
{
    internal static class BoardConfiguration
    {
        internal const int HorizontalSize = 8;
        internal const int VerticalSize = 8;
        internal const float BorderThickness = 1.5f;

        internal static readonly char[] AvailableSquareSymbols = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        internal static readonly char[] AvailableSquareNumbers = { '1', '2', '3', '4', '5', '6', '7', '8' };

        internal const string BorderGameObjectName = "Border";
        internal const string SquareMarkTextGameObjectName = "TextMark";
        
    }
}
