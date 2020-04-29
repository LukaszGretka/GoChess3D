using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Abstract
{
    interface IPieceMovement : IBasicMovement
    {
        bool IsSelected { get; set; }

        IList<Coords> ReturnPossibleMovmentCoords();

        Coords CurrentPosition { get; }

        void HandlePieceSelection(GameObject pieceGameObject);

        void HandlePieceDeselection(GameObject pieceGameObject);
    }
}
