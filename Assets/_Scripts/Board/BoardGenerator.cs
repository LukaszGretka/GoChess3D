using Assets._Scripts.Board;
using Assets._Scripts.Board.Models;
using Assets._Scripts.Configuration;
using Assets._Scripts.Helpers;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _simpleSquare;

    [SerializeField]
    private Material _darkSquareMaterial;

    [SerializeField]
    private Material _lightSquareMaterial;

    [SerializeField]
    private Material _boardBorderMaterial;

    internal static List<Square> Squares { get; private set; }

    internal void Awake()
    {
        GenerateBoardBorder();
        var fieldsContainer = GenerateFieldsContainer();
        AddFieldsToContainer(fieldsContainer.transform);
        AddBoardBorderMarking(fieldsContainer.GetComponentsInChildren<Square>());
    }

    private GameObject GenerateBoardBorder()
    {
        GameObject boardBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boardBorder.name = GameObjectNames.Border;
        boardBorder.AddAsChildrenTo(gameObject);
        boardBorder.transform.localScale = new Vector3(BoardConfiguration.BoardHorizontalSize + BoardConfiguration.BorderThickness, BoardConfiguration.BorderHight, BoardConfiguration.BoardVerticalSize + BoardConfiguration.BorderThickness);
        boardBorder.transform.position = Vector3.zero;
        boardBorder.GetComponent<Renderer>().material = _boardBorderMaterial;

        return boardBorder;
    }

    private GameObject GenerateFieldsContainer()
    {
        GameObject fieldsContainer = new GameObject(GameObjectNames.FieldsContainer);
        fieldsContainer.transform.localScale = Vector3.one;
        fieldsContainer.AddAsChildrenTo(gameObject);
        return fieldsContainer;
    }

    private void AddFieldsToContainer(Transform containerTransform)
    {
        Squares = new List<Square>();

        Vector3 primarySpawnPoint = new Vector3
        {
            x = _simpleSquare.transform.localScale.x / 2f - BoardConfiguration.BoardHorizontalSize / 2f,
            y = containerTransform.localPosition.y + BoardConfiguration.FieldsHeight,
            z = _simpleSquare.transform.localScale.z / 2f - BoardConfiguration.BoardVerticalSize / 2f
        };

        Vector3 currentSpawnPoint = primarySpawnPoint;
        Material currentSquareMaterial = _darkSquareMaterial;

        for (int horizontalIndex = 0; horizontalIndex < BoardConfiguration.SquaresHorizontalDimension; horizontalIndex++)
        {
            var spawnedSquare = Instantiate(_simpleSquare, currentSpawnPoint, Quaternion.identity, containerTransform);
            var currentSquareSymbol = BoardConfiguration.AvailableSquareSymbols[horizontalIndex];
            var currentSquareScript = spawnedSquare.AddComponent<Square>();
            SetCurrentSquareBasicComponents(currentSquareScript, BoardConfiguration.AvailableSquareNumbers[0], BoardConfiguration.AvailableSquareSymbols[horizontalIndex], currentSquareMaterial);
            Squares.Add(currentSquareScript);

            for (int verticalIndex = 1; verticalIndex < BoardConfiguration.SquaresVerticalDimension; verticalIndex++)
            {
                currentSpawnPoint = new Vector3(currentSpawnPoint.x, currentSpawnPoint.y, currentSpawnPoint.z + BoardConfiguration.FieldsSplitDistance);
                spawnedSquare = Instantiate(_simpleSquare, currentSpawnPoint, Quaternion.identity, containerTransform);
                currentSquareMaterial = SwitchSquareMaterial(currentSquareMaterial);
                var currentSquareNumber = BoardConfiguration.AvailableSquareNumbers[verticalIndex];
                currentSquareScript = spawnedSquare.AddComponent<Square>();
                SetCurrentSquareBasicComponents(currentSquareScript, currentSquareNumber, currentSquareSymbol, currentSquareMaterial);
                Squares.Add(currentSquareScript);
            }

            currentSpawnPoint = new Vector3(currentSpawnPoint.x + BoardConfiguration.FieldsSplitDistance, currentSpawnPoint.y, primarySpawnPoint.z);
        }
    }

    private Material SwitchSquareMaterial(Material currentMaterial)
    {
        return currentMaterial == _darkSquareMaterial ? _lightSquareMaterial : _darkSquareMaterial;
    }

    private Square SetCurrentSquareBasicComponents(Square currentSquare, char number, char square, Material color)
    {
        currentSquare.SetCoordinates(number, square);
        currentSquare.SetColor(color);

        return currentSquare;
    }

    private void AddBoardBorderMarking(Square[] squares)
    {
        foreach (var square in squares)
        {
            if (square.SquareMarkOrientationFlags.Contains(SquareMarkOrientation.Horizontal))
            {
                AddHorizontalBoardMarker(square);
            }

            if (square.SquareMarkOrientationFlags.Contains(SquareMarkOrientation.Vertical))
            {
                AddVerticalBoardMarker(square);
            }
        }
    }

    private void AddHorizontalBoardMarker(Square square)
    {
        if (square.GetCoordinates().Row.Equals(BoardConfiguration.AvailableSquareNumbers[0]))
        {
            AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Bottom);
        }

        if (square.GetCoordinates().Row.Equals(BoardConfiguration.AvailableSquareNumbers[BoardConfiguration.AvailableSquareNumbers.Length - 1]))
        {
            AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Top);
        }
    }

    private void AddVerticalBoardMarker(Square square)
    {
        if (square.GetCoordinates().Column.Equals(BoardConfiguration.AvailableSquareSymbols[0]))
        {
            AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Left);
        }

        if (square.GetCoordinates().Column.Equals(BoardConfiguration.AvailableSquareSymbols[BoardConfiguration.AvailableSquareSymbols.Length - 1]))
        {
            AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Right);
        }
    }

    private void AddMarkingGameObjectToSquare(GameObject square, SquareMarkLocation markLocation)
    {
        string markName = markLocation.Equals(SquareMarkLocation.Top | SquareMarkLocation.Bottom) ?
                                                    GameObjectNames.SquareMarkHorizontal : GameObjectNames.SquareMarkVertical;

        var markContainer = new GameObject(markName, typeof(TextMeshPro));
        markContainer.AddAsChildrenTo(square);

        markContainer.transform.localScale = new Vector3
        {
            x = square.transform.localScale.x / 10f,
            y = square.transform.localScale.y /2f,
            z = square.transform.localScale.z / 10f
        };

        markContainer.transform.localRotation = Quaternion.Euler(90f, markLocation == SquareMarkLocation.Bottom ? 0f : 180f, markLocation == SquareMarkLocation.Right ? 180f: 0f);
        SetMarkContainerTextMesh(markContainer, square.GetComponent<Square>().GetCoordinates(), markLocation);
        markContainer.transform.localPosition = SetMarkContainerPosition(markContainer.transform.localPosition, square.transform.localScale, markLocation);
    }

    private void SetMarkContainerTextMesh(GameObject markContainer, Coords squareCoords, SquareMarkLocation markLocation)
    {
        var markContainerTextMesh = markContainer.GetComponent<TextMeshPro>();
        markContainerTextMesh.fontSize = BoardConfiguration.BorderMarksFontSize;
        markContainerTextMesh.text = SetMarkContainerText(squareCoords, markLocation);
        markContainerTextMesh.alignment = TextAlignmentOptions.Center;
    }

    private string SetMarkContainerText(Coords squareCoords, SquareMarkLocation markLocation)
    {
        string markText = string.Empty;

        if (markLocation == SquareMarkLocation.Top || markLocation == SquareMarkLocation.Bottom)
        {
            markText = squareCoords.Column.ToString();
        }
        else if (markLocation == SquareMarkLocation.Right || markLocation == SquareMarkLocation.Left)
        {
            markText = squareCoords.Row.ToString();
        }

        return markText;
    }

    private Vector3 SetMarkContainerPosition(Vector3 markContainerLocalPosition, Vector3 squareLocalScale, SquareMarkLocation markLocation)
    {
        switch (markLocation)
        {
            case SquareMarkLocation.Top:
                markContainerLocalPosition.z += squareLocalScale.z;
                break;

            case SquareMarkLocation.Bottom:
                markContainerLocalPosition.z -= squareLocalScale.z;
                break;

            case SquareMarkLocation.Left:
                markContainerLocalPosition.x -= squareLocalScale.x;
                break;

            case SquareMarkLocation.Right:
                markContainerLocalPosition.x += squareLocalScale.x;
                break;
        }

        markContainerLocalPosition.y += 0.1f;

        return markContainerLocalPosition;
    }
}
