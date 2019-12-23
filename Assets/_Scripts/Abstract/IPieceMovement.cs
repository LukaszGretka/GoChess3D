using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Abstract
{
    interface IPieceMovement : IBasicMovement
    {
        bool IsSelected { get; set; }

        void ResetToStartingPosition(PieceColor pieceColor);

        void MakeMove();

        Coords CurrentPosition { get; }
    }
}
