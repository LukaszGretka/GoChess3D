using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Abstract
{
    interface IPieceMovement : IBasicMovement
    {
        bool IsSelected { get; set; }

        IEnumerable<Square> GetPossibleMovementSquares(Square currentSquare);

        Coords CurrentPosition { get; }

        void HandlePieceSelection(GameObject pieceGameObject);

        void HandlePieceDeselection(GameObject pieceGameObject);
    }
}
