using Assets._Scripts.Abstract;
using Assets._Scripts.Logic.PiecesMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Pieces
{
    class King : IPiece
    {
        private IPieceMovement kingMovement;

        public King(IPieceMovement pieceMovement)
        {
            kingMovement = pieceMovement;
        }

        public string Name => GetType().Name;
    }
}
