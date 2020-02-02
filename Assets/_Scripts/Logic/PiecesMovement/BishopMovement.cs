using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;
using UnityEngine;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal abstract class BishopMovement : MonoBehaviour, IPieceMovement
    {
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Coords CurrentPosition => throw new NotImplementedException();

        public MovementType MovementType => throw new NotImplementedException();

        public bool AbleToMoveBackward => throw new NotImplementedException();

        public bool CheckIfMovePossible()
        {
            throw new NotImplementedException();
        }

        public void MakeMove()
        {
            throw new NotImplementedException();
        }

        public void ResetToStartingPosition(PieceColor pieceColor)
        {
            throw new NotImplementedException();
        }
    }
}
