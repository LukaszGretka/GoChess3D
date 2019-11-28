using Assets._Scripts.Abstract;

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
