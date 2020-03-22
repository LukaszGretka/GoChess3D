using Assets._Scripts.Abstract;
using Assets._Scripts.Configuration;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Pieces.Helpers
{
    internal static class PieceHelper
    {
        private const string BlackPieceMaterialName = "BlackPiecesMaterial";
        private const string WhitePieceMaterialName = "WhitePiecesMaterial";

        internal static void SetDefaultPieceMaterial(GameObject pieceGameObject)
        {
            var materialName = pieceGameObject.GetComponent<IPiece>().PieceColor == PieceColor.White ?
                WhitePieceMaterialName : BlackPieceMaterialName;

            var resourceMaterial = Resources.Load($"{Path.PiecesMaterialsPath}{materialName}");

            if (resourceMaterial is null)
                Debug.LogError($"No material has been found to be attached to {pieceGameObject.name}");

            pieceGameObject.GetComponent<Renderer>().material = resourceMaterial as Material;
        }
    }
}
