using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces.Enums;
using System;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal abstract class KnightMovement : PieceMovementBase, IPieceMovement
    {
        public Coords CurrentPosition { get; private set; } // should be assign in every MakeMove method

        public MovementType MovementType => MovementType.Knight;

        public bool AbleToMoveBackward => true;

        public bool CheckIfMovePossible()
        {
            throw new NotImplementedException();
        }

        public void HandlePieceSelection()
        {
            throw new NotImplementedException();
        }

        public void MakeMove() //proably need coords in parameter - will see
        {
            throw new NotImplementedException();
        }

        public void ResetToStartingPosition(PieceColor pieceColor)
        {
            throw new NotImplementedException();
        }
    }
}
