using Assets._Scripts.Logic;
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

        FillPlayerGUIValues();
    }

    internal void FillPlayerGUIValues()
    {
        _playerNameTopBarUI.text = GetComponent<Player>().Name;
        _playerRemainTimeTopBarUI.text = GameFlowEngine.initialPlayerGameTime.ToString();
        _turnInfoTopBarUI.text = GameFlowEngine.currentPlayerTurn.ToString();
    }
}
