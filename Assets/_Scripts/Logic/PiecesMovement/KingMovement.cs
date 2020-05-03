using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal abstract class KingMovement : PieceMovementBase, IPieceMovement
    {
        public MovementType MovementType => MovementType.DiagonalAndDerpendicular;

        public bool AbleToMoveBackward => true;

        public bool CheckIfMovePossible()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            return SquareHelpers.GetLocatedSquares(MovementType, currentSquare)
                .Where(square => square.transform.position.x == currentSquare.transform.position.x + 1f
                                || square.transform.position.x == currentSquare.transform.position.x -1f
                                || square.transform.position.z == currentSquare.transform.position.z +1 
                                || square.transform.position.z == currentSquare.transform.position.z -1f);
        }
    }
}
