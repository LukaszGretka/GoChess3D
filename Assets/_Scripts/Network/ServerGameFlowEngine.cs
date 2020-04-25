using Assets._Scripts.Logic;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Network
{
    internal class ServerGameFlowEngine : NetworkBehaviour
    {
        [SerializeField]
        internal static float initialPlayerGameTime;

        private List<Player> _readyPlayers;

        private void Start()
        {
            _readyPlayers = new List<Player>();
        }

        internal void SetPlayerAsReady(Player player)
        {
            _readyPlayers.Add(player);

            if (_readyPlayers.Count == 2)
            {
                RpcStartMatch();
            }
        }

        [ClientRpc]
        internal void RpcStartMatch()
        {
            foreach(var player in _readyPlayers)
            {
                var playerGUI = player.GetComponent<PlayerGUI>();

                if (playerGUI != null)
                {
                    playerGUI.SetPlayerName();
                    playerGUI.SetCurrentPlayerTurn(PlayerTurn.WhitePlayerTurn);
                    playerGUI.UpdateRemainingTime(initialPlayerGameTime.ToString("mm:ss"));
                }
            }
        }

        private void SwitchPlayerTurn()
        {
            throw new NotFiniteNumberException();
        }
    }
}
