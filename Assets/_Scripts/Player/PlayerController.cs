using Assets._Scripts.Abstract;
using UnityEngine;

public class PlayerController : Player
{
    private IPieceMovement _lastSelectedPieceMovementComponent;
    private GameObject _lastSelectedPiece;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        SetCameraSettings();

        var networkManager = FindObjectOfType<NetworkManagerGoChess3D>();

        if (networkManager != null)
            PieceColor = transform.position.z == networkManager.WhitePlayerSpawnPoint.position.z ?
                Assets._Scripts.Pieces.Enums.PieceColor.White : Assets._Scripts.Pieces.Enums.PieceColor.Black;

        Name = "Default Player"; // TODO should be set by user in future
    }

    private void Update()
    {
        if (isLocalPlayer)
            SelectPiece();
    }

    private void SetCameraSettings()
    {
        Camera.main.orthographic = false;
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localEulerAngles = new Vector3(45f, 0f, 0f);
    }

    private void SelectPiece()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray rayFromCam = Camera.main.ScreenPointToRay(Input.mousePosition);

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

    private void MakeMove()
    {
        // 1. Get piece which is selected.
        // 2. Calculate possibilities of movement.
        // 3. Perform Piece movement.
        // 4. Deselect moved piece.
        // 5. Auto end of player turn.
    }
}
