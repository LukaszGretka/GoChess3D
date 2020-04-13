using Assets._Scripts.Pieces.Logic;
using Mirror;
using UnityEngine;

public class NetworkManagerGoChess3D : NetworkManager
{
    public Transform WhitePlayerSpawnPoint;
    public Transform BlackPlayerSpawnPoint;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform playerSpawnPoint = numPlayers == 0 ? WhitePlayerSpawnPoint : BlackPlayerSpawnPoint;
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 2)
        {
            var pieceSpawner = GameObject.Find("BoardSpawnerPoint").GetComponent<PieceSpawnerManager>();
            if (pieceSpawner != null)
                pieceSpawner.RpcSpawnPiecesAtDefaultPositions();
        }
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, NetworkIdentity player)
    {
        var pieceSpawner = GameObject.Find("BoardSpawnerPoint").GetComponent<PieceSpawnerManager>();
        if (pieceSpawner != null)
            pieceSpawner.DespawnAllPieces();

        base.OnServerRemovePlayer(conn, player);
    }
}
