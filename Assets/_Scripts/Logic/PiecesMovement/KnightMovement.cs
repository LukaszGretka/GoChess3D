using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal abstract class KnightMovement : PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.Knight;

        public bool AbleToMoveBackward => true;

        public bool CheckIfMovePossible()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            throw new NotImplementedException();
        }
    }
}
