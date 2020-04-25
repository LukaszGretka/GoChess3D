using Assets._Scripts.Pieces.Enums;
using UnityEngine;

[RequireComponent(typeof(PlayerGUI))]
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private void Awake()
    {
        Name = GameObject.Find("MainMenuGUIManager")?.GetComponent<MainMenuGuiManager>()?.PlayerName?? string.Empty;
    }

    public string Name { get; set; }

    public PieceColor PieceColor { get; set; }
}
