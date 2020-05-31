using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement;
using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Pieces
{
    internal class Pawn : Piece
    {
        protected override string Name => GetType().Name;
    }
}
