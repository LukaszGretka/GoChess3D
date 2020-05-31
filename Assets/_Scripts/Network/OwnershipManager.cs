using Assets._Scripts.Abstract;
using Assets._Scripts.Pieces.Enums;
using Mirror;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Network
{
    internal class OwnershipManager : NetworkBehaviour
    {
        private const string PieceTagName = "Piece";

        [Server]
        internal void SetPieceAuthority(NetworkConnection playerConnection, PieceColor playerPieceColor)
        {
            var pieces = GameObject.FindGameObjectsWithTag(PieceTagName)
                                   .Where(p => p.GetComponent<Piece>().PieceColor == playerPieceColor).ToList();

            if (pieces is null)
            {
                Debug.LogError(string.Format("Game objects with tag {0} and {1} color, can't be found", PieceTagName, playerPieceColor));
            }

            foreach (var piece in pieces)
            {
                piece.GetComponent<NetworkIdentity>().AssignClientAuthority(playerConnection);
            }
        }
    }
}
