using Assets._Scripts.Board;
using Assets._Scripts.Helpers;
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
            var currentSquareSymbol = BoardConfiguration.AvailableSquareSymbols[horizontalIndex];
            var currentSquareScript = spawnedSquare.AddComponent<Square>();
            SetCurrentSquareBasicComponents(currentSquareScript, BoardConfiguration.AvailableSquareNumbers[0], BoardConfiguration.AvailableSquareSymbols[horizontalIndex], currentSquareMaterial);

            for (int verticalIndex = 1; verticalIndex < BoardConfiguration.SquaresVerticalDimension; verticalIndex++)
            {
                currentSpawnPoint = new Vector3(currentSpawnPoint.x, currentSpawnPoint.y, currentSpawnPoint.z + 1f);
                spawnedSquare = Instantiate(_simpleSquare, currentSpawnPoint, Quaternion.identity, containerTransform);
                currentSquareMaterial = SwitchSquareMaterial(currentSquareMaterial);
                var currentSquareNumber = BoardConfiguration.AvailableSquareNumbers[verticalIndex];
                currentSquareScript = spawnedSquare.AddComponent<Square>();
                SetCurrentSquareBasicComponents(currentSquareScript, currentSquareNumber, currentSquareSymbol, currentSquareMaterial);
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
        string markName = markLocation.Equals(SquareMarkLocation.Top | SquareMarkLocation.Bottom) ?
                                                    "SquareMarkHorizontal" : "SquareMarkVertical";

        var markContainer = new GameObject(markName, typeof(TextMesh));
        markContainer.AddAsChildrenTo(square.gameObject);

        markContainer.transform.localScale = new Vector3
        {
            x = square.transform.localScale.x / 10f,
            y = square.transform.localScale.y /2f,
            z = square.transform.localScale.z / 10f
        };

        markContainer.transform.localRotation = Quaternion.Euler(markLocation == SquareMarkLocation.Top ? -90f : 90f, 0f, 0f);

        var markCointanderTextMesh = markContainer.GetComponent<TextMesh>();
        markCointanderTextMesh.fontSize = 72;
        markCointanderTextMesh.text = SetMarkContainerText(square, markLocation);
        markCointanderTextMesh.alignment = TextAlignment.Center;
        markCointanderTextMesh.anchor = TextAnchor.MiddleCenter;

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

    private string SetMarkContainerText(GameObject square, SquareMarkLocation markLocation)
    {
        if (markLocation == SquareMarkLocation.Top || markLocation == SquareMarkLocation.Bottom)
        {
            return square.GetComponent<Square>().GetCoordinates().Column.ToString();
        }
        else if (markLocation == SquareMarkLocation.Right || markLocation == SquareMarkLocation.Left)
        {
            return square.GetComponent<Square>().GetCoordinates().Row.ToString();
        }

        return string.Empty;
    }
}
