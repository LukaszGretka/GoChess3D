using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal class QueenMovement: PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.DiagonalAndDerpendicular;

        public bool AbleToMoveBackward => true;

        public bool CheckIfMovePossible()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            return SquaresHelper.GetMovement(MovementType, currentSquare);
        }
    }
}
