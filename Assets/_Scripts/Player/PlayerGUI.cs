using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : Player
{
    private Text _playerNameTopBarUI;
    private Text _turnInfoTopBarUI;
    private Text _playerRemainTimeTopBarUI;

    public override void OnStartClient()
    {
        NetworkManagerGoChess3D.OnPlayersConnected += NetworkManagerGoChess3D_OnPlayersConnected;

        base.OnStartClient();

        GameObject topBarPanel = GameObject.Find("TopBarPanel");

        _playerNameTopBarUI = topBarPanel.transform.Find("PlayerNameValue").GetComponent<Text>();
        _turnInfoTopBarUI = topBarPanel.transform.Find("PlayerTurnInfoValue").GetComponent<Text>();
        _playerRemainTimeTopBarUI = topBarPanel.transform.Find("GameRemainTimeValue").GetComponent<Text>();
    }

    public override void OnStartLocalPlayer()
    {
        _playerNameTopBarUI.text = string.IsNullOrEmpty(Name) ? string.Format("{0} player", PieceColor.ToString())
                                              : Name;
    }

    public void OnDestroy()
    {
        NetworkManagerGoChess3D.OnPlayersConnected -= NetworkManagerGoChess3D_OnPlayersConnected;
    }

    private void NetworkManagerGoChess3D_OnPlayersConnected()
    {
        RpcSetCurrentGameStatus("Match will start in the second");
        RpcUpdateRemainingTime(200.ToString());
    }

    [ClientRpc]
    internal void RpcSetCurrentGameStatus(string gameStatusMessage)
    {
        _turnInfoTopBarUI.text = gameStatusMessage;  // TODO format it lately
    }

    internal void SetPlayerName()
    {
        _playerNameTopBarUI.text = string.IsNullOrEmpty(Name) ? string.Format("{0} player", PieceColor.ToString())
                                                              : Name;
    }

    // TODO | Should be synchronized with the server and update every 1 sec
    [ClientRpc]
    internal void RpcUpdateRemainingTime(string time)
    {
        _playerRemainTimeTopBarUI.text = time;
    }
}
