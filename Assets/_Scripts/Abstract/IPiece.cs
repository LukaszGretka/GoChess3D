using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Abstract
{
    internal interface IPiece : IPieceMovement
    {
        string Name { get; }

        PieceColor PieceColor { get; set; }
    }
}
