using Assets._Scripts.Logic;
using Assets._Scripts.Network.Helpers;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Network
{
    internal class ServerGameFlowEngine : NetworkBehaviour
    {
        [SerializeField]
        internal float initialPlayerGameTime;

        [SyncVar]
        internal bool FirstTurn = true;

        public override void OnStartServer()
        {
            base.OnStartServer();
            NetworkManagerGoChess3D.OnPlayersConnected += NetworkManagerGoChess3D_OnPlayersConnected;
        }

        private void NetworkManagerGoChess3D_OnPlayersConnected()
        {
            InitializeMatch();
        }

        private void InitializeMatch()
        {
            NetworkPlayersHelper.UpdateGameStatusForConnectedPlayers("Match started. Waiting for white player move.");
        }

        private void SwitchPlayerTurn()
        {
            throw new NotFiniteNumberException();
        }

        private void OnDestroy()
        {
            NetworkManagerGoChess3D.OnPlayersConnected -= NetworkManagerGoChess3D_OnPlayersConnected;
        }
    }
}
