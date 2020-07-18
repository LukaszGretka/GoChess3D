using Assets._Scripts.Board.Control;
using Assets._Scripts.Pieces.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Logic.PiecesMovement.Abstract
{
    abstract class PieceMovementBase : MonoBehaviour
    {
        private OnBoardMovementLogic _onBoardMovementLogic;

        private void Start()
        {
            _onBoardMovementLogic = GameObject.Find("Board").GetComponent<OnBoardMovementLogic>();
        }

        public void HandlePieceSelection(GameObject pieceGameObject, IEnumerable<Square> possibleMovementSquares)
        {
            IsSelected = true;
            pieceGameObject.GetComponent<Renderer>().material.color = Color.red;
            _onBoardMovementLogic.SetBacklightColorToSquare(possibleMovementSquares);
        }

        public void HandlePieceDeselection(GameObject pieceGameObject)
        {
            IsSelected = false;
            PieceHelper.SetDefaultPieceMaterial(pieceGameObject);
            _onBoardMovementLogic.RemoveBacklightFromSquares();
        }

        public bool IsSelected { get; private set; }
    }
}
