using Assets._Scripts.Board;
using Assets._Scripts.Helpers;
using System;
using System.Linq;
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

    // Start is called before the first frame update
    internal void Awake()
    {
        GenerateBoardBorder();
        var fieldsContainer = GenerateFieldsContainer();
        AddFieldsToContainer(fieldsContainer.transform);
        AddBoardBorderMarking(fieldsContainer);
    }

    private GameObject GenerateBoardBorder()
    {
        GameObject boardBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boardBorder.name = BoardConfiguration.BorderGameObjectName;
        boardBorder.AddAsChildrenTo(gameObject);
        boardBorder.transform.localScale = new Vector3(BoardConfiguration.BoardHorizontalSize + BoardConfiguration.BorderThickness, BoardConfiguration.BorderHight, BoardConfiguration.BoardVerticalSize + BoardConfiguration.BorderThickness);
        boardBorder.transform.position = Vector3.zero;
        boardBorder.GetComponent<Renderer>().material = _boardBorderMaterial;

        return boardBorder;
    }

    private GameObject GenerateFieldsContainer()
    {
        GameObject fieldsContainer = new GameObject("FieldsContainer");
        fieldsContainer.transform.localScale = Vector3.one;
        fieldsContainer.AddAsChildrenTo(gameObject);
        return fieldsContainer;
    }

    private void AddFieldsToContainer(Transform containerTransform)
    {
        char currentSquareSymbol = BoardConfiguration.AvailableSquareSymbols[0];
        char currentSquareNumber = BoardConfiguration.AvailableSquareNumbers[0];

        Vector3 primarySpawnPoint = new Vector3
        {
            x = _simpleSquare.transform.localScale.x / 2f - BoardConfiguration.BoardHorizontalSize / 2f,
            y = containerTransform.localPosition.y + 0.1f, // TODO | replace with config variable
            z = _simpleSquare.transform.localScale.z / 2f - BoardConfiguration.BoardVerticalSize / 2f
        };

        Vector3 currentSpawnPoint = primarySpawnPoint;
        Material currentSquareMaterial = _darkSquareMaterial;

        for (int horizontalIndex = 0; horizontalIndex < BoardConfiguration.SquaresHorizontalDimension; horizontalIndex++)
        {
            var spawnedSquare = Instantiate(_simpleSquare, currentSpawnPoint, Quaternion.identity, containerTransform);
            currentSquareSymbol = BoardConfiguration.AvailableSquareSymbols[horizontalIndex];
            var currentSquareScript = spawnedSquare.AddComponent<Square>();
            currentSquareScript = SetCurrentSquareBasicComponents(currentSquareScript, BoardConfiguration.AvailableSquareNumbers[0], BoardConfiguration.AvailableSquareSymbols[horizontalIndex], currentSquareMaterial);
            //AddSquareTextMark(currentSquareScript);

            for (int verticalIndex = 1; verticalIndex < BoardConfiguration.SquaresVerticalDimension; verticalIndex++)
            {
                currentSpawnPoint = new Vector3(currentSpawnPoint.x, currentSpawnPoint.y, currentSpawnPoint.z + 1f);
                spawnedSquare = Instantiate(_simpleSquare, currentSpawnPoint, Quaternion.identity, containerTransform);
                currentSquareMaterial = SwitchSquareMaterial(currentSquareMaterial);
                currentSquareNumber = BoardConfiguration.AvailableSquareNumbers[verticalIndex];
                currentSquareScript = spawnedSquare.AddComponent<Square>();
                currentSquareScript = SetCurrentSquareBasicComponents(currentSquareScript, currentSquareNumber, currentSquareSymbol, currentSquareMaterial);
                //AddSquareTextMark(currentSquareScript);
            }

            currentSpawnPoint = new Vector3(currentSpawnPoint.x + 1f, currentSpawnPoint.y, primarySpawnPoint.z);
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

    // TODO | Need refactoring for sure! (but works well)
    private void AddBoardBorderMarking(GameObject fieldsContainer)
    {
        var squares = fieldsContainer.GetComponentsInChildren<Square>();

        foreach (var square in squares)
        {
            if (square.SquareMarkOrientationFlags.Contains(SquareMarkOrientation.Horizontal))
            {
                if (square.GetComponent<Square>().GetCoordinates().Row.Equals('1'))
                {
                    AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Bottom);
                }

                if (square.GetComponent<Square>().GetCoordinates().Row.Equals('8'))
                {
                    AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Top);
                }
            }

            if (square.GetComponent<Square>().SquareMarkOrientationFlags.Contains(SquareMarkOrientation.Vertical))
            {
                if (square.GetComponent<Square>().GetCoordinates().Column.Equals('A'))
                {
                    AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Left);
                }

                if (square.GetComponent<Square>().GetCoordinates().Column.Equals('H'))
                {
                    AddMarkingGameObjectToSquare(square.gameObject, SquareMarkLocation.Right);
                }
            }

        }
    }

    private void AddMarkingGameObjectToSquare(GameObject square, SquareMarkLocation markLocation)
    {
        var markContainer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        markContainer.name = markLocation.Equals(SquareMarkLocation.Top | SquareMarkLocation.Bottom) ? 
                                                    "SquareMarkHorizontal" : "SquareMarkVertical";

        markContainer.AddAsChildrenTo(square.gameObject);
        markContainer.transform.localScale = square.transform.localScale;

        Vector3 markPositionOffset = Vector3.zero;

        // TODO | Refactor this switch 
        switch (markLocation)
        {
            case SquareMarkLocation.Top:
                markPositionOffset = new Vector3
                {
                    z = markContainer.transform.localPosition.z + square.transform.localScale.z
                };
                break;

            case SquareMarkLocation.Bottom:
                markPositionOffset = new Vector3
                {
                    z = markContainer.transform.localPosition.z - square.transform.localScale.z
                };
                break;
            case SquareMarkLocation.Left:
                markPositionOffset = new Vector3
                {
                    x = markContainer.transform.localPosition.x - square.transform.localScale.x
                };
                break;
            case SquareMarkLocation.Right:
                markPositionOffset = new Vector3
                {
                    x = markContainer.transform.localPosition.x + square.transform.localScale.x
                };
                break;
        }

        markContainer.transform.localPosition = markPositionOffset;
    }

    [Obsolete]
    private void AddSquareTextMark(Square currentSquare)
    {
        foreach (var textSymbolFlag in currentSquare.SquareMarkOrientationFlags)
        {
            GameObject squareMark = new GameObject(BoardConfiguration.SquareMarkTextGameObjectName);
            squareMark.AddAsChildrenTo(currentSquare.gameObject);
            SetTextSymbolLocalPosition(currentSquare, squareMark);
            SetTextSymbolTextMeshOptions(currentSquare, squareMark, textSymbolFlag);
        }
    }

    private void SetTextSymbolLocalPosition(Square currentSquare, GameObject squareMark)
    {
        squareMark.transform.localPosition = Vector3.zero;

        squareMark.transform.localEulerAngles = new Vector3(currentSquare.transform.rotation.x + 90f,
            currentSquare.transform.rotation.y, currentSquare.transform.rotation.z);

        squareMark.transform.localScale = new Vector3(currentSquare.gameObject.transform.localScale.x / 5,
            currentSquare.gameObject.transform.localScale.y, currentSquare.gameObject.transform.localScale.z / 5);
    }

    private void SetTextSymbolTextMeshOptions(Square square, GameObject squareMark, SquareMarkOrientation markOrientation)
    {
        squareMark.AddComponent<MeshRenderer>();
        var textSymbolTextMesh = squareMark.AddComponent<TextMesh>();

        textSymbolTextMesh.text = markOrientation == SquareMarkOrientation.Horizontal ?
            square.GetCoordinates().Column.ToString() : square.GetCoordinates().Row.ToString();

        textSymbolTextMesh.fontSize = 20;
    }
}
