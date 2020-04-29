using Assets._Scripts.Abstract;
using UnityEngine;

namespace Assets._Scripts.Board.Control
{
    internal class OnBoardMovementLogic : MonoBehaviour
    {
        internal void ShowPossibleMovement(GameObject selectedPiece)
        {
            var squarePosition = selectedPiece.GetComponentInParent<Transform>().position;
            var pieceMovement = selectedPiece.GetComponent<IPieceMovement>();


            if (true)
            {
                Debug.Log(nameof(squarePosition) + " " + squarePosition);
                Debug.Log(nameof(pieceMovement.IsSelected) + " " + pieceMovement.IsSelected);
                Debug.Log(nameof(pieceMovement.MovementType) + " " + pieceMovement.MovementType);
                Debug.Log(nameof(pieceMovement.CurrentPosition) + " column: " + pieceMovement.CurrentPosition.Column + " row: " + pieceMovement.CurrentPosition.Row);

            }
            // var piecePosition = pieceMovement.CurrentPosition.

            // movement possibility to selected Piece
            // highlight squares which is allowed to move 
        }


    }
}
