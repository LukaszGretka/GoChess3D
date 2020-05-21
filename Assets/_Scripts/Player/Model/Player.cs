using Assets._Scripts.Pieces.Enums;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerGUI))]
public class Player : NetworkBehaviour
{
    public string Name { get; set; }

    public static PieceColor PieceColor { get; set; }
}
