using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Pieces.Logic
{
    /// <summary>
    /// Contains various scenarios for pieces spawn configuration
    /// </summary>
    internal class PieceSpawnerManager : MonoBehaviour
    {
        internal static IEnumerable<Coords> GetDefaultPieceCoords<T>(PieceColor pieceColor) where T : IPiece
        {
            Dictionary<Type, IEnumerable<Coords>> piecesCoordsTable = new Dictionary<Type, IEnumerable<Coords>>()
            {
                [typeof(King)] = pieceColor == PieceColor.White ? new List<Coords>() { new Coords('1', 'E') } : new List<Coords>() { new Coords('8', 'E') },
                [typeof(Queen)] = pieceColor == PieceColor.White ? new List<Coords>() { new Coords('1', 'D') } : new List<Coords>() { new Coords('8', 'D') },
                [typeof(Bishop)] = pieceColor == PieceColor.White ? new List<Coords>() { new Coords('1', 'C'), new Coords('1', 'F') } : new List<Coords>() { new Coords('8', 'C'), new Coords('8', 'F') },
                [typeof(Knight)] = pieceColor == PieceColor.White ? new List<Coords>() { new Coords('1', 'B'), new Coords('1', 'G') } : new List<Coords>() { new Coords('8', 'B'), new Coords('8', 'G') },
                [typeof(Rook)] = pieceColor == PieceColor.White ? new List<Coords>() { new Coords('1', 'A'), new Coords('1', 'H') } : new List<Coords>() { new Coords('8', 'A'), new Coords('8', 'H') },
                [typeof(Pawn)] = pieceColor == PieceColor.White ? new List<Coords>()
                {
                    new Coords('2', 'A'),
                    new Coords('2', 'B'),
                    new Coords('2', 'C'),
                    new Coords('2', 'D'),
                    new Coords('2', 'E'),
                    new Coords('2', 'F'),
                    new Coords('2', 'G'),
                    new Coords('2', 'H')
                }
                : new List<Coords>()
                {
                    new Coords('7', 'A'),
                    new Coords('7', 'B'),
                    new Coords('7', 'C'),
                    new Coords('7', 'D'),
                    new Coords('7', 'E'),
                    new Coords('7', 'F'),
                    new Coords('7', 'G'),
                    new Coords('7', 'H')
                },
            };

            if (!piecesCoordsTable.TryGetValue(typeof(T), out IEnumerable<Coords> coords))
            {
                Debug.LogError($"Invalid type for spawning piece. Can't found coords for type {typeof(T)}");
            }

            return coords;
        }
    }
}
