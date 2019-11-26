using Assets._Scripts.Board;
using Assets._Scripts.Board.Models;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    private Coord _coord;

    public bool IsOccupied { get; set; }

    internal List<SquareMarkOrientation> SquareMarkOrientationFlags { get; private set; }

    internal void SetCoordinates(char row, char column)
    {
        _coord = new Coord(row, column);
        SetSquareTextSymbolFlat();
    }

    internal Coord GetCoordinates()
    {
        return _coord;
    }

    internal void SetColor(Material material)
    {
        gameObject.GetComponent<Renderer>().material = material;
    }

    private void SetSquareTextSymbolFlat()
    {
        SquareMarkOrientationFlags = new List<SquareMarkOrientation>();

        if (_coord.Row.Equals(BoardConfiguration.AvailableSquareNumbers[0]) ||
           _coord.Row.Equals(BoardConfiguration.AvailableSquareNumbers[BoardConfiguration.AvailableSquareNumbers.Length - 1]))
        {
            SquareMarkOrientationFlags.Add(SquareMarkOrientation.Horizontal);
        }
        if (_coord.Column.Equals(BoardConfiguration.AvailableSquareSymbols[0]) ||
            _coord.Column.Equals(BoardConfiguration.AvailableSquareSymbols[BoardConfiguration.AvailableSquareSymbols.Length - 1]))
        {
            SquareMarkOrientationFlags.Add(SquareMarkOrientation.Vertical);
        }
    }
}
