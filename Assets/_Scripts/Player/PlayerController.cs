using Assets._Scripts.Abstract;
using Assets._Scripts.Pieces;
using Assets._Scripts.Pieces.Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool ActiveRound { get; set; }
    
    public string PlayerName = "Default Player";
    public PieceColor PlayerPieceColor;

    private Camera _playerCamera;
    private Player _player;

    private void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _player = GetComponent<Player>();

        _player.Name = PlayerName; // TODO should be set by user in future
        _player.PieceColor = PlayerPieceColor;
    }   

    void Update()
    {
        SelectPiece();
    }

    private void SelectPiece()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray rayFromCam = _playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(rayFromCam, out rayHit))
            {
                if (_player.PieceColor == rayHit.collider.gameObject.GetComponent<IPiece>().PieceColor)
                {
                    rayHit.collider.gameObject.GetComponent<IPiece>().IsSelected = true;
                }
            }
        }
    }
}
