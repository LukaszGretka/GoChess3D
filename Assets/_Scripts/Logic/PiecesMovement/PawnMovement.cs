using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal class PawnMovement : PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.Derpendicularly;

        public bool AbleToMoveBackward => false;

        public bool CheckIfMovePossible()
        {
            throw new NotImplementedException();
        }

        public IList<Coords> ReturnPossibleMovmentCoords()
        {
            throw new NotImplementedException();
        }
    }
}
