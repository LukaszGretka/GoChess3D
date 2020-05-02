using Assets._Scripts.Board.Models;
using Assets._Scripts.Pieces.Helpers;
using UnityEngine;

namespace Assets._Scripts.Logic.PiecesMovement.Abstract
{
    abstract class PieceMovementBase : MonoBehaviour
    {
        public void HandlePieceSelection(GameObject pieceGameObject)
        {
            IsSelected = true;
            pieceGameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        public void HandlePieceDeselection(GameObject pieceGameObject)
        {
            IsSelected = false;
            PieceHelper.SetDefaultPieceMaterial(pieceGameObject);
        }

        public Coords CurrentPosition { get; set; }

        public bool IsSelected { get; set; }
    }
}
