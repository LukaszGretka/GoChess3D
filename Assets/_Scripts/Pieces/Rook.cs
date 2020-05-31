using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement;
using Assets._Scripts.Pieces.Enums;
using UnityEngine;

namespace Assets._Scripts.Pieces
{
    internal class Rook : Piece
    {
        protected override string Name => GetType().Name;
    }
}
