using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Logic
{
    internal enum PlayerTurn
    {
        WhitePlayerTurn,
        BlackPlayerTurn
    }

    internal class GameFlowEngine : MonoBehaviour
    {
        [SerializeField]
        internal static float initialPlayerGameTime;

        internal static PlayerTurn currentPlayerTurn;  

        private void Start()
        {
            NetworkManagerGoChess3D.OnPlayersLoaded += NetworkManagerGoChess3D_OnPlayersLoaded;
        }

        private void NetworkManagerGoChess3D_OnPlayersLoaded()
        {
            // start timer
            currentPlayerTurn = PlayerTurn.WhitePlayerTurn;
        }

        private void SwitchPlayerTurn()
        {
            throw new NotFiniteNumberException();
        }
    }
}
