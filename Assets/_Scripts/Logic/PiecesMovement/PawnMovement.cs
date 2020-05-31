using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare)
        {
            var pieceOnSquareColor = GetComponentInChildren<Piece>().PieceColor;
            return true == true ? GetWhitePlayerFirstTurnPossibleMovement(currentSquare, pieceOnSquareColor)
                                                                    : GetStandardPossibleMovement(currentSquare, pieceOnSquareColor);
        }

        private IEnumerable<Square> GetWhitePlayerFirstTurnPossibleMovement(Square currentSquare, PieceColor pieceOnSquareColor)
        {
            return SquaresHelper.GetMovement(MovementType, currentSquare)
                .Where(square => (pieceOnSquareColor == PieceColor.White ?
                        square.transform.position.z == currentSquare.transform.position.z + 2f 
                            || square.transform.position.z == currentSquare.transform.position.z + 1f
                        : square.transform.position.z == currentSquare.transform.position.z - 2f 
                            || square.transform.position.z == currentSquare.transform.position.z - 1f));
        }
        private IEnumerable<Square> GetStandardPossibleMovement(Square currentSquare, PieceColor pieceOnSquareColor)
        {
            return SquaresHelper.GetMovement(MovementType, currentSquare)
                .Where(square => pieceOnSquareColor == PieceColor.White ?
                        square.transform.position.z == currentSquare.transform.position.z + 1f
                        : square.transform.position.z == currentSquare.transform.position.z - 1f);
        }
    }
}
