using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    private const int BoardHorizontalSize = 8;
    private const int BoardVerticalSize = 8;
    private const int BoardBorderThickness = 1;

    private readonly char[] _availableSquareSymbols = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
    private readonly char[] _availableSquareNumbers = { '1', '2', '3', '4', '5', '6', '7', '8' };

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

        char _currentSquareSymbol = _availableSquareSymbols[0];
        char _currentSquareNumber = _availableSquareNumbers[0];

        Material currentSquareMaterial = _darkSquareMaterial;

        for (int horizontalIndex = 0; horizontalIndex < BoardHorizontalSize; horizontalIndex++)
        {
            var spawnedSquare = Instantiate(_simpleSquare, spawnPoint, Quaternion.identity, transform);
            _currentSquareSymbol = _availableSquareSymbols[horizontalIndex];
            var currentSquareScript = spawnedSquare.AddComponent<Square>();
            SetCurrentSquareBasicComponents(currentSquareScript, _availableSquareNumbers[0], _availableSquareSymbols[horizontalIndex], currentSquareMaterial);

            for (int verticalIndex = 1; verticalIndex < BoardVerticalSize; verticalIndex++)
            {
                spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + 1);
                spawnedSquare = Instantiate(_simpleSquare, spawnPoint, Quaternion.identity, transform);
                currentSquareMaterial = SwitchSquareMaterial(currentSquareMaterial);
                _currentSquareNumber = _availableSquareNumbers[verticalIndex];
                currentSquareScript = spawnedSquare.AddComponent<Square>();
                SetCurrentSquareBasicComponents(currentSquareScript, _currentSquareNumber, _currentSquareSymbol, currentSquareMaterial);
            }

            spawnPoint = new Vector3(spawnPoint.x + 1, spawnPoint.y, 0);
        }
    }

    private Material SwitchSquareMaterial(Material currentMaterial)
    {
        return currentMaterial == _darkSquareMaterial ? _lightSquareMaterial : _darkSquareMaterial;
    }

    private void SetCurrentSquareBasicComponents(Square currentSquare, char number, char square, Material color)
    {
        currentSquare.SetCoordinates(number, square);
        currentSquare.SetColor(color);
    }

    private void GenerateBoardBorder()
    {
        GameObject boardBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boardBorder.name = "Border";

        boardBorder.transform.parent = transform;
        boardBorder.transform.localScale = new Vector3(BoardHorizontalSize + BoardBorderThickness, _simpleSquare.transform.localScale.y, BoardVerticalSize + BoardBorderThickness);
        boardBorder.transform.position = new Vector3((BoardHorizontalSize - BoardBorderThickness) / 2.0f, transform.position.y - _simpleSquare.transform.position.y - (_simpleSquare.transform.localScale.y) / 2.0f, (BoardVerticalSize - BoardBorderThickness) / 2.0f);

        boardBorder.GetComponent<Renderer>().material = _boardBorderMaterial;
    }
}
