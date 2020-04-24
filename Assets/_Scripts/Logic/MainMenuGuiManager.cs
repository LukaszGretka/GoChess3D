using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuGuiManager : MonoBehaviour
{
    public string PlayerName;
    public Text playerInput;

    public void OnClick_ConnectButton()
    {
        PlayerName = playerInput.text; // add validation
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("OfflineScene");
    }
}

