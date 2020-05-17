using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Configuration;
using Assets._Scripts.Logic.PiecesMovement.Abstract;
using Assets._Scripts.Pieces;
using Assets._Scripts.Pieces.Enums;
using Assets._Scripts.Pieces.Helpers;
using Assets._Scripts.Pieces.Logic;
using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerPieceSpawner : NetworkBehaviour
{
    private const string SquareTagName = "Square";
    private const string PieceTagName = "Piece";

    internal void SpawnPiecesAtDefaultPositions(NetworkConnection playerNetworkConnection, PieceColor playerPieceColor)
    {
        IEnumerable<GameObject> squaredTaggedGameObjects = GameObject.FindGameObjectsWithTag(SquareTagName);

        if (!squaredTaggedGameObjects.Any())
        {
            Debug.LogError($"Can not find any object with tag {SquareTagName}");
            return;
        }

        var squares = squaredTaggedGameObjects.SelectMany(square => square.GetComponents<Square>()).ToList();

        var spawningSquares = new List<Square>();
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('1')));
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('2')));
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('7')));
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('8')));

        SpawnPiece<King>(spawningSquares, playerNetworkConnection, playerPieceColor);
        SpawnPiece<Queen>(spawningSquares, playerNetworkConnection, playerPieceColor);
        SpawnPiece<Bishop>(spawningSquares, playerNetworkConnection, playerPieceColor);
        SpawnPiece<Knight>(spawningSquares, playerNetworkConnection, playerPieceColor);
        SpawnPiece<Rook>(spawningSquares, playerNetworkConnection, playerPieceColor);
        SpawnPiece<Pawn>(spawningSquares, playerNetworkConnection, playerPieceColor);

        SetSpawningSquaresAsOccupied(spawningSquares);
    }

    private void SpawnPiece<T>(IEnumerable<Square> squares, NetworkConnection playerNetworkConnection, PieceColor playerPieceColor) where T : Component, IPiece
    {
        IEnumerable<Coords> pieceBoardCoords = PieceSpawnerManager.GetDefaultPieceCoords<T>(playerPieceColor);

        foreach (Coords pieceCoords in pieceBoardCoords)
        {
            var pieceGameObjectResource = Resources.Load($"{Path.PiecesPrefabsPath}{typeof(T).Name}");
            var spawnedPiece = SpawnPieceAtDefaultSquare(squares, pieceCoords, pieceGameObjectResource);
            var pieceAttachedScript = spawnedPiece.AddComponent<T>();

            pieceAttachedScript.PieceColor = playerPieceColor;
            (pieceAttachedScript as PieceMovementBase).CurrentPosition = pieceCoords;

            PieceHelper.SetDefaultPieceMaterial(spawnedPiece);

            spawnedPiece.transform.localRotation = Quaternion.Euler(default, pieceAttachedScript.PieceColor == PieceColor.White ? 180f : default, default);
            NetworkServer.Spawn(spawnedPiece, playerNetworkConnection);
        }
    }

    private GameObject SpawnPieceAtDefaultSquare(IEnumerable<Square> squares, Coords pieceCoords, UnityEngine.Object pieceGameObjectResource)
    {
        Transform spawnerSquareTransform = GetSpawnerSquareTransform(squares, pieceCoords);
        var spawnedPiece = (GameObject)Instantiate(pieceGameObjectResource, spawnerSquareTransform.localPosition, Quaternion.identity, spawnerSquareTransform);
        SetNetworkTransformChildComponent(spawnerSquareTransform, spawnedPiece);

        spawnedPiece.transform.localScale = new Vector3(spawnerSquareTransform.localScale.x / 2f, spawnerSquareTransform.localScale.y * 10f, spawnerSquareTransform.localScale.z / 2f);
        spawnedPiece.transform.localPosition = Vector3.up;

        return spawnedPiece;
    }

    private Transform GetSpawnerSquareTransform(IEnumerable<Square> squares, Coords coords)
    {
        return squares.Where(square => square.GetCoordinates().Column.Equals(coords.Column) && square.GetCoordinates().Row.Equals(coords.Row)).SingleOrDefault().gameObject.transform;
    }

    private void SetNetworkTransformChildComponent(Transform squareTransform, GameObject piece)
    {
        var childTransform = squareTransform.gameObject.AddComponent<NetworkTransformChild>();
        childTransform.target = piece.transform;
    }

    private void SetSpawningSquaresAsOccupied(List<Square> squares)
    {
        squares.ForEach(square => square.IsOccupied = true);
    }
}