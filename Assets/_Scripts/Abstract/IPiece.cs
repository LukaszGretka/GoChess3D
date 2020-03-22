using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Abstract
{
    public interface IPiece
    {
        string Name { get; }

        PieceColor PieceColor { get; set; }
    }
}
