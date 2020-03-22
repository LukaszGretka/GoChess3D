using Assets._Scripts.Abstract;
using UnityEngine;

namespace Assets._Scripts.Logic.PiecesMovement.Abstract
{
    abstract class PieceMovementBase : MonoBehaviour
    {
        public bool IsSelected { get; set; }

        public void HandlePieceSelection(GameObject pieceGameObject)
        {
            IsSelected = true;
            var piece = pieceGameObject.GetComponent<IPiece>();

            Debug.Log($"Selected piece: {piece.Name}, {piece.PieceColor}");
        }

        public void HandlePieceDeselection(GameObject pieceGameObject)
        {
            IsSelected = false;
            var piece = pieceGameObject.GetComponent<IPiece>();
            Debug.Log($"Deselected piece: {piece.Name}, {piece.PieceColor}");
        }
    }
}
