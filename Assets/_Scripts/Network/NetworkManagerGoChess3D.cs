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

    private static List<(NetworkConnection, Player)> _connectedPlayers = new List<(NetworkConnection, Player)>(); 


    private const int MaximumAmountOfPlayers = 2;

    public override void Awake()
    {
        _connectedPlayers = new List<(NetworkConnection, Player)>();
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform playerSpawnPoint = numPlayers == 0 ? WhitePlayerSpawnPoint : BlackPlayerSpawnPoint;
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

        NetworkServer.AddPlayerForConnection(conn, player);
        _connectedPlayers.Add((conn, player.GetComponent<Player>()));

        if (numPlayers == MaximumAmountOfPlayers)
        {
            var spawner = GameObject.FindGameObjectWithTag("GameFlowEngine").GetComponent<ServerPieceSpawner>();

            foreach (var connectedPlayers in _connectedPlayers)
            {
                spawner.SpawnPiecesAtDefaultPositions(connectedPlayers.Item1, connectedPlayers.Item2.PieceColor);
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
