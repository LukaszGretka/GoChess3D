using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal abstract class QueenMovement: PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.Derpendicularly | MovementType.Diagonaly;

        public bool AbleToMoveBackward => true;

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
