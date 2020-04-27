using Assets._Scripts.Pieces.Enums;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerGUI))]
[RequireComponent(typeof(PlayerController))]
public class Player : NetworkBehaviour
{
    public string Name { get; set; }

    public PieceColor PieceColor { get; set; }
}
