using System.Collections.Generic;
using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal abstract class KingMovement : PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.Diagonaly | MovementType.Derpendicularly;

        public bool AbleToMoveBackward => true;

        public bool CheckIfMovePossible()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            throw new System.NotImplementedException();
        }
    }
}
