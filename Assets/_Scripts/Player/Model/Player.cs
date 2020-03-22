using Assets._Scripts.Pieces.Enums;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public string Name { get; protected set; }

    public PieceColor PieceColor { get; protected set; }
}
