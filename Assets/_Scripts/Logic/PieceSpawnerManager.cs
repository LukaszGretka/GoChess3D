using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Configuration;
using Assets._Scripts.Pieces;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Logic
{
    /// <summary>
    /// Contains various scenarios for pieces spawn configuration
    /// </summary>
    internal class PieceSpawnerManager : MonoBehaviour
    {
        private const string SQUARE_TAG_NAME = "Square";

        public GameObject testKing; //TODO | DELETE IF AFTER TESTS (should be get from resorces)

        private void Start()
        {
            SpawnPiecesAtDefaultPositions();
        }

        /// <summary>
        /// Default scenario for standard cheess game
        /// </summary>
        internal void SpawnPiecesAtDefaultPositions()
        {
            IEnumerable<GameObject> squaredTaggedGameObjects = GameObject.FindGameObjectsWithTag(SQUARE_TAG_NAME);

            if (!squaredTaggedGameObjects.Any())
            {
                Debug.LogError($"Can not find any object with tag {SQUARE_TAG_NAME}");
                return;
            }

            var squares = squaredTaggedGameObjects.SelectMany(square => square.GetComponents<Square>()).ToList();

            var spawningSquares = new List<Square>();
            spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('1')));
            spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('2')));
            spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('7')));
            spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('8')));

            for (int currentPieceColor = 0; currentPieceColor <= 1; currentPieceColor++)
            {
                SpawnPiece<King>(spawningSquares, (PieceColor)currentPieceColor);
                SpawnPiece<Queen>(spawningSquares, (PieceColor)currentPieceColor);
            }
        }

        private void SpawnPiece<T>(IEnumerable<Square> squares, PieceColor pieceColor) where T : Component, IPiece
        {
            IEnumerable<Coords> pieceBoardCoords = GetDefaultPieceCoords<T>(pieceColor);

            foreach (Coords pieceCoords in pieceBoardCoords)
            {
                Transform spawnerSquareTransform = GetSpawnerSquareTransform(squares, pieceCoords);
                var pieceGameObject = Resources.Load($"{Path.PiecesPrefabsPath}{typeof(T).Name}");
                var spawnedPiece = Instantiate(pieceGameObject, spawnerSquareTransform.localPosition, Quaternion.identity, spawnerSquareTransform);
                var component = (spawnedPiece as GameObject).AddComponent<T>();
                component.PieceColor = pieceColor;
            }
        }

        private IEnumerable<Coords> GetDefaultPieceCoords<T>(PieceColor pieceColor) where T : IPiece
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

        private Transform GetSpawnerSquareTransform(IEnumerable<Square> squares, Coords coords)
        {
            return squares.Where(square => square.GetCoordinates().Column.Equals(coords.Column) && square.GetCoordinates().Row.Equals(coords.Row)).SingleOrDefault().gameObject.transform;
        }
    }
}
