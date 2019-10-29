using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    private const int BoardHorizontalSize = 8;
    private const int BoardVerticalSize = 8;

    [SerializeField]
    private GameObject _simpleSquare;

    [SerializeField]
    private Material _darkSquareMaterial;

    [SerializeField]
    private Material _lightSquareMaterial;

    // Start is called before the first frame update
    internal void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Vector3 spawnPoint = Vector3.zero;
        Material currentSquareMaterial = _darkSquareMaterial;

        for (int horizontalIndex = 0; horizontalIndex < BoardHorizontalSize; horizontalIndex++)
        {
            var spawnedSquare = Instantiate(_simpleSquare, spawnPoint, Quaternion.identity, transform);
            spawnedSquare.GetComponent<Renderer>().material = currentSquareMaterial;

            for (int verticalIndex = 1; verticalIndex < BoardVerticalSize; verticalIndex++)
            {
                spawnPoint = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + 1);
                spawnedSquare = Instantiate(_simpleSquare, spawnPoint, Quaternion.identity, transform);
                currentSquareMaterial = SwitchSquareMaterial(currentSquareMaterial);
                spawnedSquare.GetComponent<Renderer>().material = currentSquareMaterial;
            }

            spawnPoint = new Vector3(spawnPoint.x + 1, spawnPoint.y, 0);
        }
    }

    private Material SwitchSquareMaterial(Material currentMaterial)
    {
        return currentMaterial == _darkSquareMaterial ? _lightSquareMaterial : _darkSquareMaterial;
    }
}
