using Assets._Scripts.Pieces.Logic;
using Mirror;
using UnityEngine;

public class NetworkManagerGoChess3D : NetworkManager
{
    public Transform WhitePlayerSpawnPoint;
    public Transform BlackPlayerSpawnPoint;

    public delegate void PlayersLoadedHandler();
    public static event PlayersLoadedHandler OnPlayersConnected;

    private const int MaximumAmountOfPlayers = 2;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform playerSpawnPoint = numPlayers == 0 ? WhitePlayerSpawnPoint : BlackPlayerSpawnPoint;
        GameObject player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == MaximumAmountOfPlayers)
        {
            OnPlayersConnected.Invoke();
        }
        else
        {
            foreach(var connection in NetworkServer.connections)
            {
                connection.Value.identity.GetComponent<PlayerGUI>()
                    .RpcSetCurrentGameStatus($"Waiting for {MaximumAmountOfPlayers - numPlayers} more player");
            }
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
