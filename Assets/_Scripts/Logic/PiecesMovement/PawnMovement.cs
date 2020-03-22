using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal class PawnMovement : PieceMovementBase, IPieceMovement
    {
        public Coords CurrentPosition => throw new NotImplementedException();

        public MovementType MovementType => MovementType.Derpendicularly;

        public bool AbleToMoveBackward => false;

        public bool CheckIfMovePossible()
        {
            throw new NotImplementedException();
        }

        public void MakeMove()
        {
            throw new NotImplementedException();
        }

        public void ResetToStartingPosition(PieceColor pieceColor)
        {
            throw new NotImplementedException();
        }
    }
}
