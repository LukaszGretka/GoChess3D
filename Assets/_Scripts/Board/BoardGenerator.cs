using Assets._Scripts.Board;
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
    internal void Start()
    {
        GenerateBoardFields();
        GenerateBoardBorder();
    }

    private void GenerateBoardFields()
    {
        Vector3 spawnPoint = Vector3.zero;

        char currentSquareSymbol = BoardConfiguration.AvailableSquareSymbols[0];
        char currentSquareNumber = BoardConfiguration.AvailableSquareNumbers[0];

        Material currentSquareMaterial = _darkSquareMaterial;

        for (int horizontalIndex = 0; horizontalIndex < BoardConfiguration.HorizontalSize; horizontalIndex++)
        {
            var spawnedSquare = Instantiate(_simpleSquare, spawnPoint, Quaternion.identity, transform);
            currentSquareSymbol = BoardConfiguration.AvailableSquareSymbols[horizontalIndex];
            var currentSquareScript = spawnedSquare.AddComponent<Square>();
            currentSquareScript = SetCurrentSquareBasicComponents(currentSquareScript, BoardConfiguration.AvailableSquareNumbers[0], BoardConfiguration.AvailableSquareSymbols[horizontalIndex], currentSquareMaterial);
            AddSquareTextMark(currentSquareScript);

            for (int verticalIndex = 1; verticalIndex < BoardConfiguration.VerticalSize; verticalIndex++)
            {
                spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + 1);
                spawnedSquare = Instantiate(_simpleSquare, spawnPoint, Quaternion.identity, transform);
                currentSquareMaterial = SwitchSquareMaterial(currentSquareMaterial);
                currentSquareNumber = BoardConfiguration.AvailableSquareNumbers[verticalIndex];
                currentSquareScript = spawnedSquare.AddComponent<Square>();
                currentSquareScript = SetCurrentSquareBasicComponents(currentSquareScript, currentSquareNumber, currentSquareSymbol, currentSquareMaterial);
                AddSquareTextMark(currentSquareScript);
            }

            spawnPoint = new Vector3(spawnPoint.x + 1, spawnPoint.y, 0);
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

    private void GenerateBoardBorder()
    {
        GameObject boardBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boardBorder.name = BoardConfiguration.BorderGameObjectName;

        boardBorder.transform.parent = transform;
        boardBorder.transform.localScale = new Vector3(BoardConfiguration.HorizontalSize + BoardConfiguration.BorderThickness, _simpleSquare.transform.localScale.y, BoardConfiguration.VerticalSize + BoardConfiguration.BorderThickness);
        boardBorder.transform.position = new Vector3((BoardConfiguration.HorizontalSize - 1) / 2.0f, transform.position.y - _simpleSquare.transform.position.y - (_simpleSquare.transform.localScale.y) / 2.0f, (BoardConfiguration.VerticalSize - 1) / 2.0f);

        boardBorder.GetComponent<Renderer>().material = _boardBorderMaterial;
    }

    private void AddSquareTextMark(Square currentSquare)
    {
        foreach (var textSymbolFlag in currentSquare.SquareTextSymbolFlags)
        {
            GameObject squareMark = new GameObject(BoardConfiguration.SquareMarkTextGameObjectName);
            squareMark.transform.parent = currentSquare.gameObject.transform;

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

    private void SetTextSymbolTextMeshOptions(Square square, GameObject squareMark, SquareMarkTextFlag squareMarkTextFlag)
    {
        squareMark.AddComponent<MeshRenderer>();
        var textSymbolTextMesh = squareMark.AddComponent<TextMesh>();

        textSymbolTextMesh.text = squareMarkTextFlag == SquareMarkTextFlag.Horizontal ?
            square.GetCoordinates().Column.ToString() : square.GetCoordinates().Row.ToString();

        textSymbolTextMesh.fontSize = 20;
    }
}
