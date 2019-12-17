using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces;
using UnityEngine;

namespace Assets._Scripts.Logic.PiecesMovement
{
    internal abstract class KingMovement : MonoBehaviour, IPieceMovement
    {
        public MovementType MovementType => MovementType.Diagonaly | MovementType.Derpendicularly;

        public bool AbleToMoveBackward => true;

        public Coords CurrentPosition { get; protected set; }

        public bool CheckIfMovePossible()
        {
            throw new System.NotImplementedException();
        }

        public void MakeMove()
        {
            throw new System.NotImplementedException();
        }

        public void ResetToStartingPosition(PieceColor pieceColor)
        {
            throw new System.NotImplementedException();
        }
    }
}
