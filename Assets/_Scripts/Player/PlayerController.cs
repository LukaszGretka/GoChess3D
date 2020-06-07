using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Control;
using Assets._Scripts.Helpers;
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NetworkTransform))]
public class PlayerController : Player
{
    private GameObject _selectedPiece;
    private OnBoardMovementLogic _onBoardMovementLogic;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        SetCameraSettings();

        _onBoardMovementLogic = GameObject.Find("Board").GetComponent<OnBoardMovementLogic>();
        var networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerGoChess3D>();

        if (networkManager != null)
        {
            PieceColor = transform.position.z == networkManager.WhitePlayerSpawnPoint.position.z ?
                Assets._Scripts.Pieces.Enums.PieceColor.White : Assets._Scripts.Pieces.Enums.PieceColor.Black;
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0) && !CheckIfClickedOnSquare())
        {
            var newSelectedPiece = SelectPiece();

            if (newSelectedPiece != null)
            {
                if (_selectedPiece != null)
                {
                    _selectedPiece.GetComponent<IPieceMovement>().HandlePieceDeselection(_selectedPiece);
                    _onBoardMovementLogic.RemoveBacklightFromSquares();
                }

                newSelectedPiece.GetComponent<IPieceMovement>().HandlePieceSelection(newSelectedPiece);

                _selectedPiece = newSelectedPiece;
                return;
            }
        }

        if (Input.GetMouseButtonDown(0) && _selectedPiece != null && CheckIfClickedOnSquare())
        {
            var pieceMoved = PerformPieceMovement(GameObjectHelper.GetComponentFromRayCast<Square>(), _onBoardMovementLogic.GetPossiblePieceMovement(_selectedPiece));

            if (pieceMoved)
            {
                _selectedPiece.GetComponent<IPieceMovement>().HandlePieceDeselection(_selectedPiece);
                _selectedPiece = null;
            }
        }
    }

    private void SetCameraSettings()
    {
        Camera.main.orthographic = false;
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localEulerAngles = new Vector3(45f, 0f, 0f);
    }

    private bool CheckIfClickedOnSquare()
    {
        return GameObjectHelper.GetComponentFromRayCast<Square>() != null;
    }

    private GameObject SelectPiece()
    {
        var hitPiece = GameObjectHelper.GetGameObjectFromRayCast();
        var hitPieceComponent = hitPiece.GetComponent<Piece>();

        if (hitPieceComponent is null)
        {
            Debug.LogError($"No {nameof(Piece)} attached to hit object. Object name: {hitPiece.name}");
            return null;
        }

        if (PieceColor != hitPieceComponent.PieceColor)
        {
            return null;
        }

        var hitPieceIPieceMovementComponent = hitPiece.GetComponent<IPieceMovement>();

        if (hitPieceIPieceMovementComponent is null)
        {
            Debug.LogError($"No {nameof(IPieceMovement)} attached to hit object");
            return null;
        }

        return hitPiece;
    }

    private bool PerformPieceMovement(Square selectedSquare, IEnumerable<Square> possibleMovementSquares)
    {
        if (possibleMovementSquares.ToList().Contains(selectedSquare))
        {
            CmdDeattachPieceFromLeavingSquare(_selectedPiece.GetComponentInParent<Square>().gameObject);
            CmdAttachPieceToTargetingSquare(selectedSquare.gameObject, _selectedPiece);
            return true;
        }

        return false;
    }

    [Command]
    private void CmdDeattachPieceFromLeavingSquare(GameObject leavingSquare)
    {
        leavingSquare.GetComponent<Square>().IsOccupied = false;
    }

    [Command]
    private void CmdAttachPieceToTargetingSquare(GameObject targetingSquare, GameObject lastSelectedPiece)
    {
        lastSelectedPiece.transform.parent = targetingSquare.transform;
        lastSelectedPiece.transform.position = new Vector3(targetingSquare.transform.position.x,
                                                                lastSelectedPiece.transform.position.y,
                                                                 targetingSquare.transform.position.z);

        targetingSquare.GetComponent<Square>().IsOccupied = true;
    }
}
