using Assets._Scripts.Abstract;
using System.Linq;
using UnityEngine;

internal class SquaresAttackHelper : MonoBehaviour
{
    internal static bool CheckIfEncouteredHostilePiece(Piece movedPiece)
    {
        var piecesAtSquare = movedPiece.GetComponentInParent<Square>().GetComponentsInChildren<Piece>();

        return piecesAtSquare.Length > 1 && piecesAtSquare.Any(x => x.PieceColor != movedPiece.PieceColor);
    }

    // TODO: Add immune for the king
    internal static void RemoveHostilePiece(Piece movedPiece)
    {
        var piecesAtSquare = movedPiece.GetComponentInParent<Square>().GetComponentsInChildren<Piece>();
        var hostilePiece = piecesAtSquare.Where(x => x.PieceColor != movedPiece.PieceColor).FirstOrDefault();

        if(hostilePiece is null)
            Debug.LogError($"Not found hostile piece while trying to perform remove action. Occured at position: {movedPiece.GetComponent<Square>().GetCoordinates()}");

        Destroy(hostilePiece.gameObject);
    }
}
