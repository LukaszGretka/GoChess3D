using Assets._Scripts.Board.Control;
using Assets._Scripts.Board.Models;
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

        public void HandlePieceSelection(GameObject pieceGameObject)
        {
            IsSelected = true;
            pieceGameObject.GetComponent<Renderer>().material.color = Color.red;
            _onBoardMovementLogic.SetBacklightColorToSquare(_onBoardMovementLogic.GetPossiblePieceMovement(pieceGameObject));
        }

        public void HandlePieceDeselection(GameObject pieceGameObject)
        {
            IsSelected = false;
            PieceHelper.SetDefaultPieceMaterial(pieceGameObject);
            _onBoardMovementLogic.RemoveBacklightFromSquares();
        }

        public Coords CurrentPosition { get; set; }

        public bool IsSelected { get; set; }
    }
}
