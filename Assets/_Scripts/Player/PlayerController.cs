using Assets._Scripts.Abstract;
using Assets._Scripts.Pieces.Enums;
using UnityEngine;

public class PlayerController : Player
{
    public PieceColor PlayerPieceColor;

    private IPieceMovement _lastSelectedPieceMovementComponent;
    private GameObject _lastSelectedPiece;

    private Camera _playerCamera;

    private void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();

        Name = "Default Player"; // TODO should be set by user in future
        PieceColor = PlayerPieceColor;
    }

    void Update()
    {
        SelectPiece();
    }

    private void SelectPiece()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayFromCam = _playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayFromCam, out RaycastHit rayHit))
            {
                var hitPiece = rayHit.collider.gameObject;
                var hitPieceIPieceComponent = hitPiece.GetComponent<IPiece>();

                if (hitPieceIPieceComponent is null)
                {
                    Debug.LogWarning($"No IPiece attached to hit object. Object name: {hitPiece.name}");
                    return;
                }

                if (PieceColor == hitPiece.GetComponent<IPiece>().PieceColor)
                {
                    var hitPieceIPieceMovementComponent = hitPiece.GetComponent<IPieceMovement>();

                    if (hitPieceIPieceMovementComponent is null)
                    {
                        Debug.LogError("No IPieceMovement attached to hit object");
                        return;
                    }

                    if (_lastSelectedPieceMovementComponent != null && hitPieceIPieceMovementComponent.IsSelected == _lastSelectedPieceMovementComponent.IsSelected)
                    {
                        Debug.Log("Selected the same Piece");
                        return;
                    }

                    if (_lastSelectedPiece is null == false)
                        _lastSelectedPieceMovementComponent.HandlePieceDeselection(_lastSelectedPiece);

                    hitPieceIPieceMovementComponent.HandlePieceSelection(hitPiece);
                    _lastSelectedPieceMovementComponent = hitPieceIPieceMovementComponent;
                    _lastSelectedPiece = hitPiece;
                }
            }
        }
    }
}
