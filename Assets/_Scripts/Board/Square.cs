using Assets._Scripts.Board;
using Assets._Scripts.Board.Models;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField]
    internal char Row;

    [SerializeField]
    internal char Column;

    [SerializeField]
    internal bool IsOccupied;   

    internal List<SquareMarkOrientation> SquareMarkOrientationFlags { get; private set; }

    internal Coords GetCoordinates()
    {
        return new Coords(Row, Column);
    }

    internal void SetColor(Material material)
    {
        gameObject.GetComponent<Renderer>().material = material;
    }

    private void SetSquareTextSymbolFlat()
    {
        SquareMarkOrientationFlags = new List<SquareMarkOrientation>();

        if (GetCoordinates().Row.Equals(BoardConfiguration.AvailableSquareNumbers[0]) ||
           GetCoordinates().Row.Equals(BoardConfiguration.AvailableSquareNumbers[BoardConfiguration.AvailableSquareNumbers.Length - 1]))
        {
            SquareMarkOrientationFlags.Add(SquareMarkOrientation.Horizontal);
        }
        if (GetCoordinates().Column.Equals(BoardConfiguration.AvailableSquareSymbols[0]) ||
            GetCoordinates().Column.Equals(BoardConfiguration.AvailableSquareSymbols[BoardConfiguration.AvailableSquareSymbols.Length - 1]))
        {
            SquareMarkOrientationFlags.Add(SquareMarkOrientation.Vertical);
        }
    }
}
