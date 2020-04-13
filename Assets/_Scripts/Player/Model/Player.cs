using Assets._Scripts.Pieces.Enums;
using Mirror;

public class Player : NetworkBehaviour
{
    public string Name { get; set; }

    public PieceColor PieceColor { get; set; }
}
