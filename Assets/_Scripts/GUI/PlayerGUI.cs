using Assets._Scripts.Logic;
using Assets._Scripts.Network;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
    private Text _playerNameTopBarUI;
    private Text _turnInfoTopBarUI;
    private Text _playerRemainTimeTopBarUI;

    private void Start()
    {
        GameObject topBarPanel = GameObject.Find("TopBarPanel");

        _playerNameTopBarUI = topBarPanel.transform.Find("PlayerNameValue").GetComponent<Text>();
        _turnInfoTopBarUI = topBarPanel.transform.Find("PlayerTurnInfoValue").GetComponent<Text>();
        _playerRemainTimeTopBarUI = topBarPanel.transform.Find("GameRemainTimeValue").GetComponent<Text>();
    }

    public void ReadyButton_OnClick(Button button)
    {
        var gameFlowEngine = GameObject.FindGameObjectWithTag("GameFlowEngine").GetComponent<ServerGameFlowEngine>();
        if (gameFlowEngine != null)
        {
            gameFlowEngine.SetPlayerAsReady(GetComponent<Player>());
            button.gameObject.SetActive(false);
        }
    }

    internal void SetCurrentPlayerTurn(PlayerTurn playerTurn)
    {
        _turnInfoTopBarUI.text = playerTurn.ToString();  // TODO format it lately
    }

    internal void SetPlayerName()
    {
        string playerName = GetComponent<Player>().Name;

        _playerNameTopBarUI.text = string.IsNullOrEmpty(playerName) ? string.Format("{0} player", GetComponent<Player>().PieceColor.ToString()) 
                                                                    : playerName;
    }

    // TODO | Should be synchronized with the server and update every 1 sec
    internal void UpdateRemainingTime(string time)
    {
        _playerRemainTimeTopBarUI.text = time;
    }
}
