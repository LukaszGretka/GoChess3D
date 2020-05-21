using Assets._Scripts.Network.Helpers;
using Assets._Scripts.Pieces.Logic;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerGoChess3D : NetworkManager
{
    public Transform WhitePlayerSpawnPoint;
    public Transform BlackPlayerSpawnPoint;

    public delegate void PlayersLoadedHandler();
    public static event PlayersLoadedHandler OnPlayersConnected;

    private static List<Player> _connectedPlayers = new List<Player>(); 

    private const int MaximumAmountOfPlayers = 2;

    public override void Awake()
    {
        _connectedPlayers = new List<Player>();
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform playerSpawnPoint = numPlayers == 0 ? WhitePlayerSpawnPoint : BlackPlayerSpawnPoint;
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        player.GetComponent<PlayerController>().RpcSetPlayerColor(numPlayers);
        _connectedPlayers.Add(player.GetComponent<Player>());

        if (numPlayers == MaximumAmountOfPlayers)
        {
            foreach (var connectedPlayer in _connectedPlayers)
            {
                connectedPlayer.gameObject.GetComponent<PlayerPieceSpawner>().RpcSpawnPieces();
            }
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
