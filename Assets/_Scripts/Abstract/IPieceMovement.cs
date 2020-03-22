using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using UnityEngine;

namespace Assets._Scripts.Abstract
{
    interface IPieceMovement : IBasicMovement
    {
        bool IsSelected { get; set; }

        void ResetToStartingPosition(PieceColor pieceColor);

        void MakeMove();

        Coords CurrentPosition { get; }

        void HandlePieceSelection(GameObject pieceGameObject);

        void HandlePieceDeselection(GameObject pieceGameObject);
    }
}
