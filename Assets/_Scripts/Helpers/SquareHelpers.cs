using Assets._Scripts.Movement;
using System.Collections.Generic;
using System.Linq;

internal static class SquareHelpers
{
    internal static IEnumerable<Square> GetLocatedSquares(MovementType movementType, Square relativeSquare)
    {
        switch (movementType)
        {
            case MovementType.Diagonaly:
                return GetDiagonalyLocatedSquares(relativeSquare);
            case MovementType.Derpendicularly:
                return GetDerpendicularlyLocatedSquares(relativeSquare);
            case MovementType.Knight:
                return GetKnightMovementLocatedSquares(relativeSquare);
            case MovementType.DiagonalAndDerpendicular:
                return CombinedDiagonalAndDerpendicular(relativeSquare);
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
}
