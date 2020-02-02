using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement;
using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Pieces
{
    internal class Knight : KnightMovement, IPiece
    {
        public string Name => GetType().Name;

        public PieceColor PieceColor { get; set; }
    }
}
