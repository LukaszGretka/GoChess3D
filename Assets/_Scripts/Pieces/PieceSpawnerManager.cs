using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Pieces
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
            }
        }

        private void SpawnPiece<T>(IEnumerable<Square> squares, PieceColor pieceColor) where T : Component, IPiece
        {
            Coords pieceBoardCoords = GetDefaultPieceCoords<T>(pieceColor);
            GameObject spawnerSquare = GetSpawnerSquareGameObject(squares, pieceBoardCoords);
            Vector3 pieceSpawnPosition = spawnerSquare.transform.localPosition;
            Instantiate(testKing, pieceSpawnPosition, Quaternion.identity, spawnerSquare.transform); // TODO | change last argum. single responsible principle
        }

        private Coords GetDefaultPieceCoords<T>(PieceColor pieceColor) where T : IPiece
        {
            Dictionary<Type, Coords> piecesCoordsTable = new Dictionary<Type, Coords>()
            {
                [typeof(King)] = pieceColor == PieceColor.White ? new Coords('1', 'E') : new Coords('8', 'E'),
                // TODO | Add more after whole implementation
            };

            if (!piecesCoordsTable.TryGetValue(typeof(T), out Coords coords))
            {
                Debug.LogError($"Invalid type for spawning piece. Can't found coords for type {typeof(T)}");
            }

            return coords;
        }

        private GameObject GetSpawnerSquareGameObject(IEnumerable<Square> squares, Coords coords)
        {
            return squares.Where(square => square.GetCoordinates().Column.Equals(coords.Column) && square.GetCoordinates().Row.Equals(coords.Row)).SingleOrDefault().gameObject;
        }
    }
}
