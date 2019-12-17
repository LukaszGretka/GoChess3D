using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement;
using UnityEngine;

namespace Assets._Scripts.Pieces
{
    internal class King : KingMovement, IPiece
    {
        public string Name => GetType().Name;

        public PieceColor PieceColor { get; private set; }

        public bool IsSelected { get; set; } = false;
    }
}
