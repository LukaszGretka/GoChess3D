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


        private void SwitchPlayerTurn()
        {
            throw new NotFiniteNumberException();
        }
    }
}
