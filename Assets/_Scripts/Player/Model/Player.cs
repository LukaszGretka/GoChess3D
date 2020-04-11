using Assets._Scripts.Pieces.Enums;
using Mirror;

public class Player : NetworkBehaviour
{
    public string Name { get; protected set; }

    public PieceColor PieceColor { get; protected set; }
}
