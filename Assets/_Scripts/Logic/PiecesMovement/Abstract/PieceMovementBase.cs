using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Pieces.Helpers;
using Mirror;
using UnityEngine;

namespace Assets._Scripts.Logic.PiecesMovement.Abstract
{
    abstract class PieceMovementBase : MonoBehaviour
    {
        public Coords CurrentPosition { get; set; }

        public bool IsSelected { get; set; }

        public void HandlePieceSelection(GameObject pieceGameObject)
        {
            IsSelected = true;
            var piece = pieceGameObject.GetComponent<IPiece>();

            pieceGameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        public void HandlePieceDeselection(GameObject pieceGameObject)
        {
            IsSelected = false;
            PieceHelper.SetDefaultPieceMaterial(pieceGameObject);

            var piece = pieceGameObject.GetComponent<IPiece>();
        }
    }
}
