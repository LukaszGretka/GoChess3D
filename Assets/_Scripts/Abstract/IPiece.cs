using Assets._Scripts.Pieces;

namespace Assets._Scripts.Abstract
{
    internal interface IPiece
    {
        string Name { get; }

        PieceColor PieceColor { get; }
        
        bool IsSelected { get; set; }
    }
}
