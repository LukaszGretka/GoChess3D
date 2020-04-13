using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static string PlayerName;
    public Text playerInput;

    public void OnClick_ConnectButton()
    {
        PlayerName = playerInput.text; // add validation
        SceneManager.LoadScene("Match");
    }
}

