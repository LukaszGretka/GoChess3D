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
    private IPieceMovement _lastSelectedPieceMovementComponent;
    private GameObject _lastSelectedPiece;
    private bool _anyPieceSelected;

    private OnBoardMovementLogic _onBoardMovementLogic;
    private IEnumerable<Square> _possibleMovementSquares;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        SetCameraSettings();

        _onBoardMovementLogic = GameObject.Find("BoarderManager").GetComponent<OnBoardMovementLogic>();
        var networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManagerGoChess3D>();

        if (networkManager != null)
        {
            PieceColor = transform.position.z == networkManager.WhitePlayerSpawnPoint.position.z ?
                Assets._Scripts.Pieces.Enums.PieceColor.White : Assets._Scripts.Pieces.Enums.PieceColor.Black;
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0) && !_anyPieceSelected)
        {
            _anyPieceSelected = SelectPiece();
            return;
        }

        if (_possibleMovementSquares != null && Input.GetMouseButtonDown(0) && _anyPieceSelected)
        {
            MakeMove();
            _anyPieceSelected = false;
        }
    }

    private void SetCameraSettings()
    {
        Camera.main.orthographic = false;
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localEulerAngles = new Vector3(45f, 0f, 0f);
    }

    [ClientRpc]
    internal void RpcSetPlayerColor(int color)
    {
        if (!isLocalPlayer)
            return;

        PieceColor = color == 1 ? Assets._Scripts.Pieces.Enums.PieceColor.White : Assets._Scripts.Pieces.Enums.PieceColor.Black;
    }


    private bool SelectPiece()
    {
        Ray rayFromCam = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(rayFromCam, out RaycastHit rayHit))
        {
            return false;
        }
        var hitPiece = rayHit.collider.gameObject;
        var hitPieceIPieceComponent = hitPiece.GetComponent<IPiece>();

        if (hitPieceIPieceComponent is null)
        {
            Debug.LogWarning($"No IPiece attached to hit object. Object name: {hitPiece.name}");
            return false;
        }

        if (PieceColor != hitPiece.GetComponent<IPiece>().PieceColor)
        {
            return false;
        }

        var hitPieceIPieceMovementComponent = hitPiece.GetComponent<IPieceMovement>();

        if (hitPieceIPieceMovementComponent is null)
        {
            Debug.LogError("No IPieceMovement attached to hit object");
            return false;
        }

        if (_lastSelectedPieceMovementComponent != null && hitPieceIPieceMovementComponent.IsSelected == _lastSelectedPieceMovementComponent.IsSelected)
        {
            Debug.Log("Selected the same Piece");
            return false;
        }

        if (_lastSelectedPiece is null == false)
        {
            _lastSelectedPieceMovementComponent.HandlePieceDeselection(_lastSelectedPiece);
            _onBoardMovementLogic.RemoveBacklightFromSquares();
        }

        hitPieceIPieceMovementComponent.HandlePieceSelection(hitPiece);
        _lastSelectedPieceMovementComponent = hitPieceIPieceMovementComponent;
        _lastSelectedPiece = hitPiece;

        if (hitPiece != null)
        {
            _possibleMovementSquares = _onBoardMovementLogic.ShowPossibleMovement(hitPiece);
            return true;
        }

        return false;
    }

    // TODO - just for test need refactoring after that
    private void MakeMove()
    {
        Ray rayFromCam = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(rayFromCam, out RaycastHit rayHit))
        {
            return;
        }

        var hitSquare = rayHit.collider.gameObject;
        var hitSquareComponent = hitSquare.GetComponent<Square>();

        if (hitSquareComponent is null)
        {
            return;
        }

        var _possibleMovementSquaresListed = _possibleMovementSquares.ToList();

        if (!_possibleMovementSquaresListed.Contains(hitSquareComponent))
        {
            Debug.Log($"Not able to move at selected square {hitSquareComponent.GetCoordinates().ToString()}");
            return;
        }

        AttachPieceToTargetingSquare(hitSquareComponent);
        DeattachPieceFromLeavingSquare(_lastSelectedPiece.GetComponentInParent<Square>());

        // 1. Get piece which is selected.
        // 2. Calculate possibilities of movement.
        // 3. Perform Piece movement.
        // 4. Deselect moved piece.
        // 5. Auto end of player turn.
    }

    private void DeattachPieceFromLeavingSquare(Square leavingSquare)
    {
        leavingSquare.IsOccupied = false;
        Destroy(leavingSquare.GetComponent<NetworkTransformChild>());
    }

    private void AttachPieceToTargetingSquare(Square targetingSquare)
    {
        var hitSquareTransformChild = targetingSquare.gameObject.AddComponent<NetworkTransformChild>();
        hitSquareTransformChild.target = _lastSelectedPiece.transform;

        targetingSquare.transform.parent = hitSquareTransformChild.transform;
        _lastSelectedPiece.transform.position = new Vector3(targetingSquare.transform.position.x,
                                                                _lastSelectedPiece.transform.position.y,
                                                                 targetingSquare.transform.position.z);

        targetingSquare.IsOccupied = true;
    }
}
