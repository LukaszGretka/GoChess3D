using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement;
using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Pieces
{
    internal class Queen : Piece
    {
        protected override string Name => GetType().Name;
    }
}
