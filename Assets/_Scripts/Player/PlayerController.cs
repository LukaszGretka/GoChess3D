using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Control;
using UnityEngine;

public class PlayerController : Player
{
    private IPieceMovement _lastSelectedPieceMovementComponent;
    private GameObject _lastSelectedPiece;
    private GameObject _currentSelectedPiece;

    private OnBoardMovementLogic _onBoardMovementLogic;

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

    private void Update()
    {
        if (isLocalPlayer && Input.GetMouseButtonDown(0))
        {
            _currentSelectedPiece = SelectPiece();

            if (_currentSelectedPiece != null)
            {
                _onBoardMovementLogic.ShowPossibleMovement(_currentSelectedPiece);
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

    private GameObject SelectPiece()
    {
        Ray rayFromCam = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(rayFromCam, out RaycastHit rayHit))
        {
            return null;
        }
        var hitPiece = rayHit.collider.gameObject;
        var hitPieceIPieceComponent = hitPiece.GetComponent<IPiece>();

        if (hitPieceIPieceComponent is null)
        {
            Debug.LogWarning($"No IPiece attached to hit object. Object name: {hitPiece.name}");
            return null;
        }

        if (PieceColor != hitPiece.GetComponent<IPiece>().PieceColor)
        {
            return null;
        }

        var hitPieceIPieceMovementComponent = hitPiece.GetComponent<IPieceMovement>();

        if (hitPieceIPieceMovementComponent is null)
        {
            Debug.LogError("No IPieceMovement attached to hit object");
            return null;
        }

        if (_lastSelectedPieceMovementComponent != null && hitPieceIPieceMovementComponent.IsSelected == _lastSelectedPieceMovementComponent.IsSelected)
        {
            Debug.Log("Selected the same Piece");
            return null;
        }

        if (_lastSelectedPiece is null == false)
            _lastSelectedPieceMovementComponent.HandlePieceDeselection(_lastSelectedPiece);

        hitPieceIPieceMovementComponent.HandlePieceSelection(hitPiece);
        _lastSelectedPieceMovementComponent = hitPieceIPieceMovementComponent;
        _lastSelectedPiece = hitPiece;

        return hitPiece;
    }

    private void MakeMove()
    {
        // 1. Get piece which is selected.
        // 2. Calculate possibilities of movement.
        // 3. Perform Piece movement.
        // 4. Deselect moved piece.
        // 5. Auto end of player turn.
    }
}
