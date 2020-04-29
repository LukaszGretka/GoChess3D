using Mirror;

namespace Assets._Scripts.Network.Helpers
{
    internal class NetworkPlayersHelper : NetworkBehaviour
    {
        internal static void UpdateGameStatusForConnectedPlayers(string message)
        {
            foreach (var connection in NetworkServer.connections)
            {
                connection.Value.identity.GetComponent<PlayerGUI>().RpcSetCurrentGameStatus(message);
            }
        }
    }
}
