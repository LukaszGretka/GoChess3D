using Assets._Scripts.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Abstract
{
    interface IPieceMovement : IBasicMovement
    {
        bool IsSelected { get; }

        IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare);

        void HandlePieceSelection(GameObject pieceGameObject, IEnumerable<Square> possibleMovementSquares);

        void HandlePieceDeselection(GameObject pieceGameObject);
    }
}
