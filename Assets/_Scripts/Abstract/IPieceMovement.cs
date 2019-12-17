using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces;

namespace Assets._Scripts.Abstract
{
    interface IPieceMovement : IBasicMovement
    {
        void ResetToStartingPosition(PieceColor pieceColor);

        void MakeMove();

        Coords CurrentPosition { get; }
    }
}
