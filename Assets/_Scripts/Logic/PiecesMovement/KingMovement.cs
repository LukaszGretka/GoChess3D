using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal class KingMovement : PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.DiagonalAndDerpendicular;

        public bool AbleToMoveBackward => true;

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            return SquaresHelper.GetMovement(MovementType, currentSquare)
                .Where(square => square.transform.position.x == currentSquare.transform.position.x + 1f
                                || square.transform.position.x == currentSquare.transform.position.x -1f
                                || square.transform.position.z == currentSquare.transform.position.z +1 
                                || square.transform.position.z == currentSquare.transform.position.z -1f);
        }
    }
}
