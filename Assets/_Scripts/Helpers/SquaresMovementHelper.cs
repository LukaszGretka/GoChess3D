using Assets._Scripts.Abstract;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Movement;
using Assets._Scripts.Pieces;
using Assets._Scripts.Pieces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal static class SquaresMovementHelper
{
    private const string SquareTagName = "Square";
    private static IEnumerable<Square> _onBoardSquares;

    internal static IEnumerable<Square> GetMovement(MovementType movementType, Square relativeSquare)
    {
        // TODO to refactor. May cost big performane hit.
        _onBoardSquares = GameObject.FindGameObjectsWithTag(SquareTagName)
                                    .Select(square => square.GetComponent<Square>());

        switch (movementType)
        {
            case MovementType.Diagonaly:
                return GetDiagonalyLocatedSquares(relativeSquare).FilterBlockedDiagonalFields(relativeSquare)
                                                                 .FilterSamePieceColorOccupiedSquares(relativeSquare);

            case MovementType.Derpendicularly:
                return GetDerpendicularlyLocatedSquares(relativeSquare).FilterBlockedDerpendicularFields(relativeSquare)
                                                                       .FilterSamePieceColorOccupiedSquares(relativeSquare);

            case MovementType.Knight:
                return GetKnightMovementLocatedSquares(relativeSquare).FilterSamePieceColorOccupiedSquares(relativeSquare);

            case MovementType.DiagonalAndDerpendicular:
                return CombinedDiagonalAndDerpendicular(relativeSquare).FilterBlockedDiagonalFields(relativeSquare)
                                                                       .FilterBlockedDerpendicularFields(relativeSquare)
                                                                       .FilterSamePieceColorOccupiedSquares(relativeSquare);
            default:
                break;
        }

        throw new NotSupportedException("Invalid value of: " + nameof(movementType));
    }

    private static IEnumerable<Square> GetVerticalLocatedSquares(Square relativeSquare)
    {
        return _onBoardSquares.Where(square => square.transform.position.x == relativeSquare.transform.position.x
                                                        && square.transform.position.z != relativeSquare.transform.position.z);
    }

    private static IEnumerable<Square> GetHorizontalLocatedSquares(Square relativeSquare)
    {
        return _onBoardSquares.Where(square => square.transform.position.z == relativeSquare.transform.position.z
                                                        && square.transform.position.x != relativeSquare.transform.position.x);
    }

    private static IEnumerable<Square> GetDerpendicularlyLocatedSquares(Square relativeSquare)
    {
        return Enumerable.Concat(GetVerticalLocatedSquares(relativeSquare), GetHorizontalLocatedSquares(relativeSquare));
    }

    private static IEnumerable<Square> GetDiagonalyLocatedSquares(Square relativeSquare)
    {
        var currentSquareZ = relativeSquare.transform.position.z;
        var currentSquareX = relativeSquare.transform.position.x;

        return _onBoardSquares.Where(square => ((square.transform.position.x - square.transform.position.z == currentSquareX - currentSquareZ)
                                            || (square.transform.position.x + square.transform.position.z == currentSquareX + currentSquareZ))
                                            && !(square.transform.position.z == currentSquareZ && square.transform.position.x == currentSquareX));
    }

    private static IEnumerable<Square> CombinedDiagonalAndDerpendicular(Square relativeSquare)
    {
        return Enumerable.Concat(GetDiagonalyLocatedSquares(relativeSquare), GetDerpendicularlyLocatedSquares(relativeSquare));
    }

    private static IEnumerable<Square> GetKnightMovementLocatedSquares(Square relativeSquare)
    {
        return _onBoardSquares.Where(square => (square.transform.position.z == relativeSquare.transform.position.z + 2f
                                                || square.transform.position.z == relativeSquare.transform.position.z - 2f)
                                                    && (square.transform.position.x == relativeSquare.transform.position.x + 1f
                                                || square.transform.position.x == relativeSquare.transform.position.x - 1f));
    }

    private static IEnumerable<Square> FilterBlockedDerpendicularFields(this IEnumerable<Square> squares, Square relativeSquare)
    {
        var squaresList = squares.ToList();

        var relativeSquarePosX = relativeSquare.transform.position.x;
        var relativeSquarePosZ = relativeSquare.transform.position.z;

        foreach (var occupiedSquare in squares.Where(square => square.IsOccupied))
        {
            var occupedSquarePosX = occupiedSquare.transform.position.x;
            var occupedSquarePosZ = occupiedSquare.transform.position.z;

            // Top
            if (occupedSquarePosX == relativeSquarePosX && occupedSquarePosZ > relativeSquarePosZ)
            {
                squaresList.RemoveAll(x => x.transform.position.x == occupedSquarePosX
                                        && x.transform.position.z > occupedSquarePosZ);
                continue;
            }

            // Bottom
            if (occupedSquarePosX == relativeSquarePosX && occupedSquarePosZ < relativeSquarePosZ)
            {
                squaresList.RemoveAll(x => x.transform.position.x == occupedSquarePosX
                                        && x.transform.position.z < occupedSquarePosZ);
                continue;
            }

            // Right
            if (occupedSquarePosZ == relativeSquarePosZ && occupedSquarePosX > relativeSquarePosX)
            {
                squaresList.RemoveAll(x => x.transform.position.z == occupedSquarePosZ
                                        && x.transform.position.x > occupedSquarePosX);
                continue;
            }

            // Left
            if (occupedSquarePosZ == relativeSquarePosZ && occupedSquarePosX < relativeSquarePosX)
            {
                squaresList.RemoveAll(x => x.transform.position.z == occupedSquarePosZ
                                        && x.transform.position.x < occupedSquarePosX);
                continue;
            }
        }

        return squaresList;
    }

    private static IEnumerable<Square> FilterBlockedDiagonalFields(this IEnumerable<Square> squares, Square relativeSquare)
    {
        var squaresList = squares.ToList();

        var relativeSquarePosX = relativeSquare.transform.position.x;
        var relativeSquarePosZ = relativeSquare.transform.position.z;

        foreach (var occupiedSquare in squares.Where(square => square.IsOccupied))
        {
            var occupedSquarePosX = occupiedSquare.transform.position.x;
            var occupedSquarePosZ = occupiedSquare.transform.position.z;

            // Top-Left
            if (occupedSquarePosX < relativeSquarePosX && occupedSquarePosZ > relativeSquarePosZ)
            {
                squaresList.RemoveAll(x => x.transform.position.x <= occupedSquarePosX
                                        && x.transform.position.z > occupedSquarePosZ);
                continue;
            }

            // Top-Right
            if (occupedSquarePosX > relativeSquarePosX && occupedSquarePosZ > relativeSquarePosZ)
            {
                squaresList.RemoveAll(x => x.transform.position.x >= occupedSquarePosX
                                        && x.transform.position.z > occupedSquarePosZ);
                continue;
            }

            // Down-Left
            if (occupedSquarePosX < relativeSquarePosX && occupedSquarePosZ < relativeSquarePosZ)
            {
                squaresList.RemoveAll(x => x.transform.position.x <= occupedSquarePosX
                                        && x.transform.position.z < occupedSquarePosZ);
                continue;
            }

            // Down-Right
            if (occupedSquarePosX > relativeSquarePosX && occupedSquarePosZ < relativeSquare.transform.position.z)
            {
                squaresList.RemoveAll(x => x.transform.position.x >= occupedSquarePosX
                                        && x.transform.position.z < occupedSquarePosZ);
                continue;
            }
        }

        return squaresList;
    }

    private static IEnumerable<Square> FilterSamePieceColorOccupiedSquares(this IEnumerable<Square> inputCollection, Square relativeSquare)
    {
        List<Square> outputCollection = inputCollection.ToList();

        var processedCollection = inputCollection.Select(x => x.GetComponentInChildren<Piece>())
                                                  .Where(x => x != null && x.PieceColor == relativeSquare.GetComponentInChildren<Piece>().PieceColor)
                                                  .Select(y => y.GetComponentInParent<Square>());

        foreach (var item in processedCollection)
        {
            foreach (var innerItem in inputCollection)
            {
                if (innerItem.Column == item.Column && innerItem.Row == item.Row)
                {
                    outputCollection.RemoveAll(pred => pred.GetCoordinates().Column == item.Column && pred.GetCoordinates().Row == item.Row);
                }
            }
        }

        return outputCollection;
    }

    [Obsolete]
    private static IEnumerable<Coords> GetDefaultPieceCoords<T>(PieceColor pieceColor) where T : Piece
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

    [Obsolete]
    private static List<Square> GetSpawnableSquares()
    {
        IEnumerable<GameObject> squaredTaggedGameObjects = GameObject.FindGameObjectsWithTag(SquareTagName);

        if (!squaredTaggedGameObjects.Any())
        {
            Debug.LogError($"Can not find any object with tag {SquareTagName}");
            return new List<Square>();
        }

        var squares = squaredTaggedGameObjects.SelectMany(square => square.GetComponents<Square>()).ToList();

        var spawningSquares = new List<Square>();
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('1')));
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('2')));
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('7')));
        spawningSquares.AddRange(squares.Where(x => x.GetCoordinates().Row.Equals('8')));

        return spawningSquares;
    }
}
