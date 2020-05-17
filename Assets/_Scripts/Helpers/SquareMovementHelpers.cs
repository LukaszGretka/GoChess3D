using Assets._Scripts.Movement;
using System.Collections.Generic;
using System.Linq;

internal static class SquareMovementHelpers
{
    internal static IEnumerable<Square> GetMovement(MovementType movementType, Square relativeSquare)
    {
        switch (movementType)
        {
            case MovementType.Diagonaly:
                return GetDiagonalyLocatedSquares(relativeSquare).FilterBlockedDiagonalFields(relativeSquare);
            case MovementType.Derpendicularly:
                return GetDerpendicularlyLocatedSquares(relativeSquare).FilterBlockedDerpendicularFields(relativeSquare);
            case MovementType.Knight:
                return GetKnightMovementLocatedSquares(relativeSquare);
            case MovementType.DiagonalAndDerpendicular:
                return CombinedDiagonalAndDerpendicular(relativeSquare).FilterBlockedDiagonalFields(relativeSquare)
                                                                       .FilterBlockedDerpendicularFields(relativeSquare);
            default:
                break;
        }

        throw new System.NotSupportedException("Invalid value of: " + nameof(movementType));
    }

    internal static IEnumerable<Square> GetVerticalLocatedSquares(Square relativeSquare)
    {
        return BoardGenerator.Squares.Where(square => square.transform.position.x == relativeSquare.transform.position.x
                                                        && square.transform.position.z != relativeSquare.transform.position.z);
    }

    internal static IEnumerable<Square> GetHorizontalLocatedSquares(Square relativeSquare)
    {
        return BoardGenerator.Squares.Where(square => square.transform.position.z == relativeSquare.transform.position.z
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

        return BoardGenerator.Squares.Where(square => ((square.transform.position.x - square.transform.position.z == currentSquareX - currentSquareZ)
                                            || (square.transform.position.x + square.transform.position.z == currentSquareX + currentSquareZ))
                                            && !(square.transform.position.z == currentSquareZ && square.transform.position.x == currentSquareX));
    }

    private static IEnumerable<Square> CombinedDiagonalAndDerpendicular(Square relativeSquare)
    {
        return Enumerable.Concat(GetDiagonalyLocatedSquares(relativeSquare), GetDerpendicularlyLocatedSquares(relativeSquare));
    }

    private static IEnumerable<Square> GetKnightMovementLocatedSquares(Square relativeSquare)
    {
        return BoardGenerator.Squares.Where(square => (square.transform.position.z == relativeSquare.transform.position.z + 2f
                                                || square.transform.position.z == relativeSquare.transform.position.z - 2f)
                                                    && (square.transform.position.x == relativeSquare.transform.position.x + 1f
                                                || square.transform.position.x == relativeSquare.transform.position.x - 1f));
    }

    private static IEnumerable<Square> FilterBlockedDiagonalFields(this IEnumerable<Square> squares, Square relativeSquare)
    {
        // TODO this no make any sense to cast is to list and again to ienumerable
        var squaresList = squares.ToList();

        foreach (var occupiedSquare in squares.Where(square => square.IsOccupied))
        {
            // Top-Left
            if (occupiedSquare.transform.position.x < relativeSquare.transform.position.x && occupiedSquare.transform.position.z > relativeSquare.transform.position.z)
            {
                squaresList.RemoveAll(x => x.transform.position.x <= occupiedSquare.transform.position.x && x.transform.position.z >= occupiedSquare.transform.position.z);
                continue;
            }

            // Top-Right
            if (occupiedSquare.transform.position.x > relativeSquare.transform.position.x && occupiedSquare.transform.position.z > relativeSquare.transform.position.z)
            {
                squaresList.RemoveAll(x => x.transform.position.x >= occupiedSquare.transform.position.x && x.transform.position.z >= occupiedSquare.transform.position.z);
                continue;
            }

            // Down-Left
            if (occupiedSquare.transform.position.x < relativeSquare.transform.position.x && occupiedSquare.transform.position.z < relativeSquare.transform.position.z)
            {
                squaresList.RemoveAll(x => x.transform.position.x <= occupiedSquare.transform.position.x && x.transform.position.z <= occupiedSquare.transform.position.z);
                continue;
            }

            // Down-Right
            if (occupiedSquare.transform.position.x > relativeSquare.transform.position.x && occupiedSquare.transform.position.z < relativeSquare.transform.position.z)
            {
                squaresList.RemoveAll(x => x.transform.position.x >= occupiedSquare.transform.position.x && x.transform.position.z <= occupiedSquare.transform.position.z);
                continue;
            }
        }

        return squaresList;
    }

    private static IEnumerable<Square> FilterBlockedDerpendicularFields(this IEnumerable<Square> squares, Square relativeSquare)
    {
        // TODO this no make any sense to cast is to list and again to ienumerable
        var squaresList = squares.ToList();

        foreach (var occupiedSquare in squares.Where(square => square.IsOccupied))
        {
            // Left
            if (occupiedSquare.transform.position.x < relativeSquare.transform.position.x)
            {
                squaresList.RemoveAll(x => x.transform.position.x <= occupiedSquare.transform.position.x);
                continue;
            }

            // Right
            if (occupiedSquare.transform.position.x > relativeSquare.transform.position.x)
            {
                squaresList.RemoveAll(x => x.transform.position.x >= occupiedSquare.transform.position.x);
                continue;
            }

            // Top
            if (occupiedSquare.transform.position.z > relativeSquare.transform.position.z)
            {
                squaresList.RemoveAll(x => x.transform.position.z >= occupiedSquare.transform.position.z);
                continue;
            }

            // Bottom
            if (occupiedSquare.transform.position.z < relativeSquare.transform.position.z)
            {
                squaresList.RemoveAll(x => x.transform.position.z <= occupiedSquare.transform.position.z);
                continue;
            }
        }

        return squaresList;
    }
}
