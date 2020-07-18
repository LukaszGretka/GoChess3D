using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using System.Collections.Generic;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal class RookMovement : PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.Derpendicularly;

        public bool AbleToMoveBackward => true;

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            return SquaresMovementHelper.GetMovement(MovementType, currentSquare);
        }
    }
}
