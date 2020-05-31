using Assets._Scripts.Pieces.Enums;
using UnityEngine;

namespace Assets._Scripts.Abstract
{
    internal abstract class Piece : MonoBehaviour
    {
        protected abstract string Name { get; }

        [SerializeField]
        internal PieceColor PieceColor;
    }
}
