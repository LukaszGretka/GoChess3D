using Assets._Scripts.Network;
using Assets._Scripts.Network.Helpers;
using Mirror;
using UnityEngine;

public class NetworkManagerGoChess3D : NetworkManager
{
    public Transform WhitePlayerSpawnPoint;
    public Transform BlackPlayerSpawnPoint;

    public delegate void PlayersLoadedHandler();
    public static event PlayersLoadedHandler OnPlayersConnected;

    private OwnershipManager _ownershipManager;
    private const int MaximumAmountOfPlayers = 2;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        _ownershipManager = GameObject.Find("ServerManager").GetComponent<OwnershipManager>();
        Transform playerSpawnPoint = numPlayers == 0 ? WhitePlayerSpawnPoint : BlackPlayerSpawnPoint;
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        _ownershipManager.SetPieceAuthority(conn, numPlayers == 1 ? Assets._Scripts.Pieces.Enums.PieceColor.White : Assets._Scripts.Pieces.Enums.PieceColor.Black);

        if (numPlayers == MaximumAmountOfPlayers)
        {
            //TODO start game
        }
        else
        {
            NetworkPlayersHelper.UpdateGameStatusForConnectedPlayers($"Waiting for {MaximumAmountOfPlayers - numPlayers} more player to join.");
        }
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        StopClient();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        StopServer();
    }
}
