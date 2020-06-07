using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using System.Collections.Generic;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal class KnightMovement : PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.Knight;

        public bool AbleToMoveBackward => true;

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            return SquaresHelper.GetMovement(MovementType, currentSquare);
        }
    }
}
